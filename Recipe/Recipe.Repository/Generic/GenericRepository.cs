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

        /// <summary>
        /// Method asynchronously asynchronously executes SQL INSERT query with parameters and inserts rows in table
        /// </summary>
        /// <param name="entity">Object with values that will be passed as parameter values in SQL DELETE query</param>
        /// <returns>Task</returns>
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

        /// <summary>
        /// Method asynchronously executes SQL INSERT query with parameters and inserts multiple rows in table
        /// </summary>
        /// <param name="entityList">List of objects with values that will be passed as parameter values in SQL INSERT query</param>
        /// <returns>Task</returns>
        public async Task CreateAsync(IEnumerable<T> entityList)
        {
            try
            {
                var columns = GetColumns();

                foreach (T entity in entityList)
                {
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

        /// <summary>
        /// Method asynchronously executes SQL DELETE query with parameters and deletes rows in table
        /// </summary>
        /// <param name="entity">Object with values that will be passed as parameter values in SQL DELETE query</param>
        /// <returns>Task</returns>
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

        /// <summary>
        /// Method asynchronously retrieves rows from SQL table filtered with primary key
        /// </summary>
        /// <param name="id">SQL table row id (Primary Key)</param>
        /// <returns>Task<T></returns>
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

        /// <summary>
        /// Method asynchronously executes retrieve rows from SQL table with or without WHERE filter
        /// </summary>
        /// <param name="where">SQL WHERE filter that extends default SELECT query</param>
        /// <returns>Task<IEnumerable<T>></returns>
        public async Task<IEnumerable<T>> GetAllAsync(string where = null)
        {
            try
            {
                IEnumerable<T> entityList;

                var query = $"SELECT * FROM {TableName} ";

                if (!string.IsNullOrWhiteSpace(where))
                    query += where;

                entityList = await ExecuteQueryWithReturnAsync(query, default(T));

                return entityList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method asynchronously executes SQL UPDATE query with parameters and updates rows in table
        /// </summary>
        /// <param name="entity">Object with values that will be passed as parameter values in SQL UPDATE query</param>
        /// <returns>Task</returns>
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

        /// <summary>
        /// Method asynchronously executes SQL query with return data
        /// </summary>
        /// <param name="sqlQuery">SQL query</param>
        /// <param name="entity">Object with values that will be passed as parameter values in SQL query</param>
        /// <returns>Task<IEnumerable<T>></returns>
        public async Task<IEnumerable<T>> ExecuteQueryWithReturnAsync(string sqlQuery, T entity)
        {
            if (EqualityComparer<T>.Default.Equals(entity, default(T)))
                return await _connection.QueryAsync<T>(sqlQuery, transaction: _transaction);

            return await _connection.QueryAsync<T>(sqlQuery, entity, transaction: _transaction);
        }

        /// <summary>
        /// Method asynchronously executes SQL query with no return data
        /// </summary>
        /// <param name="sqlQuery">SQL query</param>
        /// <param name="entity">Object with values that will be passed as parameter values in SQL query</param>
        /// <returns>Task</returns>
        public async Task ExecuteQueryAsync(string sqlQuery, T entity)
        {
            if (EqualityComparer<T>.Default.Equals(entity, default(T)))
                await _connection.ExecuteAsync(sqlQuery, transaction: _transaction);

            await _connection.ExecuteAsync(sqlQuery, entity, transaction: _transaction);
        }

        /// <summary>
        /// Method asynchronously executes SQL query with return data
        /// </summary>
        /// <param name="storedProcedure">Name of the SQL stored procedure in your database</param>
        /// <param name="entity">Object with values that will be passed as parameter values in SQL stored procedure</param>
        /// <returns>Task<IEnumerable<T>></returns>
        public async Task<IEnumerable<T>> ExecuteStoredProcedureWithReturnAsync(string storedProcedure, T entity)
        {
            return await _connection.QueryAsync<T>(storedProcedure, entity, commandType: CommandType.StoredProcedure, transaction: _transaction);
        }

        /// <summary>
        /// Method asynchronously executes SQL stored procedure with no return data
        /// </summary>
        /// <param name="storedProcedure">Name of the SQL stored procedure in your database</param>
        /// <param name="entity">Object with values that will be passed as parameter values in SQL stored procedure</param>
        /// <returns>Task</returns>
        public async Task ExecuteStoredProcedureAsync(string storedProcedure, T entity)
        {
            await _connection.ExecuteAsync(storedProcedure, entity, commandType: CommandType.StoredProcedure, transaction: _transaction);
        }

        /// <summary>
        /// Method gets entity properties names as string. They are used as SQL query parameters
        /// </summary>
        /// <returns>IEnumerable<string></returns>
        private IEnumerable<string> GetColumns()
        {
            return typeof(T)
                    .GetProperties()
                    .Where(e => !e.Name.ToLower().Contains("id") && !e.PropertyType.GetTypeInfo().IsGenericType)
                    .Select(e => e.Name);
        }
    }
}
