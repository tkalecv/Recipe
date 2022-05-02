using Recipe.Models.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Recipe.Service.Common
{
    public interface IIngredientService
    {
        Task<int> CreateAsync(IEnumerable<IIngredient> entities);
        Task<int> CreateAsync(IIngredient entity);
        Task<int> DeleteAsync(IIngredient entity);
        Task<IIngredient> FindByIDAsync(int id);
        Task<IEnumerable<IIngredient>> GetAllAsync();
        Task<int> UpdateAsync(IIngredient entity);
    }
}