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
        public IDbTransaction Transaction { get; }
        public IDbConnection Connection => Transaction.Connection;

        private string TableName { get; set; }

        public GenericRepository(IDbTransaction transaction)
        {
            Transaction = transaction;

            //TODO: remove first char I if it is an interface
            TableName = typeof(T).Name;
        }

        /// <summary>
        /// Method asynchronously executes SQL INSERT query with parameters and inserts rows in table. Number of affected rows is returned
        /// </summary>
        /// <param name="entity">Object with values that will be passed as parameter values in SQL INSERT query</param>
        /// <returns>Task<int></returns>
        public async Task<int> CreateAsync(T entity)
        {
            try
            {
                var columns = GetColumns();
                var stringOfColumns = string.Join(", ", columns);
                var stringOfParameters = string.Join(", ", columns.Select(e => "@" + e));
                var query = $"INSERT INTO {TableName} ({stringOfColumns}) values ({stringOfParameters})";

                return await ExecuteQueryAsync(query, entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method asynchronously executes SQL INSERT query with parameters and inserts multiple rows in table. Number of affected rows is returned
        /// </summary>
        /// <param name="entityList">List of objects with values that will be passed as parameter values in SQL INSERT query</param>
        /// <returns>Task<int></returns>
        public async Task<int> CreateAsync(IEnumerable<T> entityList)
        {
            try
            {
                var columns = GetColumns();
                int rowNumber = 0;

                foreach (T entity in entityList)
                {
                    var stringOfColumns = string.Join(", ", columns);
                    var stringOfParameters = string.Join(", ", columns.Select(e => "@" + e));
                    string query = $"INSERT INTO {TableName} ({stringOfColumns}) values ({stringOfParameters})";

                    rowNumber += await ExecuteQueryAsync(query, entity);
                }
                return rowNumber;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method asynchronously executes SQL DELETE query with parameters and deletes rows in table. Number of affected rows is returned
        /// </summary>
        /// <param name="entity">Object with values that will be passed as parameter values in SQL DELETE query</param>
        /// <returns>Task<int></returns>
        public async Task<int> DeleteAsync(T entity)
        {
            try
            {
                var query = $"DELETE FROM {TableName} WHERE {TableName}ID = @{TableName}ID";

                return await ExecuteQueryAsync(query, entity);
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
        /// Method asynchronously retrieve rows from SQL table with or without WHERE filter
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
        /// Method asynchronously executes SQL UPDATE query with parameters and updates rows in table. Number of affected rows is returned
        /// </summary>
        /// <param name="entity">Object with values that will be passed as parameter values in SQL UPDATE query</param>
        /// <returns>Task<int></returns>
        public async Task<int> UpdateAsync(T entity)
        {
            try
            {
                var columns = GetColumns();
                var stringOfColumns = string.Join(", ", columns.Select(e => $"{e} = @{e}"));
                var query = $"UPDATE {TableName} SET {stringOfColumns} WHERE @{TableName}ID = @{TableName}ID";

                return await ExecuteQueryAsync(query, entity);
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
                return await Connection.QueryAsync<T>(sqlQuery, transaction: Transaction);

            return await Connection.QueryAsync<T>(sqlQuery, entity, transaction: Transaction);
        }

        /// <summary>
        /// Method asynchronously executes SQL query. Number of affected rows is returned
        /// </summary>
        /// <param name="sqlQuery">SQL query</param>
        /// <param name="entity">Object with values that will be passed as parameter values in SQL query</param>
        /// <returns>Task<int></returns>
        public async Task<int> ExecuteQueryAsync(string sqlQuery, T entity)
        {
            if (EqualityComparer<T>.Default.Equals(entity, default(T)))
                return await Connection.ExecuteAsync(sqlQuery, transaction: Transaction);

            return await Connection.ExecuteAsync(sqlQuery, entity, transaction: Transaction);
        }

        /// <summary>
        /// Method asynchronously executes SQL query with return data
        /// </summary>
        /// <param name="storedProcedure">Name of the SQL stored procedure in your database</param>
        /// <param name="entity">Object with values that will be passed as parameter values in SQL stored procedure</param>
        /// <returns>Task<IEnumerable<T>></returns>
        public async Task<IEnumerable<T>> ExecuteStoredProcedureWithReturnAsync(string storedProcedure, T entity)
        {
            return await Connection.QueryAsync<T>(storedProcedure, entity, commandType: CommandType.StoredProcedure, transaction: Transaction);
        }

        /// <summary>
        /// Method asynchronously executes SQL stored procedure. Number of affected rows is returned
        /// </summary>
        /// <param name="storedProcedure">Name of the SQL stored procedure in your database</param>
        /// <param name="entity">Object with values that will be passed as parameter values in SQL stored procedure</param>
        /// <returns>Task<int></returns>
        public async Task<int> ExecuteStoredProcedureAsync(string storedProcedure, T entity)
        {
            return await Connection.ExecuteAsync(storedProcedure, entity, commandType: CommandType.StoredProcedure, transaction: Transaction);
        }

        /// <summary>
        /// Method gets entity properties names as string. They are used as SQL query parameters
        /// </summary>
        /// <returns>IEnumerable<string></returns>
        private IEnumerable<string> GetColumns()
        {
            return typeof(T)
                    .GetProperties()
                    .Where(e => !e.Name.ToLower().Equals($"{TableName.ToLower()}id")
                                && !e.PropertyType.GetTypeInfo().IsGenericType)
                    .Select(e => e.Name);
        }
    }
}
