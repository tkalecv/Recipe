using Firebase.Auth;
using Recipe.Auth.ModelsCommon;
using System.Threading.Tasks;

namespace Recipe.Service.Common
{
    public interface IUserService
    {
        Task<int> DeleteAsync(string userId);
        Task<IAuthUser> GetByIDAsync(string userId);
        Task<IAuthUser> GetWithToken(string token);
        Task<FirebaseAuthLink> Login(IAuthUser loginModel);
        Task RefreshToken(string userId);
        Task<FirebaseAuthLink> Register(IAuthUser registerModel, string role = "User");
        Task<int> UpdateAsync(string userId, IAuthUser user);
    }
}