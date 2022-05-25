using Recipe.Models.Common;
using Recipe.Repository.UnitOfWork;
using Recipe.Service.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Recipe.Service
{
    public class RecipeService : IRecipeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RecipeService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Method creates new Recipe entry in db
        /// </summary>
        /// <param name="recipe">Recipe object that will be created</param>
        /// <returnsTask<int>></returns>
        public async Task<int> CreateAsync(IRecipe recipe)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                int rowCount = await _unitOfWork.RecipeRepository.CreateAsync(recipe);

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
        /// Method creates multiple Recipe entries in db
        /// </summary>
        /// <param name="entities">List of Recipe objects that will be created</param>
        /// <returns>Task<int></returns>
        public async Task<int> CreateAsync(IEnumerable<IRecipe> recipes)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                int rowCount = await _unitOfWork.RecipeRepository.CreateAsync(recipes);

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
        /// Method removes Recipe entry from db
        /// </summary>
        /// <param name="recipe">Recipe object that will be removed</param>
        /// <returns>Task<int></returns>
        public async Task<int> DeleteAsync(IRecipe recipe)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                int rowCount = await _unitOfWork.RecipeRepository.DeleteAsync(recipe);

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
        /// Method removes Recipe entry from db
        /// </summary>
        /// <param name="id">ID unique identifier of Recipe object that will be removed</param>
        /// <returns>Task<int></returns>
        public async Task<int> DeleteAsync(int id)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                IRecipe recipe = await _unitOfWork.RecipeRepository.GetByIdAsync(id);

                int rowCount = await _unitOfWork.RecipeRepository.DeleteAsync(recipe);

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
        /// Method updates Recipe entry in db
        /// </summary>
        /// <param name="recipe">Recipe object that will be updated</param>
        /// <returns>Task<int></returns>
        public async Task<int> UpdateAsync(IRecipe recipe)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                int rowCount = await _unitOfWork.RecipeRepository.UpdateAsync(recipe);

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
        /// Method updates Recipe entry in db
        /// </summary>
        /// <param name="id">ID unique identifier of Recipe object that will be updated</param>
        /// <param name="recipe">Recipe object with new values</param>
        /// <returns>Task<int></returns>
        public async Task<int> UpdateAsync(int id, IRecipe recipe)
        {
            try
            {
                recipe.RecipeID = id;

                await _unitOfWork.BeginTransactionAsync();

                int rowCount = await _unitOfWork.RecipeRepository.UpdateAsync(recipe);

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
        /// Method retrieves Recipe entry from db filtered by ID unique identifier
        /// </summary>
        /// <param name="id">ID unique identifier of Recipe object that will be updated</param>
        /// <returns>Task<IRecipe></returns>
        public async Task<IRecipe> FindByIDAsync(int id)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                IRecipe recipe = await _unitOfWork.RecipeRepository.GetByIdAsync(id);

                await _unitOfWork.RecipeRepository.DeleteAsync(recipe);

                await _unitOfWork.CommitAsync();

                return recipe;
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();

                throw ex;
            }
        }

        /// <summary>
        /// Method retrieves all of Recipe entries from db
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns>Task<IEnumerable<IRecipe>></returns>
        public async Task<IEnumerable<IRecipe>> GetAllAsync(int? userId)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                IEnumerable<IRecipe> entities = await _unitOfWork.RecipeRepository.GetAllAsync(userId);

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
