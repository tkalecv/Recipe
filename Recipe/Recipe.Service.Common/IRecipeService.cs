using Recipe.Models.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Recipe.Service.Common
{
    public interface IRecipeService
    {
        Task<int> CreateAsync(IEnumerable<IRecipe> recipes);
        Task<int> CreateAsync(IRecipe recipe);
        Task<int> DeleteAsync(int id);
        Task<int> DeleteAsync(IRecipe recipe);
        Task<IRecipe> GetByUserIDAsync(int id);
        Task<IEnumerable<IRecipe>> GetAllAsync(int? userId);
        Task<int> UpdateAsync(int id, IRecipe recipe);
        Task<int> UpdateAsync(IRecipe recipe);
    }
}