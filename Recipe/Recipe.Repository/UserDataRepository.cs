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
    public class UserDataRepository : IUserDataRepository
    {
        private readonly IDbTransaction _transaction;
        private IDbConnection _connection => _transaction.Connection;

        public UserDataRepository(IDbTransaction transaction)
        {
            _transaction = transaction;
        }

        /// <summary>
        /// Method asynchronously inserts UserData object in table. Number of affected rows is returned
        /// </summary>
        /// <param name="userData">Object with values that will be passed as parameter values</param>
        /// <returns>Task<int></returns>
        public async Task<int> CreateAsync(IUserData userData)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters(
                    new
                    {
                        Address = userData.Address,
                        City = userData.City,
                        FirstName = userData.FirstName,
                        LastName = userData.LastName
                    });

                return await _connection.ExecuteAsync(ScriptReferences.UserData.SP_CreateUserData,
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
        /// Method asynchronously deletes UserData object from table. Number of affected rows is returned
        /// </summary>
        /// <param name="userData">Object with values that will be passed as parameter values</param>
        /// <returns>Task<int></returns>
        public async Task<int> DeleteAsync(IUserData userData)
        {
            try
            {
                return await _connection.ExecuteAsync("SP here",
                    param: userData,
                    transaction: _transaction,
                    commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method asynchronously retrieves UserData from table filtered with primary key
        /// </summary>
        /// <param name="id">UserData id (Primary Key)</param>
        /// <returns>Task<IUserData></returns>
        public async Task<IUserData> GetByIdAsync(int id)
        {
            try //TODO: check if this works
            {
                var userDatas = await _connection.QueryAsync<IUserData, IRecipe, IUserData>(ScriptReferences.UserData.SP_RetrieveUserData,
                    (userData, recipe) =>
                    {
                        userData.Recipes.Add(recipe);
                        return userData;
                    },
                    param: new { FirebaseUserID = id },
                    commandType: CommandType.StoredProcedure,
                    transaction: _transaction);

                return userDatas.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method asynchronously retrieve all UserDatas from SQL table with or without WHERE filter
        /// </summary>
        /// <returns>Task<IEnumerable<IUserData>></returns>
        public async Task<IEnumerable<IUserData>> GetAllAsync()
        {
            try
            {
                var userDatas = await _connection.QueryAsync<IUserData, IRecipe, IUserData>(ScriptReferences.UserData.SP_RetrieveUserData,
                    (userData, recipe) =>
                    {
                        userData.Recipes.Add(recipe);
                        return userData;
                    },
                    commandType: CommandType.StoredProcedure,
                    transaction: _transaction);

                return userDatas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method asynchronously updates UserData object in table. Number of affected rows is returned
        /// </summary>
        /// <param name="userData">Object with values that will be passed as parameter values</param>
        /// <returns>Task<int></returns>
        public async Task<int> UpdateAsync(IUserData userData)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.AddDynamicParams(userData);

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

