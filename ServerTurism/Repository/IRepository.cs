using System.Collections.Generic;
using ServerTurism.Model;

namespace ServerTurism.Repository
{
    public interface IRepository<T, ID> where T : IHasId<ID>
    {
        void Add(T entity);
        void Update(T entity);
        void Remove(ID id);
        List<T> FindAll();
        T FindOne(ID id);
    }
}