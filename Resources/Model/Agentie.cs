using System;

namespace ServerTurism.Model
{
    [Serializable]
    public class Agentie : IHasId<string>
    {
        public Agentie(string id, string password)
        {
            Id = id;
            Password = password;
        }

        public string Password { get; set; }
        public string Id { get; set; }
    }
}