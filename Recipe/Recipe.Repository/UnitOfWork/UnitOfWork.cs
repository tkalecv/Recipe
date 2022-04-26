using Dapper;
using Recipe.DAL;
using Recipe.Repository.Common.Generic;
using Recipe.Repository.Generic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Recipe.Repository.UnitOfWork
{
    internal class UnitOfWork : IUnitOfWork
    {
        public IDbConnection Connection { get; set; } = null;
        private IDbTransaction _transaction = null;

        private Dictionary<string, dynamic> _repositories;

        public UnitOfWork(IRecipeContext recipeContext)
        {
            Connection = recipeContext.CreateConnection();
        }

        public void BeginTransaction()
        {
            if (Connection.State == ConnectionState.Closed)
                Connection.Open();

            _transaction = Connection.BeginTransaction();
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

        public async Task<IEnumerable<T>> ExecuteQueryAsync<T, U>(string sqlQuery, U parameters)
        {
            //TODO: check if generic is null or empty object here
            if (ReferenceEquals(parameters, null))
                return await Connection.QueryAsync<T>(sqlQuery, transaction: _transaction);

            return await Connection.QueryAsync<T>(sqlQuery, parameters, transaction: _transaction);
        }

        public async Task ExecuteQueryAsync<T>(string sqlQuery, T parameters)
        {
            //TODO: check if generic is null or empty object here
            if (ReferenceEquals(parameters, null))
                await Connection.ExecuteAsync(sqlQuery, transaction: _transaction);

            await Connection.ExecuteAsync(sqlQuery, parameters, transaction: _transaction);
        }

        //TODO: move this to factory?
        //public IGenericRepository<T> Repository<T>()
        //{
        //    if (_repositories == null)
        //        _repositories = new Dictionary<string, dynamic>();
        //    var type = typeof(T).Name;
        //    if (_repositories.ContainsKey(type))
        //        return (IGenericRepository<T>)_repositories[type];
        //    var repositoryType = typeof(GenericRepository<>);
        //    _repositories.Add(type, Activator.CreateInstance(
        //        repositoryType.MakeGenericType(typeof(T)), this)
        //    );
        //    return _repositories[type];
        //}
    }
}
