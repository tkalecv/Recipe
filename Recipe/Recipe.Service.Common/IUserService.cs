using Firebase.Auth;
using Recipe.Auth.ViewModels;
using System.Threading.Tasks;

namespace Recipe.Service.Common
{
    public interface IUserService
    {
        Task<FirebaseAuthLink> Login(LoginUserVM loginModel);
        Task<FirebaseAuthLink> Register(RegisterUserVM registerModel);
    }
}