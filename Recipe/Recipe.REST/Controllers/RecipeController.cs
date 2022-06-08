using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Recipe.ExceptionHandler.CustomExceptions;
using Recipe.Models.Common;
using Recipe.REST.ViewModels.Recipe;
using Recipe.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Recipe.REST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeService _recipeService;
        private readonly IMapper _mapper;

        public RecipeController(IRecipeService recipeService, IMapper mapper)
        {
            _recipeService = recipeService;
            _mapper = mapper;
        }

        [HttpGet("/recipe/user/{userId:int}")]
        public async Task<IActionResult> GetByUserId(int userId)
        {
            try
            {
                var result = _mapper.Map<IEnumerable<RecipeReturnVM>>(await _recipeService.GetByUserIDAsync(userId));

                if (result == null || result.Count() <= 0)
                    throw new HttpStatusCodeException(StatusCodes.Status204NoContent, $"There are no Recipes for user with id {userId}.");

                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("/recipe")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = _mapper.Map<IEnumerable<RecipeReturnVM>>(await _recipeService.GetAllAsync());

                if (result == null || result.Count() <= 0)
                    throw new HttpStatusCodeException(StatusCodes.Status204NoContent, $"There are no Recipes.");

                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("/recipe/{recipeId:int}")]
        public async Task<IActionResult> GetOne([FromQuery] int recipeId)
        {
            try
            {
                var result = _mapper.Map<IEnumerable<RecipeReturnVM>>(await _recipeService.GetByIdAsync(recipeId));

                if (result == null || result.Count() <= 0)
                    throw new HttpStatusCodeException(StatusCodes.Status204NoContent, $"There is no Recipe with id {recipeId}.");

                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //TODO: add authorize attribute
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RecipePostPutVM recipe)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new HttpStatusCodeException(StatusCodes.Status400BadRequest, recipe);

                await _recipeService.CreateAsync(_mapper.Map<Models.Recipe>(recipe));

                return Ok(recipe);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut("{recipeId:int}")]
        public async Task<IActionResult> Put([FromQuery] int recipeId, [FromBody] RecipePostPutVM recipe)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new HttpStatusCodeException(StatusCodes.Status400BadRequest, recipe);

                await _recipeService.UpdateAsync(recipeId, _mapper.Map<IRecipe>(recipe));

                return Ok(recipe);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpDelete("{recipeId:int}")]
        public async Task<IActionResult> Delete([FromQuery] int recipeId)
        {
            try
            {
                await _recipeService.DeleteByIDAsync(recipeId);

                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpDelete("/user/{id:int}")]
        public async Task<IActionResult> DeleteAllUserRecipes([FromQuery] int userId)
        {
            try
            {
                await _recipeService.DeleteAllUserRecipesAsync(userId);

                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpDelete("/recipe/{recipeId:int}/user/{userId:int}")]
        public async Task<IActionResult> DeleteUserRecipe([FromQuery] int recipeId, [FromQuery] int userId)
        {
            try
            {
                await _recipeService.DeleteUserSpecificRecipeAsync(recipeId, userId);

                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
