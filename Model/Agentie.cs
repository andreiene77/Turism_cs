namespace Turism_cs
{
    public class Agentie : IHasID<string>
    {
        public string Id { get; set; }
        public string Password { get; set; }

        public Agentie(string id, string password)
        {
            Id = id;
            Password = password;
        }
    }
}
