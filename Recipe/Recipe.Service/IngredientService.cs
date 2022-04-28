using AutoMapper;
using Recipe.Models;
using Recipe.Models.Common;
using Recipe.Repository.Common;
using Recipe.Repository.Common.Generic;
using Recipe.Repository.UnitOfWork;
using Recipe.Service.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Recipe.Service
{
    public class IngredientService : IIngredientService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Ingredient> _repository;

        public IngredientService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _repository = _unitOfWork.Repository<Ingredient>();
        }

        public async Task CreateAsync(IIngredient entity)
        {
            try
            {
                await _repository.CreateAsync(_mapper.Map<Ingredient>(entity));

                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();

                throw ex;
            }
        }

        public async Task CreateAsync(IEnumerable<IIngredient> entities)
        {
            try
            {
                await _repository.CreateAsync(_mapper.Map<IEnumerable<Ingredient>>(entities));

                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();

                throw ex;
            }
        }

        public async Task DeleteAsync(IIngredient entity)
        {
            try
            {
                await _repository.DeleteAsync(_mapper.Map<Ingredient>(entity));

                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();

                throw ex;
            }
        }

        public async Task UpdateAsync(IIngredient entity)
        {
            try
            {
                await _repository.UpdateAsync(_mapper.Map<Ingredient>(entity));

                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();

                throw ex;
            }
        }

        public async Task<IIngredient> FindByIDAsync(int id)
        {
            try
            {
                IIngredient entity = await _repository.GetByIdAsync(id);

                await _unitOfWork.CommitAsync();

                return entity;
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();

                throw ex;
            }
        }

        public async Task<IEnumerable<IIngredient>> GetAllAsync()
        {
            try
            {
                IEnumerable<IIngredient> entities = await _repository.GetAllAsync();

                await _unitOfWork.CommitAsync();

                return entities;
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();

                throw ex;
            }
        }
    }
}
