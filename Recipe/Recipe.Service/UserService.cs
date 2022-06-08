using AutoMapper;
using Firebase.Auth;
using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Recipe.Auth;
using Recipe.Auth.Models;
using Recipe.Auth.ModelsCommon;
using Recipe.ExceptionHandler.CustomExceptions;
using Recipe.Helpers.AutoMapper;
using Recipe.Models;
using Recipe.Models.Common;
using Recipe.Repository.Common.UnitOfWork;
using Recipe.Repository.UnitOfWork;
using Recipe.Service.Common;
using Recipe.Service.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Recipe.Service
{
    public class UserService : IUserService
    {
        private readonly IFirebaseClient firebaseClient;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserRegistrationHelper _userRegistrationHelper;

        public UserService(IFirebaseClient firebaseClient, IUnitOfWork unitOfWork
            , IMapper mapper, IUserRegistrationHelper userRegistrationHelper)
        {
            this.firebaseClient = firebaseClient;

            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userRegistrationHelper = userRegistrationHelper;
        }

        /// <summary>
        /// Method register new user with provided data
        /// </summary>
        /// <param name="registerModel">Entity with data used to register</param>
        /// <returns>Task<FirebaseAuthLink></returns>
        public async Task<FirebaseAuthLink> Register(IAuthUser registerModel, string role = "User")
        {
            UserRecord UserRecord = null;

            try
            {
                #region Email and Password validation
                string ErrorMessage = string.Empty;
                bool EmailAndPasswordValidated;

                (EmailAndPasswordValidated, ErrorMessage) = _userRegistrationHelper.ValidateEmailAndPassword(registerModel.Email, registerModel.Password);

                if (!EmailAndPasswordValidated)
                    throw new HttpStatusCodeException(StatusCodes.Status400BadRequest, ErrorMessage);
                #endregion

                #region Create Firebase user
                UserRecord = await firebaseClient.Admin
                    .CreateUserAsync(_mapper.Map<UserRecordArgs>(registerModel));

                registerModel.Uid = UserRecord.Uid;
                #endregion

                #region Set claims
                Dictionary<string, object> Claims = new Dictionary<string, object>()
                {
                    { ClaimTypes.Role, role },
                };

                await SetCustomUserClaims(UserRecord.Uid, Claims);
                #endregion

                #region Insert User into custom db
                await _unitOfWork.BeginTransactionAsync();
                await _unitOfWork.UserDataRepository.CreateAsync(_mapper.Map<UserData>(registerModel));
                await _unitOfWork.CommitAsync();
                #endregion

                //log in the new user //TODO: should I remove this?
                return await firebaseClient.AuthProvider
                                .SignInWithEmailAndPasswordAsync(registerModel.Email, registerModel.Password);
            }
            catch (Exception ex)
            {
                if (UserRecord != null)
                {
                    await firebaseClient.Admin.DeleteUserAsync(UserRecord.Uid);

                    await _unitOfWork.RollbackAsync();
                }
                throw ex;
            }
        }

        /// <summary>
        /// Method logs in existing user with provided email and password
        /// </summary>
        /// <param name="loginModel">Entity with data used to log in</param>
        /// <returns>Task<FirebaseAuthLink></returns>
        public async Task<FirebaseAuthLink> Login(IAuthUser loginModel)
        {
            try
            {
                return await firebaseClient.AuthProvider
                                .SignInWithEmailAndPasswordAsync(loginModel.Email, loginModel.Password);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// ethod retrieves user data using his id
        /// </summary>
        /// <param name="userId">Id of the user</param>
        /// <returns>Task<UserRecord></returns>
        public async Task<IAuthUser> GetByIDAsync(string userId)
        {
            try
            {
                UserRecord firebaseUserRecord = await firebaseClient.Admin
                    .GetUserAsync(userId);

                await _unitOfWork.BeginTransactionAsync();
                IEnumerable<IUserData> userData = await _unitOfWork.UserDataRepository.GetAllAsync(firebaseUserRecord.Uid);
                await _unitOfWork.CommitAsync();

                return _mapper.MergeInto<AuthUser>(firebaseUserRecord, userData.FirstOrDefault());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method retrieves user data using his bearer token
        /// </summary>
        /// <param name="token">Users bearer token</param>
        /// <returns>Task<Firebase.Auth.User></returns>
        public async Task<IAuthUser> GetWithToken(string token)
        {
            try
            {
                User firebaseUser = await firebaseClient.AuthProvider
                    .GetUserAsync(token);

                await _unitOfWork.BeginTransactionAsync();
                IEnumerable<IUserData> userData = await _unitOfWork.UserDataRepository.GetAllAsync(firebaseUser.LocalId);
                await _unitOfWork.CommitAsync();

                return _mapper.MergeInto<AuthUser>(firebaseUser, userData.FirstOrDefault());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method updates users token expiration time to current time
        /// </summary>
        /// <param name="userId">Id of the user</param>
        /// <returns>Task</returns>
        public async Task RefreshToken(string userId)
        {
            try
            {
                await firebaseClient.Admin
                   .RevokeRefreshTokensAsync(userId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method updates user entry in custom db and firebase db
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <param name="user">User object with new values</param>
        /// <returns>Task<int></returns>
        public async Task<int> UpdateAsync(string userId, IAuthUser user)
        {
            try
            {
                user.Uid = userId;

                #region Email and Password validation
                string ErrorMessage = string.Empty;
                bool EmailAndPasswordValidated;

                (EmailAndPasswordValidated, ErrorMessage) = _userRegistrationHelper.ValidateEmailAndPassword(user.Email, user.Password);

                if (!EmailAndPasswordValidated)
                    throw new HttpStatusCodeException(StatusCodes.Status400BadRequest, ErrorMessage);
                #endregion


                await firebaseClient.Admin
                    .UpdateUserAsync(_mapper.Map<UserRecordArgs>(user));

                await _unitOfWork.BeginTransactionAsync();

                int rowCount = await _unitOfWork.UserDataRepository.UpdateAsync(userId, _mapper.Map<UserData>(user));

                await _unitOfWork.CommitAsync();

                return rowCount;
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();

                throw ex;
            }
        }

        /// <summary>
        /// Method removes User entry from custom db and firebase db
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <returns>Task<int></returns>
        public async Task<int> DeleteAsync(string userId)
        {
            try
            {
                await firebaseClient.Admin
                    .DeleteUserAsync(userId);

                await _unitOfWork.BeginTransactionAsync();

                int rowCount = await _unitOfWork.UserDataRepository.DeleteAsync(userId);

                await _unitOfWork.CommitAsync();

                return rowCount;
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();

                throw ex;
            }
        }

        #region Helper methods
        /// <summary>
        /// Method sets custom claims to specified user
        /// </summary>
        /// <param name="userId">User identifier to which you want to set claims to</param>
        /// <param name="claims">Custom claims you want to set to user</param>
        /// <returns>Task</returns>
        private async Task SetCustomUserClaims(string userId, Dictionary<string, object> claims)
        {
            try
            {
                await firebaseClient.Admin
                     .SetCustomUserClaimsAsync(userId, claims);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
