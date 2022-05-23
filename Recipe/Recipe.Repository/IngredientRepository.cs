using AutoMapper;
using Dapper;
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
    public class IngredientRepository : IIngredientRepository
    {
        private readonly IDbTransaction _transaction;
        private IDbConnection _connection => _transaction.Connection;
        private readonly IMapper _mapper;

        public IngredientRepository(IDbTransaction transaction, IMapper mapper)
        {
            _transaction = transaction;
            _mapper = mapper;
        }

        /// <summary>
        /// Method asynchronously inserts Ingredient object in table. Number of affected rows is returned
        /// </summary>
        /// <param name="ingredient">Object with values that will be passed as parameter values</param>
        /// <returns>Task<int></returns>
        public async Task<int> CreateAsync(IIngredient ingredient)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.AddDynamicParams(ingredient);
                parameters.AddDynamicParams(ingredient.MeasuringUnits.FirstOrDefault());

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
        /// Method asynchronously inserts multiple Ingredient objects in table. Number of affected rows is returned
        /// </summary>
        /// <param name="ingredientList">List of objects with values that will be passed as parameter values</param>
        /// <returns>Task<int></returns>
        public async Task<int> CreateAsync(IEnumerable<IIngredient> ingredientList)
        {
            try
            {
                int rowNumber = 0;

                foreach (IIngredient ingredient in ingredientList)
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.AddDynamicParams(ingredient);
                    parameters.AddDynamicParams(ingredient.MeasuringUnits.FirstOrDefault());

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
        /// Method asynchronously deletes Ingredient object from table. Number of affected rows is returned
        /// </summary>
        /// <param name="ingredient">Object with values that will be passed as parameter values</param>
        /// <returns>Task<int></returns>
        public async Task<int> DeleteAsync(IIngredient ingredient)
        {
            try
            {
                return await _connection.ExecuteAsync("SP name here",
                    param: ingredient,
                    transaction: _transaction,
                    commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method asynchronously retrieves Ingredient from table filtered with primary key
        /// </summary>
        /// <param name="id">Ingredient id (Primary Key)</param>
        /// <returns>Task<IIngredient></returns>
        public async Task<IIngredient> GetByIdAsync(int id)
        {
            try
            {
                var ingredients = await _connection.QueryAsync<IIngredient, IMeasuringUnit, IIngredient>("SP name here",
                    (ingredient, munit) =>
                    {
                        ingredient.MeasuringUnits.Add(munit);
                        return ingredient;
                    },
                    param: new { id = id },
                    commandType: CommandType.StoredProcedure,
                    splitOn: "measuringunitid");

                return ingredients.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method asynchronously retrieve all Ingredients from SQL table with or without WHERE filter
        /// </summary>
        /// <param name="where">SQL WHERE filter that extends default SELECT query</param>
        /// <returns>Task<IEnumerable<IIngredient>></returns>
        public async Task<IEnumerable<IIngredient>> GetAllAsync(string where = null)
        {
            try
            {
                var ingredients = await _connection.QueryAsync<IIngredient, IMeasuringUnit, IIngredient>("SP name here",
                    (ingredient, munit) =>
                    {
                        ingredient.MeasuringUnits.Add(munit);
                        return ingredient;
                    },
                    param: new { where = where },
                    commandType: CommandType.StoredProcedure,
                    splitOn: "measuringunitid");

                return ingredients;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method asynchronously updates Ingredient object in table. Number of affected rows is returned
        /// </summary>
        /// <param name="ingredient">Object with values that will be passed as parameter values</param>
        /// <returns>Task<int></returns>
        public async Task<int> UpdateAsync(IIngredient ingredient)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.AddDynamicParams(ingredient);
                parameters.AddDynamicParams(ingredient.MeasuringUnits.FirstOrDefault());

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
