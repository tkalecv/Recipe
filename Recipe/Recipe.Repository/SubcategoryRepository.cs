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

namespace Recipe.Repository
{
    public class SubcategoryRepository : ISubcategoryRepository
    {
        private readonly IDbTransaction _transaction;
        private IDbConnection _connection => _transaction.Connection;

        public SubcategoryRepository(IDbTransaction transaction)
        {
            _transaction = transaction;
        }

        /// <summary>
        /// Method asynchronously inserts Subcategory object in table. Number of affected rows is returned
        /// </summary>
        /// <param name="subcategory">Object with values that will be passed as parameter values</param>
        /// <returns>Task<int></returns>
        public async Task<int> CreateAsync(ISubcategory subcategory)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters(
                    new
                    {
                        Name = subcategory.Name,
                        CategoryID = subcategory.Category.CategoryID
                    });

                return await _connection.ExecuteAsync(ScriptReferences.Subcategory.SP_CreateSubcategory,
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
        /// Method asynchronously inserts multiple Subcategory objects in table. Number of affected rows is returned
        /// </summary>
        /// <param name="subcategoryList">List of objects with values that will be passed as parameter values</param>
        /// <returns>Task<int></returns>
        public async Task<int> CreateAsync(IEnumerable<ISubcategory> subcategoryList)
        {
            try
            {
                int rowNumber = 0;

                foreach (ISubcategory subcategory in subcategoryList)
                {
                    DynamicParameters parameters = new DynamicParameters(
                        new
                        {
                            Name = subcategory.Name,
                            CategoryID = subcategory.Category.CategoryID
                        });

                    rowNumber += await _connection.ExecuteAsync(ScriptReferences.Subcategory.SP_CreateSubcategory,
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
        /// Method asynchronously deletes Subcategory object from table. Number of affected rows is returned
        /// </summary>
        /// <param name="subcategory">Object with values that will be passed as parameter values</param>
        /// <returns>Task<int></returns>
        public async Task<int> DeleteAsync(ISubcategory subcategory)
        {
            try
            {
                return await _connection.ExecuteAsync("SP name here",
                    param: subcategory,
                    transaction: _transaction,
                    commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method asynchronously retrieves Subcategory from table filtered with primary key
        /// </summary>
        /// <param name="id">Subcategory id (Primary Key)</param>
        /// <returns>Task<ISubcategory></returns>
        public async Task<ISubcategory> GetByIdAsync(int id)
        {
            try
            {
                var Subcategorys = await _connection.QueryAsync<ISubcategory, ICategory, ISubcategory>(ScriptReferences.Subcategory.SP_RetrieveSubcategory,
                    (subcategory, category) =>
                    {
                        subcategory.Category = category;
                        return subcategory;
                    },
                    param: new { SubcategoryID = id },
                    commandType: CommandType.StoredProcedure,
                    splitOn: "categoryid",
                    transaction: _transaction);

                return Subcategorys.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method asynchronously retrieve all Subcategories from SQL table with or without WHERE filter
        /// </summary>
        /// <returns>Task<IEnumerable<ISubcategory>></returns>
        public async Task<IEnumerable<ISubcategory>> GetAllAsync()
        {
            try
            {
                var Subcategorys = await _connection.QueryAsync<ISubcategory, ICategory, ISubcategory>(ScriptReferences.Subcategory.SP_RetrieveSubcategory,
                    (subcategory, category) =>
                    {
                        subcategory.Category = category;
                        return subcategory;
                    },
                    commandType: CommandType.StoredProcedure,
                    splitOn: "categoryid",
                    transaction: _transaction);

                return Subcategorys;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method asynchronously updates Subcategory object in table. Number of affected rows is returned
        /// </summary>
        /// <param name="subcategory">Object with values that will be passed as parameter values</param>
        /// <returns>Task<int></returns>
        public async Task<int> UpdateAsync(ISubcategory subcategory)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.AddDynamicParams(subcategory);
                parameters.AddDynamicParams(new { CategoryID = subcategory.Category.CategoryID });

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
    }
}
