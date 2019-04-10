using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace Turism_cs.Repository
{
    public class AgentieRepository : IRepository<Agentie, string>
    {
        private SQLiteConnection connection;

        public AgentieRepository(SQLiteConnection connection) => this.connection = connection;

        public void Add(Agentie entity)
        {
            connection.Open();
            string username = "'" + entity.Id + "'";
            string password = "'" + entity.Password + "'";
            SQLiteCommand command = new SQLiteCommand(
                "INSERT INTO Agentii(usrname,pswd) VALUE " +
                "(" +
                username + "," +
                password +
                ");",
                connection
                );
            command.ExecuteNonQuery();
            connection.Close();
        }

        public List<Agentie> FindAll() => throw new NotImplementedException();

        public Agentie FindOne(string id)
        {
            connection.Open();
            SQLiteCommand command = new SQLiteCommand("SELECT * FROM Agentii WHERE usrname=" + "'" + id + "'" + ";", connection);
            SQLiteDataReader dataReader = command.ExecuteReader();
            Agentie agentie = new Agentie(id,  null);
            if (dataReader.HasRows)
            {
                dataReader.Read();
                agentie.Id = dataReader.GetString(0);
                agentie.Password = dataReader.GetString(1);
            }
            else
            {
                dataReader.Close();
                connection.Close();
                return null;
            }
            dataReader.Close();
            connection.Close();
            return agentie;
        }

        public void Remove(string id)
        {
            try
            {
                FindOne(id);
            }
            catch (RepositoryException)
            {
                throw;
            }
            connection.Open();
            SQLiteCommand command = new SQLiteCommand(
                "DELETE FROM Agentii WHERE id=" + "'" + id + "'" + ";",
                connection
                );
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void Update(Agentie entity) => throw new NotImplementedException();
    }
}
