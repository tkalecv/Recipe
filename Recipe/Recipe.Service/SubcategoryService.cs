using AutoMapper;
using Recipe.Models;
using Recipe.Models.Common;
using Recipe.Repository.Common.UnitOfWork;
using Recipe.Repository.UnitOfWork;
using Recipe.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipe.Service
{
    public class SubcategoryService : ISubcategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SubcategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Method creates new Subcategory entry in db
        /// </summary>
        /// <param name="subcategory">Subcategory object that will be created</param>
        /// <returnsTask<int>></returns>
        public async Task<int> CreateAsync(ISubcategory subcategory)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                int rowCount = await _unitOfWork.SubcategoryRepository.CreateAsync(subcategory);

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
                await _unitOfWork.BeginTransactionAsync();

                int rowCount = await _unitOfWork.SubcategoryRepository.CreateAsync(entities);

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
        /// <param name="subcategoryId">ID unique identifier of Subcategory object that will be removed</param>
        /// <returns>Task<int></returns>
        public async Task<int> DeleteAsync(int subcategoryId)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                int rowCount = await _unitOfWork.SubcategoryRepository.DeleteAsync(subcategoryId);

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
        /// <param name="subcategoryId">ID unique identifier of Subcategory object that will be updated</param>
        /// <param name="subcategory">Subcategory object with new values</param>
        /// <returns>Task<int></returns>
        public async Task<int> UpdateAsync(int subcategoryId, ISubcategory subcategory)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                int rowCount = await _unitOfWork.SubcategoryRepository.UpdateAsync(subcategoryId, subcategory);

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
        /// <param name="subcategoryId">ID unique identifier of Subcategory object that will be updated</param>
        /// <returns>Task<ISubcategory></returns>
        public async Task<ISubcategory> GetByIDAsync(int subcategoryId)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                IEnumerable<ISubcategory> subcategory = await _unitOfWork.SubcategoryRepository.GetAllAsync(subcategoryId, null);

                await _unitOfWork.CommitAsync();

                return subcategory.FirstOrDefault();
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
        /// <param name="categoryId">Category ID</param>
        /// <returns>Task<ISubcategory></returns>
        public async Task<IEnumerable<ISubcategory>> GetByCategoryIDAsync(int categoryId)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                IEnumerable<ISubcategory> subcategory = await _unitOfWork.SubcategoryRepository.GetAllAsync(null, categoryId);

                await _unitOfWork.CommitAsync();

                return subcategory;
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
                await _unitOfWork.BeginTransactionAsync();

                IEnumerable<ISubcategory> entities = await _unitOfWork.SubcategoryRepository.GetAllAsync(null, null);

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
