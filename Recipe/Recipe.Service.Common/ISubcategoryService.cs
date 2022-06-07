using Recipe.Models.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Recipe.Service.Common
{
    public interface ISubcategoryService
    {
        Task<int> CreateAsync(IEnumerable<ISubcategory> subcategories);
        Task<int> CreateAsync(ISubcategory subcategory);
        Task<int> DeleteAsync(int subcategoryId);
        Task<ISubcategory> GetByIDAsync(int subcategoryId);
        Task<IEnumerable<ISubcategory>> GetByCategoryIDAsync(int categoryId);
        Task<IEnumerable<ISubcategory>> GetAllAsync();
        Task<int> UpdateAsync(int id, ISubcategory entity);
    }
}