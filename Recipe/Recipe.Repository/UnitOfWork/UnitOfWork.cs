using Dapper;
using Nito.AsyncEx;
using Recipe.DAL;
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

        public UnitOfWork(IRecipeContext recipeContext)
        {
            _connection = recipeContext.CreateConnection();
        }

        private async Task BeginTransactionAsync()
        {
            if (_connection.State == ConnectionState.Closed)
                await _connection.OpenAsync();

            if (_transaction == null)
                _transaction = await _connection.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            await _transaction.CommitAsync();
            await DisposeAsync();
        }

        public async Task RollbackAsync()
        {
            await _transaction.RollbackAsync();
            await DisposeAsync();
        }

        public async Task DisposeAsync()
        {
            if (_transaction != null)
                await _transaction.DisposeAsync();
            _transaction = null;

            if (_connection != null)
                await _connection.DisposeAsync();
        }

        public IGenericRepository<T> Repository<T>()
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
