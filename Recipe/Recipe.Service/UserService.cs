using AutoMapper;
using Firebase.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Recipe.Auth;
using Recipe.Auth.ViewModels;
using Recipe.ExceptionHandler.CustomExceptions;
using Recipe.Repository.Common.Generic;
using Recipe.Repository.UnitOfWork;
using Recipe.Service.Common;
using Recipe.Service.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Recipe.Service
{
    //TODO: Add methods for GetUser and RefreshToken
    public class UserService : IUserService
    {
        private readonly IFirebaseClient firebaseClient;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserRegistrationHelper _userRegistrationHelper;
        private readonly IGenericRepository<Models.User> _repository;

        public UserService(IFirebaseClient firebaseClient, IUnitOfWork unitOfWork
            , IMapper mapper, IUserRegistrationHelper userRegistrationHelper)
        {
            this.firebaseClient = firebaseClient;

            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userRegistrationHelper = userRegistrationHelper;
            // use only repository in all the methods, do not reuse service methods, because 
            // transaction passed to repository will be null after commit.
            // Keep all the logic in service then call that method in controller.
            _repository = _unitOfWork.Repository<Models.User>();
        }

        /// <summary>
        /// Method register new user with provided data
        /// </summary>
        /// <param name="registerModel">Entity with data used to register</param>
        /// <returns>Task<FirebaseAuthLink></returns>
        public async Task<FirebaseAuthLink> Register(RegisterUserVM registerModel)
        {
            FirebaseAuthLink UserInfo = null;

            //TODO: insert user in mine db
            try
            {
                #region Email and Password validation
                string ErrorMessage = string.Empty;
                bool EmailAndPasswordValidated;

                (EmailAndPasswordValidated, ErrorMessage) = _userRegistrationHelper.ValidateEmailAndPassword(registerModel.Email, registerModel.Password);

                if (!EmailAndPasswordValidated)
                    throw new HttpStatusCodeException(StatusCodes.Status400BadRequest, ErrorMessage);
                #endregion

                //create the user
                UserInfo = await firebaseClient.AuthProvider.CreateUserWithEmailAndPasswordAsync(registerModel.Email, registerModel.Password);

                #region Set claims
                Dictionary<string, object> Claims = new Dictionary<string, object>()
                {
                    { "Role", "User" },
                };

                await SetCustomUserClains(UserInfo.User.LocalId, Claims);
                #endregion

                //insert user in custom db
                await _repository.CreateAsync(_mapper.Map<Models.User>(registerModel));

                //log in the new user //TODO: should I remove this?
                UserInfo = await firebaseClient.AuthProvider
                                .SignInWithEmailAndPasswordAsync(registerModel.Email, registerModel.Password);

                return UserInfo;
            }
            catch (Exception ex)
            {
                if (UserInfo != null)
                    await firebaseClient.Admin.DeleteUserAsync(UserInfo.User.LocalId);

                throw ex;
            }
        }

        /// <summary>
        /// Method logs in existing user with provided email and password
        /// </summary>
        /// <param name="loginModel">Entity with data used to log in</param>
        /// <returns>Task<FirebaseAuthLink></returns>
        public async Task<FirebaseAuthLink> Login(LoginUserVM loginModel)
        {
            try
            {
                //log in an existing user
                return await firebaseClient.AuthProvider
                                .SignInWithEmailAndPasswordAsync(loginModel.Email, loginModel.Password);

            }
            catch (Exception ex)
            {
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
        private async Task SetCustomUserClains(string userId, Dictionary<string, object> claims)
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
