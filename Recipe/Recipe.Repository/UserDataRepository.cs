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
                        FirebaseUserID = userData.FirebaseUserID,
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
        /// <param name="firebaseUserId">Users firebase id</param>
        /// <returns>Task<int></returns>
        public async Task<int> DeleteAsync(string firebaseUserId)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters(
                    new
                    {
                        FirebaseUserID = firebaseUserId
                    });


                return await _connection.ExecuteAsync(ScriptReferences.UserData.SP_DeleteUserData,
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
        /// Method asynchronously retrieve all UserDatas from SQL table with or without WHERE filter
        /// </summary>
        /// <param name="firebaseUserId">Users firebase id</param>
        /// <returns>Task<IEnumerable<IUserData>></returns>
        public async Task<IEnumerable<IUserData>> GetAllAsync(string? firebaseUserId)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                if (firebaseUserId != null)
                    parameters.AddDynamicParams(new { FirebaseUserID = firebaseUserId });

                var userDatas = await _connection.QueryAsync<UserData>(ScriptReferences.UserData.SP_RetrieveUserData,
                    param: parameters,
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
        /// <param name="firebaseUserId">Users firebase id</param>
        /// <param name="userData">Object with values that will be passed as parameter values</param>
        /// <returns>Task<int></returns>
        public async Task<int> UpdateAsync(string firebaseUserId, IUserData userData)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters(
                    new
                    {
                        FirebaseUserID = firebaseUserId,
                        FirstName = userData.FirstName,
                        LastName = userData.LastName,
                        Address = userData.Address,
                        City = userData.City
                    });

                return await _connection.ExecuteAsync(ScriptReferences.UserData.SP_UpdateUserData,
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

