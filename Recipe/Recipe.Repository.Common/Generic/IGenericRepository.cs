using System.Collections.Generic;
using System.Threading.Tasks;

namespace Recipe.Repository.Common.Generic
{
    public interface IGenericRepository<T>
    {
        Task CreateAsync(IEnumerable<T> entityList);
        Task CreateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<IEnumerable<T>> GetAllAsync(string where = null);
        Task<IEnumerable<K>> LoadData<K, U>(string storedProcedure, U parameters);
        Task SaveData<U>(string storedProcedure, U parameters);
        Task UpdateAsync(T entity);
    }
}
