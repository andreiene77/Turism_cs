using System.Collections.Generic;

namespace Turism_cs.Repository
{
    public interface IRepository<T, ID> where T : IHasID<ID>
    {
        void Add(T entity);
        void Update(T entity);
        void Remove(ID id);
        List<T> FindAll();
        T FindOne(ID id);
    }
}
