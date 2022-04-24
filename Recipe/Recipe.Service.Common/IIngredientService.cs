using Recipe.Models.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Recipe.Service.Common
{
    public interface IIngredientService
    {
        Task CreateAsync(IEnumerable<IIngredient> entities);
        Task CreateAsync(IIngredient entity);
        Task DeleteAsync(IIngredient entity);
        Task<IIngredient> FindByIDAsync(int id);
        Task<IEnumerable<IIngredient>> GetAllAsync();
        Task UpdateAsync(IIngredient entity);
    }
}