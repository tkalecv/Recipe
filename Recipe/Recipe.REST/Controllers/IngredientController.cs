using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Recipe.ExceptionHandler.CustomExceptions;
using Recipe.Models;
using Recipe.REST.ViewModels.Ingredient;
using Recipe.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Recipe.REST.Controllers
{
    //NOTE: You can add calls for specific filtering like "[HttpGet("{id}/name/{name}")]"
   // or like You can also use it like this [Route("/[area]/[controller]/[action]?id={id}")]

    [Route("api/ingredient")]
    [ApiController]
    public class IngredientController : ControllerBase
    {
        private readonly IIngredientService _ingredientService;
        private readonly IMapper _mapper;

        public IngredientController(IIngredientService ingredientService, IMapper mapper)
        {
            _ingredientService = ingredientService;
            _mapper = mapper;
        }

        // GET: api/<IngredientController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = _mapper.Map<IEnumerable<IngredientVM>>(await _ingredientService.GetAllAsync());

                if (result == null || result.Count() <= 0)
                    throw new HttpStatusCodeException(StatusCodes.Status204NoContent, "There are no Ingredients.");

                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // GET api/<IngredientController>/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var result = await _ingredientService.FindByIDAsync(id);

                if (result == null)
                    throw new HttpStatusCodeException(StatusCodes.Status404NotFound, $"Ingredient with id '{id}' does not exist.");

                //add validations like if it returns empty and stuff like that
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // POST api/<IngredientController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] IngredientPostVM ingredient)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new HttpStatusCodeException(StatusCodes.Status400BadRequest, ingredient);

                await _ingredientService.CreateAsync(_mapper.Map<Ingredient>(ingredient));

                return Created($"/{ingredient.Name}", ingredient);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // PUT api/<IngredientController>/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put([FromQuery] int id, [FromBody] IngredientPostVM ingredient)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new HttpStatusCodeException(StatusCodes.Status400BadRequest, ingredient);

                await _ingredientService.UpdateAsync(id, _mapper.Map<Ingredient>(ingredient));

                return Ok(ingredient);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // DELETE api/<IngredientController>/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            try
            {
                await _ingredientService.DeleteAsync(id);

                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
