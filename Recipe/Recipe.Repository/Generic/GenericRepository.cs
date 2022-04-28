using Dapper;
using Recipe.Repository.Common.Generic;
using Recipe.Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Recipe.Repository.Generic
{
    internal class GenericRepository<T> : IGenericRepository<T>
    {
        private readonly IDbTransaction _transaction;
        private IDbConnection _connection => _transaction.Connection;

        private string TableName { get; set; }

        public GenericRepository(IDbTransaction transaction)
        {
            _transaction = transaction;

            //TODO: remove first char I if it is an interface
            TableName = typeof(T).Name;
        }

        public async Task CreateAsync(T entity)
        {
            try
            {
                var columns = GetColumns();
                var stringOfColumns = string.Join(", ", columns);
                var stringOfParameters = string.Join(", ", columns.Select(e => "@" + e));
                var query = $"INSERT INTO {TableName} ({stringOfColumns}) values ({stringOfParameters})";

                await ExecuteQueryAsync(query, entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task CreateAsync(IEnumerable<T> entityList)
        {
            try
            {
                foreach (T entity in entityList)
                {
                    //TODO: this wont get values for every entity??
                    var columns = GetColumns();
                    var stringOfColumns = string.Join(", ", columns);
                    var stringOfParameters = string.Join(", ", columns.Select(e => "@" + e));
                    var query = $"INSERT INTO {TableName} ({stringOfColumns}) values ({stringOfParameters})";

                    await ExecuteQueryAsync(query, entity);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task DeleteAsync(T entity)
        {
            try
            {
                var query = $"DELETE FROM {TableName} WHERE {TableName}ID = @{TableName}ID";

                await ExecuteQueryAsync(query, entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<T> GetByIdAsync(int id)
        {
            try
            {
                IEnumerable<T> entities = await GetAllAsync($"WHERE {TableName}ID = {id};");

                return entities.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync(string where = null)
        {
            try
            {
                IEnumerable<T> entityList;

                var query = $"SELECT * FROM {TableName} ";

                if (!string.IsNullOrWhiteSpace(where))
                    query += where;

                entityList = await ExecuteQueryWithReturnAsync(query, (T)new object { }); //TODO: check if this works

                return entityList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task UpdateAsync(T entity)
        {
            try
            {
                var columns = GetColumns();
                var stringOfColumns = string.Join(", ", columns.Select(e => $"{e} = @{e}"));
                var query = $"UPDATE {TableName} SET {stringOfColumns} WHERE @{TableName}ID = @{TableName}ID";

                await ExecuteQueryAsync(query, entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<T>> ExecuteQueryWithReturnAsync(string sqlQuery, T entity)
        {
            if (EqualityComparer<T>.Default.Equals(entity, default(T))) //TODO: check if this works
                return await _connection.QueryAsync<T>(sqlQuery, transaction: _transaction);

            return await _connection.QueryAsync<T>(sqlQuery, entity, transaction: _transaction);
        }

        public async Task ExecuteQueryAsync(string sqlQuery, T entity)
        {
            if (EqualityComparer<T>.Default.Equals(entity, default(T))) //TODO: check if this works
                await _connection.ExecuteAsync(sqlQuery, transaction: _transaction);

            await _connection.ExecuteAsync(sqlQuery, entity, transaction: _transaction);
        }

        public async Task<IEnumerable<T>> ExecuteStoredProcedureWithReturnAsync(string storedProcedure, T entity)
        {
            return await _connection.QueryAsync<T>(storedProcedure, entity, commandType: CommandType.StoredProcedure, transaction: _transaction);
        }

        public async Task ExecuteStoredProcedureAsync(string storedProcedure, T entity)
        {
            await _connection.ExecuteAsync(storedProcedure, entity, commandType: CommandType.StoredProcedure, transaction: _transaction);
        }

        private IEnumerable<string> GetColumns()
        {
            return typeof(T)
                    .GetProperties()
                    .Where(e => !e.Name.ToLower().Contains("id") && !e.PropertyType.GetTypeInfo().IsGenericType)
                    .Select(e => e.Name);
        }
    }
}
