using Recipe.Models.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Recipe.Service.Common
{
    public interface ICategoryService
    {
        Task<int> CreateAsync(ICategory category);
        Task<int> CreateAsync(IEnumerable<ICategory> categories);
        Task<int> DeleteAsync(int categoryId);
        Task<ICategory> GetByIdAsync(int categoryId);
        Task<IEnumerable<ICategory>> GetAllAsync();
        Task<int> UpdateAsync(int categoryId, ICategory category);
    }
}