using Autofac;
using Dapper;
using Nito.AsyncEx;
using Recipe.DAL;
using Recipe.Repository.Common;
using Recipe.Repository.Common.Generic;
using Recipe.Repository.Generic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

namespace Recipe.Repository.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private DbConnection _connection { get; set; } = null;
        private DbTransaction _transaction { get; set; } = null;

        Dictionary<string, dynamic> _repositories { get; set; }

        #region Repositories
        private IRecipeRepository _recipeRepository;
        public IRecipeRepository RecipeRepository
        {
            get
            {
                if (_recipeRepository == null)
                    _recipeRepository = new RecipeRepository(_transaction);

                return _recipeRepository;
            }
        }
        #endregion

        public UnitOfWork(IRecipeContext recipeContext)
        {
            _connection = recipeContext.CreateConnection();
        }

        /// <summary>
        /// Method asynchronously opens a database connection and begins a database transaction
        /// </summary>
        /// <returns>Task</returns>
        public async Task BeginTransactionAsync()
        {
            if (_connection.State == ConnectionState.Closed)
                await _connection.OpenAsync();

            if (_transaction == null)
                _transaction = await _connection.BeginTransactionAsync();
        }

        /// <summary>
        /// Method asynchronously commits the database transaction and disposes the transaction and connection objects
        /// </summary>
        /// <returns>Task</returns>
        public async Task CommitAsync()
        {
            await _transaction.CommitAsync();
            await DisposeAsync();
        }

        /// <summary>
        /// Method asynchronously rolls back a transaction from a pending state
        /// </summary>
        /// <returns>Task</returns>
        public async Task RollbackAsync()
        {
            await _transaction.RollbackAsync();
            await DisposeAsync();
        }

        /// <summary>
        /// Method asynchronously disposes the transaction and connection objects
        /// </summary>
        /// <returns>Task</returns>
        public async Task DisposeAsync()
        {
            if (_transaction != null)
                await _transaction.DisposeAsync();
            _transaction = null;

            if (_connection != null)
                await _connection.DisposeAsync();
        }

        /// <summary>
        /// Method initializes GenericRepository
        /// </summary>
        /// <typeparam name="T">Object used to initialize GenericRepository</typeparam>
        /// <returns>IGenericRepository<T></returns>
        public IGenericRepository<T> Repository<T>() //TODO: remove this or edit to use for all repos
        {
            AsyncContext.Run(async () => await BeginTransactionAsync());

            if (_repositories == null)
                _repositories = new Dictionary<string, dynamic>();

            var type = typeof(T).Name;

            if (_repositories.ContainsKey(type))
                return (IGenericRepository<T>)_repositories[type];

            var repositoryType = typeof(GenericRepository<>);

            _repositories.Add(type, Activator.CreateInstance(
                repositoryType.MakeGenericType(typeof(T)), _transaction));

            return _repositories[type];
        }
    }
}
