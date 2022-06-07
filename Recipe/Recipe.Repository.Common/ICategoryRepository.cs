using Recipe.Models.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Recipe.Repository.Common
{
    public interface ICategoryRepository
    {
        Task<int> CreateAsync(ICategory category);
        Task<int> CreateAsync(IEnumerable<ICategory> categoryList);
        Task<int> DeleteAsync(int? categoryId);
        Task<IEnumerable<ICategory>> GetAllAsync(int? categoryId);
        Task<int> UpdateAsync(int categoryId, ICategory category);
    }
}