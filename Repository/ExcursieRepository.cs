using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;

namespace Turism_cs.Repository
{
    public class ExcursieRepository : IRepository<Excursie, string>
    {
        private SQLiteConnection connection;

        public ExcursieRepository(SQLiteConnection connection) => this.connection = connection;

        public void Add(Excursie entity)
        {
            connection.Open();
            string id = "'" + entity.Id + "'";
            string obiectiv = "'" + entity.Obiectiv + "'";
            string firmaTransport = "'" + entity.FirmaTransport + "'";
            string oraPlecarii = "'" + entity.OraPlecarii + "'";
            double pretul = entity.Pretul;
            int locuriDisponibile = entity.LocuriDisponibile;
            SQLiteCommand command = new SQLiteCommand(
                "INSERT INTO Excursii(id,obiectiv,firmaTransport,oraPlecarii,pretul,locuriDisponibile) VALUE " +
                "(" +
                id + "," +
                obiectiv + "," +
                firmaTransport + "," +
                oraPlecarii + "," +
                pretul + "," +
                locuriDisponibile +
                ");",
                connection
                );
            command.ExecuteNonQuery();
            connection.Close();
        }

        public List<Excursie> FindAll()
        {
            connection.Open();

            List<Excursie> excursii = new List<Excursie>();
            SQLiteCommand command = new SQLiteCommand("SELECT * FROM Excursii", connection);
            var dataRow = command.ExecuteReader();
            while (dataRow.Read())
            {
                string id = dataRow.GetString(0);
                string numeObiectiv = dataRow.GetString(1);
                string firmaTransport = dataRow.GetString(2);
                TimeSpan oraPlecare = dataRow.GetDateTime(3).TimeOfDay;
                double pret = dataRow.GetDouble(4);
                int locuriLibere = dataRow.GetInt32(5);

                Excursie e = new Excursie(id, numeObiectiv, firmaTransport, oraPlecare, pret, locuriLibere);
                excursii.Add(e);
            }
            connection.Close();
            return excursii;
        }

        public Excursie FindOne(string id)
        {
            connection.Open();
            SQLiteCommand command = new SQLiteCommand("SELECT * FROM Excursii WHERE id=" + "'" + id + "'" + ";", connection);
            SQLiteDataReader dataReader = command.ExecuteReader();
            Excursie excursie = new Excursie(id, null, null, TimeSpan.Zero, 0, 0);
            if (dataReader.HasRows)
            {
                dataReader.Read();
                excursie.Id = dataReader.GetString(0);
                excursie.Obiectiv = dataReader.GetString(1);
                excursie.FirmaTransport = dataReader.GetString(2);
                excursie.OraPlecarii = TimeSpan.Parse(dataReader.GetString(3));
                excursie.Pretul = dataReader.GetDouble(4);
                excursie.LocuriDisponibile = dataReader.GetInt32(5);
            }
            else
            {
                dataReader.Close();
                connection.Close();
                return null;
            }
            dataReader.Close();
            connection.Close();
            return excursie;
        }

        public void Remove(string id) => throw new NotImplementedException();

        public void Update(Excursie entity)
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "update Example set Info = :info, Text = :text where ID=:id";
                command.CommandText = "update Excursii set " +
                    "obiectiv=:obiectiv, " +
                    "firmaTransport=:firma, " +
                    "oraPlecarii=:ora, " +
                    "pretul=:pret, " +
                    "locuriDisponibile=:locuri " +
                    "where id=:id ";

                command.Parameters.Add("obiectiv", DbType.String).Value = entity.Obiectiv;
                command.Parameters.Add("firma", DbType.String).Value = entity.FirmaTransport;
                command.Parameters.Add("ora", DbType.String).Value = entity.OraPlecarii.ToString();
                command.Parameters.Add("pret", DbType.Double).Value = entity.Pretul;
                command.Parameters.Add("locuri", DbType.Int32).Value = entity.LocuriDisponibile;
                command.Parameters.Add("id", DbType.String).Value = entity.Id;
                
                var result = command.ExecuteNonQuery();
                if (result == 0)
                    throw new RepositoryException("Error!");
            }

            connection.Close();
        }
    }
}
