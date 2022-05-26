using AutoMapper;
using Recipe.Models;
using Recipe.Models.Common;
using Recipe.Repository.Common;
using Recipe.Repository.Common.UnitOfWork;
using Recipe.Repository.UnitOfWork;
using Recipe.Service.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Recipe.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Method creates new Category entry in db
        /// </summary>
        /// <param name="category">Category object that will be created</param>
        /// <returnsTask<int>></returns>
        public async Task<int> CreateAsync(ICategory category)
        {
            try
            {
                int rowCount = await _unitOfWork.CategoryRepository.CreateAsync(_mapper.Map<Category>(category));

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
        /// Method creates multiple Category entries in db
        /// </summary>
        /// <param name="entities">List of Category objects that will be created</param>
        /// <returns>Task<int></returns>
        public async Task<int> CreateAsync(IEnumerable<ICategory> entities)
        {
            try
            {
                int rowCount = await _unitOfWork.CategoryRepository.CreateAsync(_mapper.Map<IEnumerable<Category>>(entities));

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
        /// Method removes Category entry from db
        /// </summary>
        /// <param name="category">Category object that will be removed</param>
        /// <returns>Task<int></returns>
        public async Task<int> DeleteAsync(ICategory category)
        {
            try
            {
                int rowCount = await _unitOfWork.CategoryRepository.DeleteAsync(_mapper.Map<Category>(category));

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
        /// Method removes Category entry from db
        /// </summary>
        /// <param name="id">ID unique identifier of Category object that will be removed</param>
        /// <returns>Task<int></returns>
        public async Task<int> DeleteAsync(int id)
        {
            try
            {
                var category = await _unitOfWork.CategoryRepository.GetByIdAsync(id);

                int rowCount = await _unitOfWork.CategoryRepository.DeleteAsync(category);

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
        /// Method updates Category entry in db
        /// </summary>
        /// <param name="category">Category object that will be updated</param>
        /// <returns>Task<int></returns>
        public async Task<int> UpdateAsync(ICategory category)
        {
            try
            {
                int rowCount = await _unitOfWork.CategoryRepository.UpdateAsync(_mapper.Map<Category>(category));

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
        /// Method updates Category entry in db
        /// </summary>
        /// <param name="id">ID unique identifier of Category object that will be updated</param>
        /// <param name="category">Category object with new values</param>
        /// <returns>Task<int></returns>
        public async Task<int> UpdateAsync(int id, ICategory category)
        {
            try
            {
                category.CategoryID = id;

                int rowCount = await _unitOfWork.CategoryRepository.UpdateAsync(_mapper.Map<Category>(category));

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
        /// Method retrieves Category entry from db filtered by ID unique identifier
        /// </summary>
        /// <param name="id">ID unique identifier of Category object that will be updated</param>
        /// <returns>Task<ICategory></returns>
        public async Task<ICategory> FindByIDAsync(int id)
        {
            try
            {
                ICategory category = await _unitOfWork.CategoryRepository.GetByIdAsync(id);

                await _unitOfWork.CommitAsync();

                return category;
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();

                throw ex;
            }
        }

        /// <summary>
        /// Method retrieves all of Category entries from db
        /// </summary>
        /// <returns>Task<IEnumerable<ICategory>></returns>
        public async Task<IEnumerable<ICategory>> GetAllAsync()
        {
            try
            {
                IEnumerable<ICategory> entities = await _unitOfWork.CategoryRepository.GetAllAsync();

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
