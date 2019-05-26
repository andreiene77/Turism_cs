using System;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using ServerTurism.Model;
using ServerTurism.Networking;

namespace Turism_cs.Networking
{
    public class ClientRmi:MarshalByRefObject, ITurismObserver
    {
        private readonly IServerInterface server;
        public Collection<Excursie> Excursii { get; private set; }
        public Collection<Excursie> ExcursiiFiltered { get; private set; }
        public event EventHandler LoggedIn;
        public event EventHandler LoggedOut;
        public event EventHandler ExcursiiListChanged;
        public event EventHandler ExcursiiFilteredListChanged;

        public ClientRmi(IServerInterface server) => this.server = server;

        public void GotLoggedIn() => LoggedIn?.Invoke(this, new EventArgs());

        public void GotLoggedOut() => LoggedOut?.Invoke(this, new EventArgs());

        public void ReceivedError(string msg) => MessageBox.Show(msg);

        public void RequestLogin(string user, string pass) => server.LogInClient(user, pass, this);

        public void RequestLogout() => server.LogOutClient(this);

        public void RequestExcursii()
        {
            var excursii = server.GiveExcursii();
            Excursii = excursii;
            ExcursiiListChanged?.Invoke(this, new EventArgs());
        }

        public void RequestFilteredExcursii(string obiectiv, TimeSpan oraStart, TimeSpan oraFinish)
        {
            var excursii = server.GiveSearchExcursii(obiectiv, oraStart, oraFinish);
            ExcursiiFiltered = excursii;
            ExcursiiFilteredListChanged?.Invoke(this, new EventArgs());
        }

        public void RequestRezervare(Excursie ex, string nume, string telefon, int nrBilete) => 
            server.MakeRezervare(ex, nume, telefon, nrBilete);
    }
}
