using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using ServerTurism.Model;
using ServerTurism.Service;
using ServerTurism.Utils;

namespace ServerTurism.Networking
{
    public class Server:MarshalByRefObject, IServerInterface
    {
        private readonly ExcursiiManagementService excursiiManagementService;
        private readonly LoginService loginService;
        private readonly RezervareService rezervareService;
        private Dictionary<string, ITurismObserver> ClientsDictionary { get; }

        public Server()
        {
            ClientsDictionary = new Dictionary<string, ITurismObserver>();
            loginService = DiContainer.GetContainer().LoginService;
            rezervareService = DiContainer.GetContainer().RezervareService;
            excursiiManagementService = DiContainer.GetContainer().ExcursiiManagementService;
        }

        public void LogInClient(string username, string pass, ITurismObserver client)
        {
            if (ClientsDictionary.ContainsKey(username))
            {
                client.ReceivedError("User already logged in!");
            }
            else
            {
                var agentie = loginService.Login_user(username, pass);
                if (agentie != null)
                {
                    ClientsDictionary.Add(username, client);
                    rezervareService.User = agentie;
                    client.GotLoggedIn();
                }
                else
                {
                    client.ReceivedError("Wrong credentials!");
                }
            }
        }

        public void LogOutClient(ITurismObserver client)
        {
            if (ClientsDictionary.ContainsValue(client))
            {
                var item = ClientsDictionary.First(kvp => kvp.Value == client);
                ClientsDictionary.Remove(item.Key);
                client.GotLoggedOut();
            }
            else
            {
                client.ReceivedError("User not logged in!");
            }
        }

        public void MakeRezervare(Excursie ex, string nume, string telefon, int nrBilete)
        {
            rezervareService.Rezerva(ex, nume, telefon, nrBilete);
            Update();
        }

        public Collection<Excursie> GiveSearchExcursii(string obiectiv, TimeSpan oraStart, TimeSpan oraFinish)
        {
            excursiiManagementService.UpdateExcursiiFiltered(obiectiv, oraStart, oraFinish);
            return excursiiManagementService.ExcursiiFiltered;
        }

        public Collection<Excursie> GiveExcursii()
        {
            excursiiManagementService.UpdateExcursii();
            return excursiiManagementService.Excursii;
        }

        private void Update()
        {
            foreach (var client in ClientsDictionary.Values)
                Task.Run(()=>client.RequestExcursii());
        }
    }
}
