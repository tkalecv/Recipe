using AutoMapper;
using Recipe.Models;
using Recipe.Models.Common;
using Recipe.Repository.Common.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipe.Repository
{
    public class IngredientRepository
    {
        private readonly IGenericRepository<Ingredient> _repository;
        private readonly IMapper _mapper;

        public IngredientRepository(IGenericRepository<Ingredient> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
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
            IEnumerable<IIngredient> entities = await _repository.GetAllAsync($"WHERE IngredientID = {id};");

            return entities.FirstOrDefault();
        }

        public async Task<IEnumerable<IIngredient>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }
    }
}
