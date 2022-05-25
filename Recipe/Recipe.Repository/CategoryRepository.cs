using AutoMapper;
using Dapper;
using Recipe.Models.Common;
using Recipe.Repository.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                DynamicParameters parameters = new DynamicParameters();
                parameters.AddDynamicParams(category);

                return await _connection.ExecuteAsync("SP name here",
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
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.AddDynamicParams(category);

                    rowNumber += await _connection.ExecuteAsync("SP name here",
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
        /// <param name="category">Object with values that will be passed as parameter values</param>
        /// <returns>Task<int></returns>
        public async Task<int> DeleteAsync(ICategory category)
        {
            try
            {
                return await _connection.ExecuteAsync("SP name here",
                    param: category,
                    transaction: _transaction,
                    commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method asynchronously retrieves Category from table filtered with primary key
        /// </summary>
        /// <param name="id">Category id (Primary Key)</param>
        /// <returns>Task<ICategory></returns>
        public async Task<ICategory> GetByIdAsync(int id)
        {
            try
            {
                var categories = await _connection.QueryAsync<ICategory, ISubcategory, ICategory>("SP name here",
                    (category, subcat) =>
                    {
                        category.Subcategories.Add(subcat);
                        return category;
                    },
                    param: new { id = id },
                    commandType: CommandType.StoredProcedure,
                    splitOn: "subcategoryid",
                    transaction: _transaction);

                return categories.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method asynchronously retrieve all Categories from SQL table with or without WHERE filter
        /// </summary>
        /// <param name="where">SQL WHERE filter that extends default SELECT query</param>
        /// <returns>Task<IEnumerable<ICategory>></returns>
        public async Task<IEnumerable<ICategory>> GetAllAsync(string where = null)
        {
            try
            {
                var categories = await _connection.QueryAsync<ICategory, ISubcategory, ICategory>("SP name here",
                    (category, subcat) =>
                    {
                        category.Subcategories.Add(subcat);
                        return category;
                    },
                    param: new { where = where },
                    commandType: CommandType.StoredProcedure,
                    splitOn: "subcategoryid",
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
        /// <param name="category">Object with values that will be passed as parameter values</param>
        /// <returns>Task<int></returns>
        public async Task<int> UpdateAsync(ICategory category)
        {
            try
            {
                return await _connection.ExecuteAsync("SP name here",
                    param: category,
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
