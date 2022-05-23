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
    public class RecipeRepository : IRecipeRepository
    {
        private readonly IDbTransaction _transaction;
        private IDbConnection _connection => _transaction.Connection;
        private readonly IMapper _mapper;

        public RecipeRepository(IDbTransaction transaction, IMapper mapper)
        {
            _transaction = transaction;
            _mapper = mapper;
        }

        /// <summary>
        /// Method asynchronously inserts Recipe object in table. Number of affected rows is returned
        /// </summary>
        /// <param name="recipe">Object with values that will be passed as parameter values</param>
        /// <returns>Task<int></returns>
        public async Task<int> CreateAsync(IRecipe recipe)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.AddDynamicParams(recipe);
                //TODO: add other objects

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
        /// Method asynchronously inserts multiple Recipe objects in table. Number of affected rows is returned
        /// </summary>
        /// <param name="recipeList">List of objects with values that will be passed as parameter values</param>
        /// <returns>Task<int></returns>
        public async Task<int> CreateAsync(IEnumerable<IRecipe> recipeList)
        {
            try
            {
                int rowNumber = 0;

                foreach (IRecipe recipe in recipeList)
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.AddDynamicParams(recipe);

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
        /// Method asynchronously deletes Recipe object from table. Number of affected rows is returned
        /// </summary>
        /// <param name="recipe">Object with values that will be passed as parameter values</param>
        /// <returns>Task<int></returns>
        public async Task<int> DeleteAsync(IRecipe recipe)
        {
            try
            {
                return await _connection.ExecuteAsync("SP name here",
                    param: recipe,
                    transaction: _transaction,
                    commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method asynchronously retrieves Recipe from table filtered with primary key
        /// </summary>
        /// <param name="id">Recipe id (Primary Key)</param>
        /// <returns>Task<IRecipe></returns>
        public async Task<IRecipe> GetByIdAsync(int id)
        {
            try
            {
                var recipes = await _connection.QueryAsync<IRecipe>("SP name here",
                    param: new { id = id },
                    commandType: CommandType.StoredProcedure);

                return recipes.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method asynchronously retrieve all Recipes from SQL table with or without WHERE filter
        /// </summary>
        /// <param name="where">SQL WHERE filter that extends default SELECT query</param>
        /// <returns>Task<IEnumerable<IRecipe>></returns>
        public async Task<IEnumerable<IRecipe>> GetAllAsync(string where = null)
        {
            try
            {
                var recipes = await _connection.QueryAsync<IRecipe>("SP name here",
                    param: new { where = where },
                    commandType: CommandType.StoredProcedure);

                return recipes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method asynchronously updates Recipe object in table. Number of affected rows is returned
        /// </summary>
        /// <param name="recipe">Object with values that will be passed as parameter values</param>
        /// <returns>Task<int></returns>
        public async Task<int> UpdateAsync(IRecipe recipe)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.AddDynamicParams(recipe);

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
