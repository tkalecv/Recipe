using Recipe.Models.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Recipe.Repository.Common
{
    public interface IUserDataRepository
    {
        Task<int> CreateAsync(IUserData userData);
        Task<int> DeleteAsync(string firebaseUserId);
        Task<IEnumerable<IUserData>> GetAllAsync(string? firebaseUserId);
        Task<int> UpdateAsync(string firebaseUserId, IUserData userData);
    }
}