using Recipe.Repository.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Transactions;

namespace Recipe.Repository.UnitOfWork
{
    internal class UnitOfWork : IUnitOfWork
    {
        IDbConnection _connection = null;
        IDbTransaction _transaction = null;

        private Dictionary<string, dynamic> _repositories;

        public UnitOfWork(IDbConnection connection)
        {
            _connection = connection;
        }

        public void BeginTransaction()
        {
            _transaction = _connection.BeginTransaction();
        }

        public void Commit()
        {
            _transaction.Commit();
            Dispose();
        }

        public void Rollback()
        {
            _transaction.Rollback();
            Dispose();
        }

        public void Dispose()
        {
            if (_transaction != null)
                _transaction.Dispose();
            _transaction = null;
        }

        public IGenericRepository<TEntity> Repository<TEntity>()
        {
            if (_repositories == null)
                _repositories = new Dictionary<string, dynamic>();
            var type = typeof(TEntity).Name;
            if (_repositories.ContainsKey(type))
                return (IGenericRepository<TEntity>)_repositories[type];
            var repositoryType = typeof(GenericRepository<>);
            _repositories.Add(type, Activator.CreateInstance(
                repositoryType.MakeGenericType(typeof(TEntity)), this)
            );
            return _repositories[type];
        }
    }
}
