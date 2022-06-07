using AutoMapper;
using Dapper;
using Recipe.DAL.Scripts;
using Recipe.Models;
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
        /// <param name="subcategoryId">Subcategory id (Primary Key)</param>
        /// <returns>Task<int></returns>
        public async Task<int> DeleteAsync(int subcategoryId)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters(
                    new
                    {
                        SubcategoryID = subcategoryId
                    });

                return await _connection.ExecuteAsync(ScriptReferences.Subcategory.SP_DeleteSubcategory,
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
        /// Method asynchronously retrieve all Subcategories from SQL table with or without WHERE filter
        /// </summary>
        /// <param name="subcategoryId">Subcategory id (Primary Key)</param>
        /// <param name="categoryId">Category id</param>
        /// <returns>Task<IEnumerable<ISubcategory>></returns>
        public async Task<IEnumerable<ISubcategory>> GetAllAsync(int? subcategoryId, int? categoryId)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                if (subcategoryId != null)
                    parameters.AddDynamicParams(new { SubcategoryID = subcategoryId });
                if (categoryId != null)
                    parameters.AddDynamicParams(new { CategoryID = categoryId });


                var Subcategorys = await _connection.QueryAsync<Subcategory, Category, Subcategory>(ScriptReferences.Subcategory.SP_RetrieveSubcategory,
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
        /// <param name="subcategoryId">Subcategory id (Primary Key)</param>
        /// <param name="subcategory">Object with values that will be passed as parameter values</param>
        /// <returns>Task<int></returns>
        public async Task<int> UpdateAsync(int subcategoryId, ISubcategory subcategory)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters(
                    new
                    {
                        SubcategoryID = subcategoryId,
                        Name = subcategory.Name,
                        CategoryID = subcategory.Category.CategoryID,
                    });

                return await _connection.ExecuteAsync(ScriptReferences.Subcategory.SP_UpdateSubcategory,
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
