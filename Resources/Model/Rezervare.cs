namespace ServerTurism.Model
{
    public class Rezervare : IHasId<string>
    {
        public Rezervare(string id, Agentie agentie, Excursie excursie, string numeClient, string telefon, int nrBilete)
        {
            Id = id;
            Agentie = agentie;
            Excursie = excursie;
            NumeClient = numeClient;
            Telefon = telefon;
            NrBilete = nrBilete;
        }

        public Agentie Agentie { get; set; }
        public Excursie Excursie { get; set; }
        public string NumeClient { get; set; }
        public string Telefon { get; set; }
        public int NrBilete { get; set; }
        public string Id { get; set; }
    }
}