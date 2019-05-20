using Turism_cs.Networking;

namespace Turism_cs.Forms
{
    public class FormsContainer
    {
        private static FormsContainer _container;

        private FormsContainer()
        {
            Client = new Client("localhost", 5252);
            LoginForm = new LoginForm(Client);
            MainWindow = new MainWindow(Client);
            SearchWindow = new SearchWindow(Client);
            RezervareWindow = new RezervareWindow(Client);
        }

        public Client Client { get; }
        public LoginForm LoginForm { get; }
        public MainWindow MainWindow { get; }
        public SearchWindow SearchWindow { get; }
        public RezervareWindow RezervareWindow { get; }

        public static FormsContainer GetContainer()
        {
            if (!(_container is null)) return _container;
            _container = new FormsContainer();
            return _container;
        }
    }
}