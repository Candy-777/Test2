using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IBaseRepository<T>
    {
        public Task<int> Add(T entity);
        public Task<T> Get(int id);
        public Task<IEnumerable<T>> GetAll();
        public Task<bool> Delete (int id);
        public Task<bool> Update (T entity);

    }
}
