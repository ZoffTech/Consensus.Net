using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace consensus.net.core.Repository
{
    public abstract class BaseRepository<T> : IRepository<T>
    {
        
        public Task<int> Add(T param)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> Delete()
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<T>> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public Task<T> GetById()
        {
            throw new System.NotImplementedException();
        }

        public Task<IQueryable> Query()
        {
            throw new System.NotImplementedException();
        }

        public Task<int> Update(T value)
        {
            throw new System.NotImplementedException();
        }
    }
}