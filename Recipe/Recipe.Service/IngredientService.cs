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
            // use only repository in all the methods, do not reuse service methods, because 
            // transaction passed to repository will be null after commit.
            // Keep all the logic in service then call that method in controller.
            _repository = _unitOfWork.Repository<Ingredient>();
        }

        /// <summary>
        /// Method creates new Ingredient entry in db
        /// </summary>
        /// <param name="entity">Ingredient object that will be created</param>
        /// <returnsTask<int>></returns>
        public async Task<int> CreateAsync(IIngredient entity)
        {
            try
            {
                int rowCount = await _repository.CreateAsync(_mapper.Map<Ingredient>(entity));

                await _unitOfWork.CommitAsync();

                return rowCount;
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();

                throw ex;
            }
        }

        /// <summary>
        /// Method creates multiple Ingredient entries in db
        /// </summary>
        /// <param name="entities">List of Ingredient objects that will be created</param>
        /// <returns>Task<int></returns>
        public async Task<int> CreateAsync(IEnumerable<IIngredient> entities)
        {
            try
            {
                int rowCount = await _repository.CreateAsync(_mapper.Map<IEnumerable<Ingredient>>(entities));

                await _unitOfWork.CommitAsync();

                return rowCount;
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();

                throw ex;
            }
        }

        /// <summary>
        /// Method removes Ingredient entry from db
        /// </summary>
        /// <param name="entity">Ingredient object that will be removed</param>
        /// <returns>Task<int></returns>
        public async Task<int> DeleteAsync(IIngredient entity)
        {
            try
            {
                int rowCount = await _repository.DeleteAsync(_mapper.Map<Ingredient>(entity));

                await _unitOfWork.CommitAsync();

                return rowCount;
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();

                throw ex;
            }
        }

        /// <summary>
        /// Method removes Ingredient entry from db
        /// </summary>
        /// <param name="id">ID unique identifier of Ingredient object that will be removed</param>
        /// <returns>Task<int></returns>
        public async Task<int> DeleteAsync(int id)
        {
            try
            {
                var entity = await _repository.GetByIdAsync(id);

                int rowCount = await _repository.DeleteAsync(entity);

                await _unitOfWork.CommitAsync();

                return rowCount;
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();

                throw ex;
            }
        }

        /// <summary>
        /// Method updates Ingredient entry in db
        /// </summary>
        /// <param name="entity">Ingredient object that will be updated</param>
        /// <returns>Task<int></returns>
        public async Task<int> UpdateAsync(IIngredient entity)
        {
            try
            {
                int rowCount = await _repository.UpdateAsync(_mapper.Map<Ingredient>(entity));

                await _unitOfWork.CommitAsync();

                return rowCount;
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();

                throw ex;
            }
        }

        /// <summary>
        /// Method retrieves Ingredient entry from db filtered by ID unique identifier
        /// </summary>
        /// <param name="id">ID unique identifier of Ingredient object that will be updated</param>
        /// <returns>Task<IIngredient></returns>
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

        /// <summary>
        /// Method retrieves all of Ingredient entries from db
        /// </summary>
        /// <returns>Task<IEnumerable<IIngredient>></returns>
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
