using System.Configuration;
using System.Data.SQLite;

namespace ServerTurism.Utils
{
    public static class Utils
    {
        private static string GetConnectionString(string name)
        {
            string connectionString = null;

            var settings = ConfigurationManager.ConnectionStrings[name];

            if (settings != null)
                connectionString = settings.ConnectionString;
            return connectionString;
        }

        public static SQLiteConnection CreateConnection()
        {
            var connectionString = GetConnectionString("Agentie");
            if (connectionString == null)
                return null;
            var conn = new SQLiteConnection(connectionString);
            return conn;
        }
    }
}