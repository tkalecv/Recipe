using Recipe.Models.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Recipe.Service.Common
{
    public interface ISubcategoryService
    {
        Task<int> CreateAsync(IEnumerable<ISubcategory> entities);
        Task<int> CreateAsync(ISubcategory entity);
        Task<int> DeleteAsync(int id);
        Task<int> DeleteAsync(ISubcategory entity);
        Task<ISubcategory> FindByIDAsync(int id);
        Task<IEnumerable<ISubcategory>> GetAllAsync();
        Task<int> UpdateAsync(int id, ISubcategory entity);
        Task<int> UpdateAsync(ISubcategory entity);
    }
}