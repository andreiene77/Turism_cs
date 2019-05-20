using System;
using System.Collections.Generic;
using System.Data.SQLite;
using ServerTurism.Model;

namespace ServerTurism.Repository
{
    public class RezervareRepository : IRepository<Rezervare, string>
    {
        private readonly SQLiteConnection connection;
        private readonly IRepository<Agentie, string> repoAg;
        private readonly IRepository<Excursie, string> repoEx;

        public RezervareRepository(SQLiteConnection connection, IRepository<Agentie, string> repoAg,
            IRepository<Excursie, string> repoEx)
        {
            this.connection = connection;
            this.repoAg = repoAg;
            this.repoEx = repoEx;
        }

        public void Add(Rezervare entity)
        {
            connection.Open();
            var id = "'" + entity.Id + "'";
            var usernameAg = "'" + entity.Agentie.Id + "'";
            var ide = "'" + entity.Excursie.Id + "'";
            var numeClient = "'" + entity.NumeClient + "'";
            var telefon = "'" + entity.Telefon + "'";
            var nrBilete = entity.NrBilete;
            var command = new SQLiteCommand(
                "INSERT INTO Rezervari(id,username_ag,ide,numeClient,telefon,nrBilete) VALUES " +
                "(" +
                id + "," +
                usernameAg + "," +
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
            var command = new SQLiteCommand("SELECT * FROM Rezervari WHERE id=" + "'" + id + "'" + ";", connection);
            var dataReader = command.ExecuteReader();
            var rezervare = new Rezervare(id, null, null, null, null, 0);
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
            FindOne(id);
            connection.Open();
            var command = new SQLiteCommand(
                "DELETE FROM Rezervari WHERE id=" + "'" + id + "'" + ";",
                connection
            );
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void Update(Rezervare entity) => throw new NotImplementedException();
    }
}