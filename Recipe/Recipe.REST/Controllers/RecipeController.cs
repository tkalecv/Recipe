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

        // GET: api/<RecipeController>
        [HttpGet("/recipe/user/{userId:int}")]
        public async Task<IActionResult> GetByUserId(int userId)
        {
            try
            {
                var result = _mapper.Map<IEnumerable<RecipeReturnVM>>(await _recipeService.GetAllAsync(userId));

                if (result == null || result.Count() <= 0)
                    throw new HttpStatusCodeException(StatusCodes.Status204NoContent, $"There are no Recipes for user with id {userId}.");

                return Ok(result); //TODO: subcategory and userdata are empty
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // GET: api/<RecipeController>
        [HttpGet("/recipe")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = _mapper.Map<IEnumerable<RecipeReturnVM>>(await _recipeService.GetAllAsync(null));

                if (result == null || result.Count() <= 0)
                    throw new HttpStatusCodeException(StatusCodes.Status204NoContent, $"There are no Recipes.");

                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // GET: api/<RecipeController>
        [HttpGet("/recipe/{recipeId:int}/user/{userId:int}")]
        public async Task<IActionResult> GetOne([FromQuery] int recipeId, [FromQuery] int userId) //TODO: pass userid
        {
            try
            {
                var result = _mapper.Map<IEnumerable<RecipeReturnVM>>(await _recipeService.GetAllAsync(userId));

                if (result == null || result.Count() <= 0)
                    throw new HttpStatusCodeException(StatusCodes.Status204NoContent, $"There is no Recipe with id {recipeId} for user with id {userId}.");

                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //TODO: add authorize attribute
        // POST api/<RecipeController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RecipePostPutVM recipe)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new HttpStatusCodeException(StatusCodes.Status400BadRequest, recipe);

                await _recipeService.CreateAsync(_mapper.Map<Models.Recipe>(recipe)); //TODO: check automapper, how to map classes

                return Ok(recipe);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // PUT api/<RecipeController>/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put([FromQuery] int id, [FromBody] RecipePostPutVM recipe)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new HttpStatusCodeException(StatusCodes.Status400BadRequest, recipe);

                await _recipeService.UpdateAsync(id, _mapper.Map<IRecipe>(recipe));

                return Ok(recipe);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // DELETE api/<RecipeController>/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            try
            {
                await _recipeService.DeleteAsync(id);

                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
