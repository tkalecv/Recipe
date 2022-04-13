﻿using Recipe.Repository.Common.Generic;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Recipe.Repository.UnitOfWork
{
    public interface IUnitOfWork
    {
        IDbConnection Connection { get; set; }
        void BeginTransaction();
        void Commit();
        void Dispose();
        Task<IEnumerable<T>> ExecuteQueryAsync<T, U>(string sqlQuery, U parameters);
        Task ExecuteQueryAsync<T>(string sqlQuery, T parameters);
        IGenericRepository<T> Repository<T>();
        void Rollback();
    }
}