using System.Collections.Generic;
using System.Threading.Tasks;

namespace Recipe.Repository.Common.Generic
{
    public interface IGenericRepository<T>
    {
        Task CreateAsync(IEnumerable<T> entityList);
        Task CreateAsync(T entity);
        Task DeleteAsync(T entity);
        Task ExecuteQueryAsync(string sqlQuery, T entity);
        Task<IEnumerable<T>> ExecuteQueryWithReturnAsync(string sqlQuery, T entity);
        Task ExecuteStoredProcedureAsync(string storedProcedure, T entity);
        Task<IEnumerable<T>> ExecuteStoredProcedureWithReturnAsync(string storedProcedure, T entity);
        Task<IEnumerable<T>> GetAllAsync(string where = null);
        Task<T> GetByIdAsync(int id);
        Task UpdateAsync(T entity);
    }
}
