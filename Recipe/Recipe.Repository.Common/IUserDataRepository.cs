using Recipe.Models.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Recipe.Repository.Common
{
    public interface IUserDataRepository
    {
        Task<int> CreateAsync(IUserData userData);
        Task<int> DeleteAsync(IUserData userData);
        Task<IEnumerable<IUserData>> GetAllAsync();
        Task<IUserData> GetByIdAsync(int id);
        Task<int> UpdateAsync(IUserData userData);
    }
}