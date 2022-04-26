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
        private IDbConnection _connection { get; set; } = null;
        private IDbTransaction _transaction = null;

        public UnitOfWork(IRecipeContext recipeContext)
        {
            _connection = recipeContext.CreateConnection();
        }

        public void BeginTransaction()
        {
            //TODO: Maybe move this to constructor?
            if (_connection.State == ConnectionState.Closed)
                _connection.Open();

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

            //TODO: should we close the connection here?
            if (_connection != null)
                _connection.Dispose();
        }

        public async Task<IEnumerable<T>> ExecuteQueryAsync<T, U>(string sqlQuery, U parameters)
        {
            if (EqualityComparer<U>.Default.Equals(parameters, default(U)))
                return await _connection.QueryAsync<T>(sqlQuery, transaction: _transaction);

            return await _connection.QueryAsync<T>(sqlQuery, parameters, transaction: _transaction);
        }

        public async Task ExecuteQueryAsync<T>(string sqlQuery, T parameters)
        {
            if (EqualityComparer<T>.Default.Equals(parameters, default(T)))
                await _connection.ExecuteAsync(sqlQuery, transaction: _transaction);

            await _connection.ExecuteAsync(sqlQuery, parameters, transaction: _transaction);
        }

        public async Task<IEnumerable<T>> LoadData<T, U>(string storedProcedure, U parameters)
        {
            return await _connection.QueryAsync<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure, transaction: _transaction);
        }

        public async Task SaveData<U>(string storedProcedure, U parameters)
        {
            await _connection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure, transaction: _transaction);
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
