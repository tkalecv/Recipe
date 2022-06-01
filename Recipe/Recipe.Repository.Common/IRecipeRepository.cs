using Recipe.Models.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Recipe.Repository.Common
{
    public interface IRecipeRepository
    {
        Task<int> CreateAsync(IEnumerable<IRecipe> recipeList);
        Task<int> CreateAsync(IRecipe recipe);
        Task<int> DeleteAsync(int? recipeId, int? userId);
        Task<IEnumerable<IRecipe>> GetAllAsync(int? recipeId, int? userId);
        Task<int> UpdateAsync(int recipeId, IRecipe recipe);
    }
}