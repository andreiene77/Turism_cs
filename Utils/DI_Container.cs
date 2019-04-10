using System;
using System.Data.SQLite;
using Turism_cs.Repository;
using Turism_cs.Service;

namespace Turism_cs.Utils
{
    public class DI_Container
    {
        public static readonly DI_Container container = new DI_Container();
        public SQLiteConnection Connection { get; }
        public AgentieRepository AgentieRepo { get; }
        public ExcursieRepository ExcursieRepo { get; }
        public RezervareRepository RezervareRepo { get; }
        public LoginService LoginService { get; }
        public ExcursiiManagementService MainWindowService { get; }
        public RezervareService RezervareService { get; }

        private DI_Container()
        {
            Connection = Utils.CreateConnection();
            AgentieRepo = new AgentieRepository(Connection);
            ExcursieRepo = new ExcursieRepository(Connection);
            RezervareRepo = new RezervareRepository(Connection, AgentieRepo, ExcursieRepo);
            LoginService = new LoginService(AgentieRepo);
            MainWindowService = new ExcursiiManagementService(ExcursieRepo);
            RezervareService = new RezervareService(MainWindowService, RezervareRepo);
        }
    }
}
