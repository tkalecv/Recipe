using AutoMapper;
using Firebase.Auth;
using Recipe.Auth;
using Recipe.Auth.ViewModels;
using Recipe.Repository.Common.Generic;
using Recipe.Repository.UnitOfWork;
using Recipe.Service.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Recipe.Service
{
    public class UserService : IUserService
    {
        private readonly IFirebaseClient firebaseClient;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Models.User> _repository;

        public UserService(IFirebaseClient firebaseClient, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.firebaseClient = firebaseClient;

            _unitOfWork = unitOfWork;
            _mapper = mapper;
            // use only repository in all the methods, do not reuse service methods, because 
            // transaction passed to repository will be null after commit.
            // Keep all the logic in service then call that method in controller.
            _repository = _unitOfWork.Repository<Models.User>();
        }

        public async Task<FirebaseAuthLink> Register(RegisterUserVM registerModel) //TODO: create new register model with address etc.
        {
            FirebaseAuthLink UserInfo = null;

            //TODO: insert user in mine db
            try
            {
                //create the user
                UserInfo = await firebaseClient.AuthProvider.CreateUserWithEmailAndPasswordAsync(registerModel.Email, registerModel.Password);

                Dictionary<string, object> Claims = new Dictionary<string, object>()
                {
                    { "Role", "User" },
                };

                await firebaseClient.Admin.SetCustomUserClaimsAsync(UserInfo.User.LocalId, Claims);

                //log in the new user
                UserInfo = await firebaseClient.AuthProvider
                                .SignInWithEmailAndPasswordAsync(registerModel.Email, registerModel.Password);

                string token = UserInfo.FirebaseToken;
                string refreshToken = UserInfo.RefreshToken;

                return UserInfo;
            }
            catch (Firebase.Auth.FirebaseAuthException ex)
            {
                if (UserInfo != null)
                    await firebaseClient.Admin.DeleteUserAsync(UserInfo.User.LocalId); //TODO: check if the task can be canceled on exception throw

                throw ex;
            }
            catch (FirebaseAdmin.Auth.FirebaseAuthException ex)
            {
                if (UserInfo != null)
                    await firebaseClient.Admin.DeleteUserAsync(UserInfo.User.LocalId);

                throw ex;
            }
            catch (Exception ex)
            {
                if (UserInfo != null)
                    await firebaseClient.Admin.DeleteUserAsync(UserInfo.User.LocalId);

                throw ex;
            }
        }

        public async Task<FirebaseAuthLink> SignIn(RegisterUserVM loginModel)
        {
            FirebaseAuthLink UserInfo = null;

            try
            {
                //log in an existing user
                UserInfo = await firebaseClient.AuthProvider
                                .SignInWithEmailAndPasswordAsync(loginModel.Email, loginModel.Password);

                string token = UserInfo.FirebaseToken;
                string refreshToken = UserInfo.RefreshToken;

                return UserInfo;
            }
            catch (Firebase.Auth.FirebaseAuthException ex)
            {
                if (UserInfo != null)
                    await firebaseClient.Admin.DeleteUserAsync(UserInfo.User.LocalId); //TODO: check if the task can be canceled on exception throw

                throw ex;
            }
            catch (FirebaseAdmin.Auth.FirebaseAuthException ex)
            {
                if (UserInfo != null)
                    await firebaseClient.Admin.DeleteUserAsync(UserInfo.User.LocalId);

                throw ex;
            }
            catch (Exception ex)
            {
                if (UserInfo != null)
                    await firebaseClient.Admin.DeleteUserAsync(UserInfo.User.LocalId);

                throw ex;
            }
        }
    }
}
