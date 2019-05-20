using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using ServerTurism.Model;
using ServerTurism.Service;
using ServerTurism.Utils;

namespace ServerTurism.Networking
{
    public class Server
    {
        private readonly ExcursiiManagementService excursiiManagementService;


        private readonly LoginService loginService;
        private readonly RezervareService rezervareService;

        public Server(int port)
        {
            TcpListener = new TcpListener(IPAddress.Loopback, port);
            ClientsDictionary = new Dictionary<string, TcpClient>();
            Running = false;
            BFormatter = new BinaryFormatter();
            loginService = DiContainer.GetContainer().LoginService;
            rezervareService = DiContainer.GetContainer().RezervareService;
            excursiiManagementService = DiContainer.GetContainer().ExcursiiManagementService;
        }

        public bool Running { get; private set; }
        private BinaryFormatter BFormatter { get; }
        private Thread ConnectionThread { get; set; }
        private TcpListener TcpListener { get; }
        private Dictionary<string, TcpClient> ClientsDictionary { get; }

        public void Start()
        {
            if (Running) return;
            TcpListener.Start();
            Running = true;
            ConnectionThread = new Thread(ListenForClientConnections);
            ConnectionThread.Start();
        }

        public void Stop()
        {
            if (!Running) return;
            TcpListener.Stop();
            Running = false;
        }

        private void ListenForClientConnections()
        {
            while (Running)
            {
                var connectedTcpClient = TcpListener.AcceptTcpClient();
                var handleThread = new Thread(() => HandleClient(connectedTcpClient));
                handleThread.Start();
            }
        }

        private void HandleClient(TcpClient connectedTcpClient)
        {
            Console.WriteLine("Client handler started");
            while (Running)
            {
                var message = ReceiveMessage(connectedTcpClient);
                if (message == null)
                {
                    Console.WriteLine("Client disconnected");
                    return;
                }

                switch (message.Type)
                {
                    case MessageType.LogIn:
                        LogInClient(message, connectedTcpClient);
                        break;
                    case MessageType.LogOut:
                        LogOutClient(connectedTcpClient);
                        break;
                    case MessageType.GetExcursii:
                        GiveExcursii(connectedTcpClient);
                        break;
                    case MessageType.GetSearchExcursii:
                        GiveSearchExcursii(message, connectedTcpClient);
                        break;
                    case MessageType.MakeRezervare:
                        MakeRezervare(message, connectedTcpClient);
                        break;
                }
            }
        }


        private void SendMessage(TcpClient client, Message message)
        {
            BFormatter.Serialize(client.GetStream(), message);
            Console.WriteLine("Message sent of type: " + message.Type);
        }

        private Message ReceiveMessage(TcpClient client)
        {
            try
            {
                var message = (Message) BFormatter.Deserialize(client.GetStream());
                Console.WriteLine("Message received of type: " + message.Type);
                return message;
            }
            catch (Exception)
            {
                client.GetStream().Close();
                client.Close();
            }

            return null;
        }

        private void LogInClient(Message message, TcpClient client)
        {
            var credentials = ((IEnumerable) message.Contents).Cast<string>().ToList();
            var username = credentials[0];
            var pass = credentials[1];
            lock (this)
            {
                if (ClientsDictionary.ContainsKey(username))
                {
                    SendMessage(client, new Message(MessageType.Error, "User already logged in!"));
                }
                else
                {
                    var agentie = loginService.Login_user(username, pass);
                    if (agentie != null)
                    {
                        ClientsDictionary.Add(username, client);
                        rezervareService.User = agentie;
                        SendMessage(client, new Message(MessageType.LoggedIn, "Successfully logged in!"));
                    }
                    else
                    {
                        SendMessage(client, new Message(MessageType.Error, "Wrong credentials!"));
                    }
                }
            }
        }

        private void LogOutClient(TcpClient client)
        {
            lock (this)
            {
                if (ClientsDictionary.ContainsValue(client))
                {
                    var item = ClientsDictionary.First(kvp => kvp.Value == client);
                    ClientsDictionary.Remove(item.Key);
                    SendMessage(client, new Message(MessageType.LoggedOut, "Successfully logged out!"));
                }
                else
                {
                    SendMessage(client, new Message(MessageType.Error, "User not logged in!"));
                }
            }
        }

        private void Update()
        {
            lock (this)
            {
                foreach (var client in ClientsDictionary.Values)
                    SendMessage(client, new Message(MessageType.UpdateNotify, excursiiManagementService.Excursii));
            }
        }

        private void MakeRezervare(Message message, TcpClient client)
        {
            lock (this)
            {
                var arguments = (List<object>) message.Contents;
                var ex = (Excursie) arguments[0];
                var nume = (string) arguments[1];
                var telefon = (string) arguments[2];
                var nrBilete = (int) arguments[3];
                rezervareService.Rezerva(ex, nume, telefon, nrBilete);
            }

            Update();
        }

        private void GiveSearchExcursii(Message message, TcpClient client)
        {
            lock (this)
            {
                var filters = ((IEnumerable) message.Contents).Cast<string>().ToList();
                var obiectiv = filters[0];
                var oraStart = TimeSpan.Parse(filters[1]);
                var oraFinish = TimeSpan.Parse(filters[2]);
                excursiiManagementService.UpdateExcursiiFiltered(obiectiv, oraStart, oraFinish);
                if (excursiiManagementService.ExcursiiFiltered != null)
                    SendMessage(client,
                        new Message(MessageType.GiveSearchExcursii, excursiiManagementService.ExcursiiFiltered));
            }
        }

        private void GiveExcursii(TcpClient client)
        {
            lock (this)
            {
                SendMessage(client, new Message(MessageType.GiveExcursii, excursiiManagementService.Excursii));
            }
        }
    }
}