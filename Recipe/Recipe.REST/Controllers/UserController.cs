using Firebase.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Recipe.Auth;
using Recipe.Auth.ViewModels;
using Recipe.ExceptionHandler.CustomExceptions;
using Recipe.Service.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Recipe.REST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("/register")]
        public async Task<IActionResult> Register(RegisterUserVM registerModel) //TODO: create new register model with address etc.
        {
            try
            {
                FirebaseAuthLink UserInfo = await _userService.Register(registerModel);
                string Token = UserInfo.FirebaseToken;
                string RefreshToken = UserInfo.RefreshToken;

                //saving the token in a session variable
                if (Token != null)
                {
                    HttpContext.Session.SetString("_UserToken", Token);
                    HttpContext.Session.SetString("_UserRefreshToken", RefreshToken);

                    return Ok(); //TODO: return UserInfo model.
                }

                throw new HttpStatusCodeException(StatusCodes.Status400BadRequest);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("/signin")]
        public async Task<IActionResult> SignIn(RegisterUserVM loginModel)
        {
            try
            {
                //log in an existing user
                FirebaseAuthLink UserInfo = await _userService.SignIn(loginModel);
                string Token = UserInfo.FirebaseToken;
                string RefreshToken = UserInfo.RefreshToken;

                //saving the token in a session variable
                if (Token != null)
                {
                    HttpContext.Session.SetString("_UserToken", Token);
                    HttpContext.Session.SetString("_UserRefreshToken", RefreshToken);

                    return Ok(); //TODO: return UserInfo model.
                }

                throw new HttpStatusCodeException(StatusCodes.Status400BadRequest);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("/signoff")]
        public IActionResult LogOut()
        {
            try
            {
                HttpContext.Session.Remove("_UserToken");
                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
