using AutoMapper;
using Dapper;
using Recipe.DAL.Scripts;
using Recipe.Models.Common;
using Recipe.Repository.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Recipe.DAL.Scripts.ScriptReferences;

namespace Recipe.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IDbTransaction _transaction;
        private IDbConnection _connection => _transaction.Connection;

        public CategoryRepository(IDbTransaction transaction)
        {
            _transaction = transaction;
        }

        /// <summary>
        /// Method asynchronously inserts Category object in table. Number of affected rows is returned
        /// </summary>
        /// <param name="category">Object with values that will be passed as parameter values</param>
        /// <returns>Task<int></returns>
        public async Task<int> CreateAsync(ICategory category)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters(
                    new
                    {
                        Name = category.Name
                    });

                return await _connection.ExecuteAsync(ScriptReferences.Category.SP_CreateCategory,
                    param: parameters,
                    transaction: _transaction,
                    commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method asynchronously inserts multiple Category objects in table. Number of affected rows is returned
        /// </summary>
        /// <param name="categoryList">List of objects with values that will be passed as parameter values</param>
        /// <returns>Task<int></returns>
        public async Task<int> CreateAsync(IEnumerable<ICategory> categoryList)
        {
            try
            {
                int rowNumber = 0;

                foreach (ICategory category in categoryList)
                {
                    DynamicParameters parameters = new DynamicParameters(
                        new
                        {
                            Name = category.Name
                        });

                    rowNumber += await _connection.ExecuteAsync(ScriptReferences.Category.SP_CreateCategory,
                        param: parameters,
                        transaction: _transaction,
                        commandType: CommandType.StoredProcedure);
                }
                return rowNumber;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method asynchronously deletes Category object from table. Number of affected rows is returned
        /// </summary>
        /// <param name="categoryId">Category id (Primary Key)</param>
        /// <returns>Task<int></returns>
        public async Task<int> DeleteAsync(int? categoryId)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();

                if (categoryId != null)
                    parameters.AddDynamicParams(new { CategoryID = categoryId });

                return await _connection.ExecuteAsync(ScriptReferences.Category.SP_DeleteCategory,
                    param: parameters,
                    transaction: _transaction,
                    commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method asynchronously retrieve all Categories from SQL table with or without WHERE filter
        /// </summary>
        /// <param name="categoryId">Category id (Primary Key)</param>
        /// <returns>Task<IEnumerable<ICategory>></returns>
        public async Task<IEnumerable<ICategory>> GetAllAsync(int? categoryId)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                if (categoryId != null)
                    parameters.AddDynamicParams(new { CategoryID = categoryId });

                var categories = await _connection.QueryAsync<Models.Category>(ScriptReferences.Category.SP_RetrieveCategory,
                    param: parameters,
                    commandType: CommandType.StoredProcedure,
                    transaction: _transaction);

                return categories;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method asynchronously updates Category object in table. Number of affected rows is returned
        /// </summary>
        /// <param name="categoryId">Category id (Primary Key)</param>
        /// <returns>Task<int></returns>
        public async Task<int> UpdateAsync(int categoryId, ICategory category)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters(
                    new
                    {
                        CategoryID = categoryId,
                        Name = category.Name,
                    });

                return await _connection.ExecuteAsync(ScriptReferences.Category.SP_UpdateCategory,
                    param: parameters,
                    transaction: _transaction,
                    commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
