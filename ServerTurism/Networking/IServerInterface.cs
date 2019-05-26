using System;
using System.Collections.ObjectModel;
using ServerTurism.Model;

namespace ServerTurism.Networking
{
    public interface IServerInterface
    {
        void LogInClient(string username, string pass, ITurismObserver client);
        void LogOutClient(ITurismObserver client);
        void MakeRezervare(Excursie ex, string nume, string telefon, int nrBilete);
        Collection<Excursie> GiveSearchExcursii(string obiectiv, TimeSpan oraStart, TimeSpan oraFinish);
        Collection<Excursie> GiveExcursii();
    }
}