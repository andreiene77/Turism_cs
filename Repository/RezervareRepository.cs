using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace Turism_cs.Repository
{
    public class RezervareRepository : IRepository<Rezervare, string>
    {
        private SQLiteConnection connection;
        private IRepository<Agentie, string> repoAg;
        private IRepository<Excursie, string> repoEx;

        public RezervareRepository(SQLiteConnection connection, IRepository<Agentie, string> repoAg, IRepository<Excursie, string> repoEx)
        {
            this.connection = connection;
            this.repoAg = repoAg;
            this.repoEx = repoEx;
        }

        public void Add(Rezervare entity)
        {
            connection.Open();
            string id = "'" + entity.Id + "'";
            string username_ag = "'" + entity.Agentie.Id + "'";
            string ide = "'" + entity.Excursie.Id + "'";
            string numeClient = "'" + entity.NumeClient + "'";
            string telefon = "'" + entity.Telefon + "'";
            int nrBilete = entity.NrBilete;
            SQLiteCommand command = new SQLiteCommand(
               "INSERT INTO Rezervari(id,username_ag,ide,numeClient,telefon,nrBilete) VALUES " +
               "(" +
               id + "," +
               username_ag + "," +
               ide + "," +
               numeClient + "," +
               telefon + "," +
               nrBilete +
               ");",
               connection
               );
            command.ExecuteNonQuery();
            connection.Close();
        }

        public List<Rezervare> FindAll() => throw new NotImplementedException();

        public Rezervare FindOne(string id)
        {
            connection.Open();
            SQLiteCommand command = new SQLiteCommand("SELECT * FROM Rezervari WHERE id=" + "'" + id + "'" + ";", connection);
            SQLiteDataReader dataReader = command.ExecuteReader();
            Rezervare rezervare = new Rezervare(id, null, null, null, null, 0);
            if (dataReader.HasRows)
            {
                dataReader.Read();
                rezervare.Id = dataReader.GetString(0);
                rezervare.Agentie = repoAg.FindOne(dataReader.GetString(1));
                rezervare.Excursie = repoEx.FindOne(dataReader.GetString(2));
                rezervare.NumeClient = dataReader.GetString(3);
                rezervare.Telefon = dataReader.GetString(4);
                rezervare.NrBilete = dataReader.GetInt32(5);
            }
            else
            {
                dataReader.Close();
                connection.Close();
                return null;
            }
            dataReader.Close();
            connection.Close();
            return rezervare;
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
                "DELETE FROM Rezervari WHERE id=" + "'" + id + "'" + ";",
                connection
                );
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void Update(Rezervare entity) => throw new NotImplementedException();
    }
}
