using Recipe.Models.Common;
using Recipe.Repository.Common;
using Recipe.Service.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Recipe.Service
{
    public class IngredientService : IIngredientService
    {
        private readonly IIngredientRepository _ingredientRepository;

        public IngredientService(IIngredientRepository ingredientRepository)
        {
            _ingredientRepository = ingredientRepository;
        }

        public async Task CreateAsync(IIngredient entity)
        {
            await _ingredientRepository.CreateAsync(entity);
        }

        public async Task CreateAsync(IEnumerable<IIngredient> entities)
        {
            await _ingredientRepository.CreateAsync(entities);
        }

        public async Task DeleteAsync(IIngredient entity)
        {
            await _ingredientRepository.DeleteAsync(entity);
        }

        public async Task UpdateAsync(IIngredient entity)
        {
            await _ingredientRepository.UpdateAsync(entity);
        }

        public async Task<IIngredient> FindByIDAsync(int id)
        {
            return await _ingredientRepository.FindByIDAsync(id);
        }

        public async Task<IEnumerable<IIngredient>> GetAllAsync()
        {
            return await _ingredientRepository.GetAllAsync();
        }
    }
}
