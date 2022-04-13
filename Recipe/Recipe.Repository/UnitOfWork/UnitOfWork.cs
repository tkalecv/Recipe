using Dapper;
using Recipe.DAL;
using Recipe.Repository.Common;
using Recipe.Repository.Common.Generic;
using Recipe.Repository.Generic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Recipe.Repository.UnitOfWork
{
    internal class UnitOfWork : IUnitOfWork
    {
        IDbConnection _connection = null;
        IDbTransaction _transaction = null;

        private Dictionary<string, dynamic> _repositories;

        public UnitOfWork(IRecipeContext recipeContext)
        {
            _connection = recipeContext.CreateConnection();
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

        public async Task<IEnumerable<T>> ExecuteQueryAsync<T, U>(string sqlQuery, U parameters)
        {
            //TODO: check if generic is null or empty object here
            if (ReferenceEquals(parameters, null))
                return await _connection.QueryAsync<T>(sqlQuery);

            return await _connection.QueryAsync<T>(sqlQuery, parameters);
        }

        public async Task ExecuteQueryAsync<T>(string sqlQuery, T parameters)
        {
            //TODO: check if generic is null or empty object here
            if (ReferenceEquals(parameters, null))
                await _connection.ExecuteAsync(sqlQuery);

            await _connection.ExecuteAsync(sqlQuery, parameters);
        }

        //TODO: move this to repo?
        public async Task<IEnumerable<T>> LoadData<T, U>(string storedProcedure, U parameters)
        {
            using IDbConnection dbConnection = _connection;

            return await dbConnection.QueryAsync<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
        }

        //TODO: move this to repo?
        public async Task SaveData<T>(string storedProcedure, T parameters)
        {
            using IDbConnection dbConnection = _connection;

            await dbConnection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
        }

        //TODO: move this to factory?
        public IGenericRepository<T> Repository<T>()
        {
            if (_repositories == null)
                _repositories = new Dictionary<string, dynamic>();
            var type = typeof(T).Name;
            if (_repositories.ContainsKey(type))
                return (IGenericRepository<T>)_repositories[type];
            var repositoryType = typeof(GenericRepository<>);
            _repositories.Add(type, Activator.CreateInstance(
                repositoryType.MakeGenericType(typeof(T)), this)
            );
            return _repositories[type];
        }
    }
}
