using Recipe.Models.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Recipe.Repository.Common
{
    public interface ISubcategoryRepository
    {
        Task<int> CreateAsync(IEnumerable<ISubcategory> subcategoryList);
        Task<int> CreateAsync(ISubcategory subcategory);
        Task<int> DeleteAsync(int subcategoryId);
        Task<IEnumerable<ISubcategory>> GetAllAsync(int? subcategoryId, int? categoryId);
        Task<int> UpdateAsync(int subcategoryId, ISubcategory subcategory);
    }
}