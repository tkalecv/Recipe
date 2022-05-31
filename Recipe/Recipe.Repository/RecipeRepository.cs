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
    public class RecipeRepository : IRecipeRepository
    {
        private readonly IDbTransaction _transaction;
        private IDbConnection _connection => _transaction.Connection;

        public RecipeRepository(IDbTransaction transaction)
        {
            _transaction = transaction;
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
                DynamicParameters parameters = new DynamicParameters(
                    new
                    {
                        Name = recipe.Name,
                        Description = recipe.Description,
                        SubcategoryID = recipe.Subcategory.SubcategoryID,
                        UserDataID = recipe.UserData.UserDataID
                    });

                return await _connection.ExecuteAsync(ScriptReferences.Recipe.SP_CreateRecipe,
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
                    DynamicParameters parameters = new DynamicParameters(
                        new
                        {
                            Name = recipe.Name,
                            Description = recipe.Description,
                            SubcategoryID = recipe.Subcategory.SubcategoryID,
                            UserDataID = recipe.UserData.UserDataID
                        });

                    rowNumber += await _connection.ExecuteAsync(ScriptReferences.Recipe.SP_CreateRecipe,
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
        /// <param name="recipeId">Recipe id (Primary Key)</param>
        /// <returns>Task<int></returns>
        public async Task<int> DeleteAsync(int recipeId)
        {
            try
            {
                return await _connection.ExecuteAsync("SP name here",
                    transaction: _transaction,
                    commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method asynchronously retrieves Recipe from table filtered by user
        /// </summary>
        /// <param name="userId">User id (Primary Key)</param>
        /// <returns>Task<IRecipe></returns>
        public async Task<IEnumerable<IRecipe>> GetByUserIdAsync(int userId) //TODO: add recipeid parameter to stored procedure
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters(
                    new
                    {
                        UserDataID = userId
                    });

                var recipes = await _connection.QueryAsync<Models.Recipe, Subcategory, UserData, Category, Models.Recipe>(ScriptReferences.Recipe.SP_RetrieveRecipeByUser,
                    (recipe, subcat, userdata, cat) =>
                    {
                        recipe.UserData = userdata;
                        subcat.Category = cat;
                        recipe.Subcategory = subcat;
                        return recipe;
                    },
                    splitOn: "SubcategoryID,UserDataID,CategoryID",
                    param: parameters,
                    commandType: CommandType.StoredProcedure,
                    transaction: _transaction);

                return recipes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method asynchronously retrieve all Recipes from SQL table with or without WHERE filter
        /// </summary>
        /// <param name="recipeId">User id parameter. If left empty, all recipes are returned</param>
        /// <param name="where">SQL WHERE filter that extends default SELECT query</param>
        /// <returns>Task<IEnumerable<IRecipe>></returns>
        public async Task<IEnumerable<IRecipe>> GetAllAsync(int? recipeId) //TODO: will we ever retrieve all recipes
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                if (recipeId != null)
                    parameters.AddDynamicParams(new { UserDataID = recipeId });

                var recipes = await _connection.QueryAsync<Models.Recipe, Subcategory, UserData, Category, Models.Recipe>(
                    "SP name here", //TODO: create stored procedure
                    (recipe, subcat, userdata, cat) =>
                    {
                        recipe.UserData = userdata;
                        subcat.Category = cat;
                        recipe.Subcategory = subcat;
                        return recipe;
                    },
                    splitOn: "SubcategoryID,UserDataID,CategoryID",
                    param: parameters,
                    commandType: CommandType.StoredProcedure,
                    transaction: _transaction);

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
        /// <param name="recipeId">Recipe id (Primary Key)</param>
        /// <param name="recipe">Object with values that will be passed as parameter values</param>
        /// <returns>Task<int></returns>
        public async Task<int> UpdateAsync(int recipeId, IRecipe recipe)
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
