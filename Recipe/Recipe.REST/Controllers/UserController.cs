using AutoMapper;
using Firebase.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Recipe.Auth.Models;
using Recipe.ExceptionHandler.CustomExceptions;
using Recipe.REST.ViewModels.User;
using Recipe.Service.Common;
using System;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Recipe.REST.Controllers
{
    [Route("api/user")] //NOTE: You can also use this api/[controller]/[action]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost("/user/register")]
        public async Task<IActionResult> Register(RegisterUserVM registerModel)
        {
            try
            {
                FirebaseAuthLink UserInfo = await _userService.Register(_mapper.Map<AuthUser>(registerModel));
                string Token = UserInfo.FirebaseToken;
                string RefreshToken = UserInfo.RefreshToken;

                //saving the token in a session variable
                if (Token != null)
                {
                    HttpContext.Session.SetString("_UserToken", Token);
                    HttpContext.Session.SetString("_UserRefreshToken", RefreshToken);

                    return Ok(); //TODO: return UserInfo model. Or create custom model to return just some info
                }

                throw new HttpStatusCodeException(StatusCodes.Status400BadRequest);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("/user/login")]
        public async Task<IActionResult> Login(LoginUserVM loginModel)
        {
            try
            {
                var a = Microsoft.AspNetCore.Http.Extensions.UriHelper.GetDisplayUrl(Request);

                //log in an existing user
                FirebaseAuthLink UserInfo = await _userService.Login(_mapper.Map<AuthUser>(loginModel));
                string Token = UserInfo.FirebaseToken;
                string RefreshToken = UserInfo.RefreshToken;

                //saving the token in a session variable
                if (Token != null)
                {
                    HttpContext.Session.SetString("_UserToken", Token);
                    HttpContext.Session.SetString("_UserRefreshToken", RefreshToken);

                    return Ok(UserInfo); //TODO: return UserInfo model. Or create custom model to return just some info
                }

                throw new HttpStatusCodeException(StatusCodes.Status400BadRequest);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("/user/logout")]
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
