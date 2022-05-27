using Recipe.Models.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Recipe.Repository.Common
{
    public interface ICategoryRepository
    {
        Task<int> CreateAsync(ICategory category);
        Task<int> CreateAsync(IEnumerable<ICategory> categoryList);
        Task<int> DeleteAsync(ICategory category);
        Task<IEnumerable<ICategory>> GetAllAsync();
        Task<ICategory> GetByIdAsync(int id);
        Task<int> UpdateAsync(ICategory category);
    }
}