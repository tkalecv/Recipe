using Recipe.Models.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Recipe.Repository.Common
{
    public interface ISubcategoryRepository
    {
        Task<int> CreateAsync(IEnumerable<ISubcategory> subcategoryList);
        Task<int> CreateAsync(ISubcategory subcategory);
        Task<int> DeleteAsync(ISubcategory subcategory);
        Task<IEnumerable<ISubcategory>> GetAllAsync(string where = null);
        Task<ISubcategory> GetByIdAsync(int id);
        Task<int> UpdateAsync(ISubcategory subcategory);
    }
}