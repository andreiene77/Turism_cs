using System;

namespace ServerTurism.Repository
{
    public class RepositoryException : Exception
    {
        public RepositoryException(string message) : base(message)
        {
        }
    }
}