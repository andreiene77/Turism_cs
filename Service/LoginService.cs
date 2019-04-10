using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turism_cs.Repository;

namespace Turism_cs.Service
{
    public class LoginService
    {
        private IRepository<Agentie, string> repo;

        public LoginService(IRepository<Agentie, string> repo) => this.repo = repo;

        public Agentie Login_user(string username, string password)
        {
            Agentie user = repo.FindOne(username);
            if (user is null)
                return null;
            if (user.Password == password)
                return user;
            return null;
        }
    }
}
