using System.Configuration;
using System.Data.SQLite;

namespace Turism_cs.Utils
{
    public class Utils
    {
        private static string GetConnectionString(string name)
        {
            string connectionString = null;

            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings[name];

            if (settings != null)
                connectionString = settings.ConnectionString;
            return connectionString;
        }

        public static SQLiteConnection CreateConnection()
        {
            string connectionString = GetConnectionString("Agentie");
            if (connectionString == null)
                return null;
            var conn = new SQLiteConnection(connectionString);
            return conn;
        }
    }
}
