using Recipe.Models.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Recipe.Service.Common
{
    public interface ICategoryService
    {
        Task<int> CreateAsync(ICategory entity);
        Task<int> CreateAsync(IEnumerable<ICategory> entities);
        Task<int> DeleteAsync(ICategory entity);
        Task<int> DeleteAsync(int id);
        Task<ICategory> FindByIDAsync(int id);
        Task<IEnumerable<ICategory>> GetAllAsync();
        Task<int> UpdateAsync(ICategory entity);
        Task<int> UpdateAsync(int id, ICategory entity);
    }
}