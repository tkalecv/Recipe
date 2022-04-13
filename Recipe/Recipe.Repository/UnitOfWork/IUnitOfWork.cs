using Recipe.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Recipe.Repository.UnitOfWork
{
    internal interface IUnitOfWork : IDisposable
    {
        void BeginTransaction();
        void Commit();
        void Rollback();

        IGenericRepository<TEntity> Repository<TEntity>();
    }
}
