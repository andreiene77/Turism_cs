using ServerTurism.Model;
using ServerTurism.Repository;

namespace ServerTurism.Service
{
    public class LoginService
    {
        private readonly IRepository<Agentie, string> repo;

        public LoginService(IRepository<Agentie, string> repo) => this.repo = repo;

        public Agentie Login_user(string username, string password)
        {
            var user = repo.FindOne(username);
            if (user is null)
                return null;
            if (user.Password == password)
                return user;
            return null;
        }
    }
}