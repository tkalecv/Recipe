using Recipe.Models.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Recipe.Service.Common
{
    public interface IRecipeService
    {
        Task<int> CreateAsync(IEnumerable<IRecipe> recipes);
        Task<int> CreateAsync(IRecipe recipe);
        Task<int> DeleteAllUserRecipesAsync(int userId);
        Task<int> DeleteByIDAsync(int recipeId);
        Task<int> DeleteUserSpecificRecipeAsync(int recipeId, int userId);
        Task<IEnumerable<IRecipe>> GetAllAsync();
        Task<IRecipe> GetByIdAsync(int recipeId);
        Task<IEnumerable<IRecipe>> GetByUserIDAsync(int userId);
        Task<int> UpdateAsync(int recipeId, IRecipe recipe);
    }
}