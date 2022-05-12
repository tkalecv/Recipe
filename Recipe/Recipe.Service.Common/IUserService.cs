using Firebase.Auth;
using Recipe.Auth.ModelsCommon;
using System.Threading.Tasks;

namespace Recipe.Service.Common
{
    public interface IUserService
    {
        Task<FirebaseAuthLink> Login(IAuthUser loginModel);
        Task<FirebaseAuthLink> Register(IAuthUser registerModel);
    }
}