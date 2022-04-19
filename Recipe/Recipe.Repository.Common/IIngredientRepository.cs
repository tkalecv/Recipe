using Recipe.Models.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Recipe.Repository.Common
{
    public interface IIngredientRepository
    {
        Task CreateAsync(IEnumerable<IIngredient> entities);
        Task CreateAsync(IIngredient entity);
        Task DeleteAsync(IIngredient entity);
        Task<IIngredient> FindByIDAsync(int id);
        Task<IEnumerable<IIngredient>> GetAllAsync();
        Task UpdateAsync(IIngredient entity);
    }
}