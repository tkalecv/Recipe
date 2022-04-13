using Recipe.Repository.Common;
using Recipe.Repository.Common.Generic;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Recipe.Repository.UnitOfWork
{
    internal interface IUnitOfWork : IDisposable
    {
        void BeginTransaction();
        void Commit();
        void Rollback();
        Task<IEnumerable<T>> ExecuteQueryAsync<T, U>(string sqlQuery, U parameters);
        Task ExecuteQueryAsync<T>(string sqlQuery, T parameters);

        Task<IEnumerable<T>> LoadData<T, U>(string storedProcedure, U parameters);
        Task SaveData<T>(string storedProcedure, T parameters);

        IGenericRepository<T> Repository<T>();
    }
}
