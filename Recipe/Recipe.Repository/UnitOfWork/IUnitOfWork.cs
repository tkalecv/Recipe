using Recipe.Repository.Common.Generic;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Recipe.Repository.UnitOfWork
{
    internal interface IUnitOfWork
    {
        IDbConnection Connection { get; set; }
        void BeginTransaction();
        void Commit();
        void Dispose();
        Task<IEnumerable<T>> ExecuteQueryAsync<T, U>(string sqlQuery, U parameters);
        Task ExecuteQueryAsync<T>(string sqlQuery, T parameters);
        Task<IEnumerable<T>> LoadData<T, U>(string storedProcedure, U parameters);
        Task SaveData<U>(string storedProcedure, U parameters);
        void Rollback();
    }
}