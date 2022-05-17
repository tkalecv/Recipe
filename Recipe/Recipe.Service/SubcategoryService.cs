using AutoMapper;
using Recipe.Models;
using Recipe.Models.Common;
using Recipe.Repository.Common.Generic;
using Recipe.Repository.UnitOfWork;
using Recipe.Service.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Recipe.Service
{
    public class SubcategoryService : ISubcategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Subcategory> _repository;

        public SubcategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            // use only repository in all the methods, do not reuse service methods, because 
            // transaction passed to repository will be null after commit.
            // Keep all the logic in service then call that method in controller.
            _repository = _unitOfWork.Repository<Subcategory>();
        }

        /// <summary>
        /// Method creates new Subcategory entry in db
        /// </summary>
        /// <param name="entity">Subcategory object that will be created</param>
        /// <returnsTask<int>></returns>
        public async Task<int> CreateAsync(ISubcategory entity)
        {
            try
            {
                int rowCount = await _repository.CreateAsync(_mapper.Map<Subcategory>(entity));

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
        /// Method creates multiple Subcategory entries in db
        /// </summary>
        /// <param name="entities">List of Subcategory objects that will be created</param>
        /// <returns>Task<int></returns>
        public async Task<int> CreateAsync(IEnumerable<ISubcategory> entities)
        {
            try
            {
                int rowCount = await _repository.CreateAsync(_mapper.Map<IEnumerable<Subcategory>>(entities));

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
        /// Method removes Subcategory entry from db
        /// </summary>
        /// <param name="entity">Subcategory object that will be removed</param>
        /// <returns>Task<int></returns>
        public async Task<int> DeleteAsync(ISubcategory entity)
        {
            try
            {
                int rowCount = await _repository.DeleteAsync(_mapper.Map<Subcategory>(entity));

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
        /// Method removes Subcategory entry from db
        /// </summary>
        /// <param name="id">ID unique identifier of Subcategory object that will be removed</param>
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
        /// Method updates Subcategory entry in db
        /// </summary>
        /// <param name="entity">Subcategory object that will be updated</param>
        /// <returns>Task<int></returns>
        public async Task<int> UpdateAsync(ISubcategory entity)
        {
            try
            {
                int rowCount = await _repository.UpdateAsync(_mapper.Map<Subcategory>(entity));

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
        /// Method updates Subcategory entry in db
        /// </summary>
        /// <param name="id">ID unique identifier of Subcategory object that will be updated</param>
        /// <param name="entity">Subcategory object with new values</param>
        /// <returns>Task<int></returns>
        public async Task<int> UpdateAsync(int id, ISubcategory entity)
        {
            try
            {
                entity.SubcategoryID = id;

                int rowCount = await _repository.UpdateAsync(_mapper.Map<Subcategory>(entity));

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
        /// Method retrieves Subcategory entry from db filtered by ID unique identifier
        /// </summary>
        /// <param name="id">ID unique identifier of Subcategory object that will be updated</param>
        /// <returns>Task<ISubcategory></returns>
        public async Task<ISubcategory> FindByIDAsync(int id)
        {
            try
            {
                ISubcategory entity = await _repository.GetByIdAsync(id);

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
        /// Method retrieves all of Subcategory entries from db
        /// </summary>
        /// <returns>Task<IEnumerable<ISubcategory>></returns>
        public async Task<IEnumerable<ISubcategory>> GetAllAsync()
        {
            try
            {
                IEnumerable<ISubcategory> entities = await _repository.GetAllAsync();

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
