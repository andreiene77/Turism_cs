using System.Data.SQLite;
using ServerTurism.Repository;
using ServerTurism.Service;

namespace ServerTurism.Utils
{
    public class DiContainer
    {
        private static DiContainer _container;

        private DiContainer()
        {
            Connection = Utils.CreateConnection();
            AgentieRepo = new AgentieRepository(Connection);
            ExcursieRepo = new ExcursieRepository(Connection);
            RezervareRepo = new RezervareRepository(Connection, AgentieRepo, ExcursieRepo);
            LoginService = new LoginService(AgentieRepo);
            ExcursiiManagementService = new ExcursiiManagementService(ExcursieRepo);
            RezervareService = new RezervareService(ExcursiiManagementService, RezervareRepo);
        }

        public SQLiteConnection Connection { get; }
        public AgentieRepository AgentieRepo { get; }
        public ExcursieRepository ExcursieRepo { get; }
        public RezervareRepository RezervareRepo { get; }
        public LoginService LoginService { get; }
        public ExcursiiManagementService ExcursiiManagementService { get; }
        public RezervareService RezervareService { get; }

        public static DiContainer GetContainer()
        {
            if (!(_container is null)) return _container;
            _container = new DiContainer();
            return _container;
        }
    }
}