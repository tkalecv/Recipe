using Recipe.Models.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Recipe.Repository.Common
{
    public interface IIngredientRepository
    {
        Task<int> CreateAsync(IEnumerable<IIngredient> ingredientList);
        Task<int> CreateAsync(IIngredient ingredient);
        Task<int> DeleteAsync(IIngredient ingredient);
        Task<IEnumerable<IIngredient>> GetAllAsync(string where = null);
        Task<IIngredient> GetByIdAsync(int id);
        Task<int> UpdateAsync(IIngredient ingredient);
    }
}