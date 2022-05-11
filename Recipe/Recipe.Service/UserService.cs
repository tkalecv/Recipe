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
        private readonly IConfiguration _configuration;
        private readonly IGenericRepository<Models.User> _repository;

        public UserService(IFirebaseClient firebaseClient, IUnitOfWork unitOfWork
            , IMapper mapper, IConfiguration configuration)
        {
            this.firebaseClient = firebaseClient;

            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configuration = configuration;
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
        public async Task<FirebaseAuthLink> Register(RegisterUserVM registerModel) //TODO: create new register model with address etc.
        {
            FirebaseAuthLink UserInfo = null;

            //TODO: insert user in mine db
            try
            {
                string ErrorMessage = string.Empty;
                bool EmailAndPasswordValidated;

                (EmailAndPasswordValidated, ErrorMessage) = ValidateEmailAndPassword(registerModel.Email, registerModel.Password);

                if (!EmailAndPasswordValidated)
                    throw new HttpStatusCodeException(StatusCodes.Status400BadRequest, ErrorMessage);

                //create the user
                UserInfo = await firebaseClient.AuthProvider.CreateUserWithEmailAndPasswordAsync(registerModel.Email, registerModel.Password);

                Dictionary<string, object> Claims = new Dictionary<string, object>()
                {
                    { "Role", "User" },
                };

                await firebaseClient.Admin
                    .SetCustomUserClaimsAsync(UserInfo.User.LocalId, Claims);

                //log in the new user //TODO: should I remove this?
                UserInfo = await firebaseClient.AuthProvider
                                .SignInWithEmailAndPasswordAsync(registerModel.Email, registerModel.Password);

                string token = UserInfo.FirebaseToken;
                string refreshToken = UserInfo.RefreshToken;

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
        public async Task<FirebaseAuthLink> Login(RegisterUserVM loginModel)
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
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Helper methods

        /// <summary>
        /// Method verifies if E-mail address and password are correct and returns corresponding error message if they are not.
        /// </summary>
        /// <param name="emailAddress">E-mail address you want to verify</param>
        /// <param name="password">Password you want to verify</param>
        /// <returns>(bool, string)</returns>
        private (bool, string) ValidateEmailAndPassword(string emailAddress, string password)
        {
            string PasswordErrorMessage = string.Empty;
            string EmailErrorMessage = string.Empty;
            bool IsPasswordValidated;
            bool IsEmailValidated;

            (IsEmailValidated, EmailErrorMessage) = ValidateEmail(emailAddress);

            (IsPasswordValidated, PasswordErrorMessage) = ValidatePassword(password);

            if (!IsEmailValidated || !IsPasswordValidated)
                return (false, EmailErrorMessage + PasswordErrorMessage);

            return (true, "");
        }

        /// <summary>
        /// Method verifies if E-mail address is correct and returns corresponding error message if it is not.
        /// </summary>
        /// <param name="emailAddress">E-mail address you want to verify</param>
        /// <returns>(bool, string)</returns>
        private (bool, string) ValidateEmail(string emailAddress)
        {
            Regex EmailVerificationRegex = new Regex(@"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
                                                      + "@"
                                                      + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$");

            if (EmailVerificationRegex.IsMatch(emailAddress))
                return (true, "");

            return (false, "The specified E-mail is not in the required form. \n");
        }

        /// <summary>
        /// Method verifies if password has expected strength and returns corresponding error message if it does not.
        /// You can control password strength in "appsettings.json" file
        /// </summary>
        /// <param name="password">Password you want to verify</param>
        /// <returns>(bool, string)</returns>
        private (bool, string) ValidatePassword(string password)
        {
            string ErrorMessage = string.Empty;
            bool ValidationPassed = true;

            if (string.IsNullOrWhiteSpace(password))
            {
                ErrorMessage = "Password should not be empty. \n";
                return (false, ErrorMessage);
            }

            bool VerifyHasNumber = ParseToBoolean(_configuration["PasswordStrength:HasNumber"]);
            bool VerifyHasUpperChar = ParseToBoolean(_configuration["PasswordStrength:HasUpperChar"]);
            bool VerifyHasLowerChar = ParseToBoolean(_configuration["PasswordStrength:HasLowerChar"]);
            bool VerifyHasSymbols = ParseToBoolean(_configuration["PasswordStrength:HasSymbol"]);

            string[] MinMaxCharsArr =
                _configuration["PasswordStrength:HasMinMaxChars"].Split(",").Length != 2
                ? new string[] { "8", "15" }
                : _configuration["PasswordStrength:HasMinMaxChars"].Split(",");

            Regex HasNumber = new Regex(@"[0-9]+");
            Regex HasUpperChar = new Regex(@"[A-Z]+");
            Regex HasMinMaxChars = new Regex(@".{" + String.Join(",", MinMaxCharsArr) + "}");
            Regex HasLowerChar = new Regex(@"[a-z]+");
            Regex HasSymbol = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

            if (VerifyHasLowerChar && !HasLowerChar.IsMatch(password))
            {
                ErrorMessage += "Password should contain at least one lower case letter. \n";
            }
            if (VerifyHasUpperChar && !HasUpperChar.IsMatch(password))
            {
                ErrorMessage += "Password should contain at least one upper case letter. \n";
            }
            if (!HasMinMaxChars.IsMatch(password))
            {
                ErrorMessage += $"Password should not be less than {MinMaxCharsArr[0]} or greater than {MinMaxCharsArr[1]} characters. \n";
            }
            if (VerifyHasNumber && !HasNumber.IsMatch(password))
            {
                ErrorMessage += "Password should contain at least one numeric value. \n";
            }
            if (VerifyHasSymbols && !HasSymbol.IsMatch(password))
            {
                ErrorMessage += "Password should contain at least one special case character.";
            }

            if (ErrorMessage != string.Empty)
                ValidationPassed = false;

            return (ValidationPassed, ErrorMessage);
        }

        /// <summary>
        /// Method checks if string value can be parsed to boolean or not. If not "true" is returned as default value.
        /// </summary>
        /// <param name="value">String value that you want to parse</param>
        /// <returns>bool</returns>
        private bool ParseToBoolean(string value)
        {
            if (value.ToLower().Equals("true") || value.ToLower().Equals("false"))
                return bool.Parse(value);

            return true;
        }
        #endregion
    }
}
