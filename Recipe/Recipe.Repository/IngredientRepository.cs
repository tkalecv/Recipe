using AutoMapper;
using Recipe.Models;
using Recipe.Models.Common;
using Recipe.Repository.Common;
using Recipe.Repository.Common.Generic;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recipe.Repository
{
    public class IngredientRepository : IIngredientRepository
    {
        private readonly IGenericRepository<Ingredient> _repository;
        private readonly IMapper _mapper;
        private string TableName { get; set; }

        public IngredientRepository(IGenericRepository<Ingredient> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;

            TableName = typeof(Ingredient).Name;
        }

        public async Task CreateAsync(IIngredient entity)
        {
            await _repository.CreateAsync(_mapper.Map<Ingredient>(entity));
        }

        public async Task CreateAsync(IEnumerable<IIngredient> entities)
        {
            await _repository.CreateAsync(_mapper.Map<IEnumerable<Ingredient>>(entities));
        }

        public async Task DeleteAsync(IIngredient entity)
        {
            await _repository.DeleteAsync(_mapper.Map<Ingredient>(entity));
        }

        public async Task UpdateAsync(IIngredient entity)
        {
            await _repository.UpdateAsync(_mapper.Map<Ingredient>(entity));
        }

        public async Task<IIngredient> FindByIDAsync(int id)
        {
            IEnumerable<IIngredient> entities = await _repository.GetAllAsync($"WHERE {TableName}ID = {id};");

            return entities.FirstOrDefault();
        }

        public async Task<IEnumerable<IIngredient>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }
    }
}
