using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Windows.Forms;
using ServerTurism.Model;
using ServerTurism.Networking;
using Message = ServerTurism.Networking.Message;

namespace Turism_cs.Networking
{
    public class Client
    {
        private readonly BlockingCollection<Message> messagesReceived;
        private readonly BlockingCollection<Message> messagesToSend;

        public Client(string server, int port)
        {
            Running = false;
            BFormatter = new BinaryFormatter();
            TcpClient = new TcpClient(server, port);
            messagesReceived = new BlockingCollection<Message>();
            messagesToSend = new BlockingCollection<Message>();
        }

        private BinaryFormatter BFormatter { get; }
        private TcpClient TcpClient { get; }
        private Thread ResponseThread { get; set; }
        private Thread HandleThread { get; set; }
        private Thread RequestThread { get; set; }
        public Collection<Excursie> Excursii { get; private set; }
        public Collection<Excursie> ExcursiiFiltered { get; private set; }
        private bool Running { get; set; }
        public event EventHandler LoggedIn;
        public event EventHandler LoggedOut;
        public event EventHandler ExcursiiListChanged;
        public event EventHandler ExcursiiFilteredListChanged;

        public void Start()
        {
            if (Running) return;
            Running = true;
            ResponseThread = new Thread(ListenForResponses);
            ResponseThread.Start();
            HandleThread = new Thread(MessageHandler);
            HandleThread.Start();
            RequestThread = new Thread(RequestSender);
            RequestThread.Start();
        }

        public void Stop()
        {
            if (!Running) return;
            Running = false;
            TcpClient.GetStream().Close();
            TcpClient.Close();
        }

        private void SendMessage(Message message) => BFormatter.Serialize(TcpClient.GetStream(), message);

        private Message ReceiveMessage()
        {
            try
            {
                return (Message) BFormatter.Deserialize(TcpClient.GetStream());
            }
            catch (Exception)
            {
                Console.WriteLine(@"Disconnecting");
                Stop();
                return null;
            }
        }

        private void ListenForResponses()
        {
            while (Running) messagesReceived.Add(ReceiveMessage());
        }

        private void MessageHandler()
        {
            while (Running)
            {
                Message message;
                try
                {
                    message = messagesReceived.Take();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return;
                }

                Thread thread;
                if (message == null)
                    return;
                switch (message.Type)
                {
                    case MessageType.Error:
                        thread = new Thread(() => HandleError((string) message.Contents));
                        thread.Start();
                        break;
                    case MessageType.LoggedIn:
                        thread = new Thread(HandleLoggedIn);
                        thread.Start();
                        break;
                    case MessageType.LoggedOut:
                        thread = new Thread(HandleLoggedOut);
                        thread.Start();
                        break;
                    case MessageType.GiveExcursii:
                        thread = new Thread(() => HandleGiveExcursii(message));
                        thread.Start();
                        break;
                    case MessageType.GiveSearchExcursii:
                        thread = new Thread(() => HandleGiveSearchExcursii(message));
                        thread.Start();
                        break;
                    case MessageType.RezervareMade:
                        thread = new Thread(HandleRezervareMade);
                        thread.Start();
                        break;
                    case MessageType.UpdateNotify:
                        thread = new Thread(() => HandleGiveExcursii(message));
                        thread.Start();
                        break;
                    default:
                        thread = new Thread(() => HandleError("Unknown server message type."));
                        thread.Start();
                        break;
                }
            }
        }

        private void HandleLoggedIn()
        {
            LoggedIn?.Invoke(this, new EventArgs());
        }

        private void HandleLoggedOut()
        {
            LoggedOut?.Invoke(this, new EventArgs());
        }

        private static void HandleError(string msg) => MessageBox.Show(msg);

        private static void HandleRezervareMade()
        {
            MessageBox.Show(@"Rezervare successfully!");
        }

        private void HandleGiveSearchExcursii(Message message)
        {
            ExcursiiFiltered = (Collection<Excursie>) message.Contents;
            ExcursiiFilteredListChanged?.Invoke(this, new EventArgs());
        }

        private void HandleGiveExcursii(Message message)
        {
            Excursii = (Collection<Excursie>) message.Contents;
            ExcursiiListChanged?.Invoke(this, new EventArgs());
        }

        private void RequestSender()
        {
            while (Running) SendMessage(messagesToSend.Take());
        }

        public void RequestLogin(string user, string pass) =>
            messagesToSend.Add(new Message(MessageType.LogIn, new List<string>
            {
                user,
                pass
            }));

        public void RequestLogout() => messagesToSend.Add(new Message(MessageType.LogOut, "Successfully logged out"));

        public void RequestGetExcursii() =>
            messagesToSend.Add(new Message(MessageType.GetExcursii, "Get excursii list"));

        public void RequestGetFilteredExcursii(string obiectiv, TimeSpan oraStart, TimeSpan oraFinish)
        {
            var filters = new List<string> {obiectiv, oraStart.ToString(), oraFinish.ToString()};
            messagesToSend.Add(new Message(MessageType.GetSearchExcursii, filters));
        }

        public void RequestRezervare(Excursie ex, string nume, string telefon, int nrBilete)
        {
            messagesToSend.Add(new Message(MessageType.MakeRezervare, new List<object>
            {
                ex, nume, telefon, nrBilete
            }));
        }
    }
}