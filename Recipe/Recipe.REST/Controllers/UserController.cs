using Firebase.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Recipe.Auth;
using Recipe.Auth.ViewModels;
using Recipe.ExceptionHandler.CustomExceptions;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Recipe.REST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IFirebaseClient firebaseClient;

        public UserController(IFirebaseClient firebaseClient)
        {
            this.firebaseClient = firebaseClient;
        }

        [HttpPost("/register")]
        public async Task<IActionResult> Registration(RegisterUserVM registerModel) //TODO: create new register model with address etc.
        {
            //TODO: move logic to user service
            //TODO: insert user in mine db
            try
            {
                FirebaseAuthLink UserInfo = null;

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

                //saving the token in a session variable
                if (token != null)
                {
                    HttpContext.Session.SetString("_UserToken", token);
                    HttpContext.Session.SetString("_UserRefreshToken", refreshToken);

                    return Ok(); //TODO: return UserInfo model.
                }

                throw new HttpStatusCodeException(StatusCodes.Status400BadRequest);
            }
            catch (Firebase.Auth.FirebaseAuthException ex)
            {
                throw ex;
            }
            catch (FirebaseAdmin.Auth.FirebaseAuthException ex)
            {
                throw ex;
            }
        }

        [HttpPost("/login")]
        public async Task<IActionResult> SignIn(RegisterUserVM loginModel)
        {
            try
            {
                //log in an existing user
                var loggedUserInfo = await firebaseClient.AuthProvider
                                .SignInWithEmailAndPasswordAsync(loginModel.Email, loginModel.Password);

                var test = HttpContext.User;

                string token = loggedUserInfo.FirebaseToken;
                string refreshToken = loggedUserInfo.RefreshToken;

                //save the token to a session variable
                if (token != null)
                {
                    HttpContext.Session.SetString("_UserToken", token);
                    HttpContext.Session.SetString("_UserRefreshToken", refreshToken);

                    return Ok(); //TODO: return loggedUserInfo model.
                }

                throw new HttpStatusCodeException(StatusCodes.Status400BadRequest);
            }
            catch (FirebaseAuthException ex)
            {
                throw ex;
            }
        }
    }
}
