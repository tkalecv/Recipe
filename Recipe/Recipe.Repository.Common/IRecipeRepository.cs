using Recipe.Models.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Recipe.Repository.Common
{
    public interface IRecipeRepository
    {
        Task<int> CreateAsync(IEnumerable<IRecipe> recipeList);
        Task<int> CreateAsync(IRecipe recipe);
        Task<int> DeleteAsync(IRecipe recipe);
        Task<IEnumerable<IRecipe>> GetAllAsync(string where = null);
        Task<IRecipe> GetByIdAsync(int id);
        Task<int> UpdateAsync(IRecipe recipe);
    }
}