using Recipe.Models.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Recipe.Service.Common
{
    public interface IRecipeService
    {
        Task<int> CreateAsync(IEnumerable<IRecipe> recipes);
        Task<int> CreateAsync(IRecipe recipe);
        Task<int> DeleteAsync(int recipeId);
        Task<IEnumerable<IRecipe>> GetByUserIDAsync(int id);
        Task<IEnumerable<IRecipe>> GetAllAsync(int? userId);
        Task<IRecipe> GetByIdAsync(int recipeId);
        Task<int> UpdateAsync(int id, IRecipe recipe);
    }
}