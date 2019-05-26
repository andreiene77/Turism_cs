using System.Collections.ObjectModel;
using ServerTurism.Model;

namespace ServerTurism.Networking
{
    public interface ITurismObserver
    {
        void GotLoggedIn();
        void GotLoggedOut();
        void ReceivedError(string msg);
        void RequestExcursii();
    }
}
