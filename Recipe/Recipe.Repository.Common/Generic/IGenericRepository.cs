using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipe.Repository.Common.Generic
{
    public interface IGenericRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync(string where = null);
        Task CreateAsync(T entity);
        Task CreateAsync(IEnumerable<T> entityList);
        Task DeleteAsync(T entity);
        Task UpdateAsync(T entity);
    }
}
