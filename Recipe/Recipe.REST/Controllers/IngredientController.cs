using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Recipe.Models;
using Recipe.Models.Common;
using Recipe.REST.ViewModels;
using Recipe.REST.ViewModels.Error;
using Recipe.REST.ViewModels.Ingredient;
using Recipe.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Recipe.REST.Controllers
{
    //TODO: add calls for specific filtering like "[HttpGet("{id}/name/{name}")]"
    //TODO: add global response handlers
    //TODO: add verification for row numbers

    [Route("api/[controller]")]
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
                    return StatusCode(StatusCodes.Status204NoContent, result);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ErrorVM { Message = ex.ToString() });
            }
        }

        // GET api/<IngredientController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var result = await _ingredientService.FindByIDAsync(id);

                if (result == null)
                    return StatusCode(StatusCodes.Status404NotFound, new ErrorVM { Message = $"Ingredient with id '{id}' does not exist." });

                //add validations like if it returns empty and stuff like that
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ErrorVM { Message = ex.ToString() });
            }
        }

        // POST api/<IngredientController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] IngredientPostVM ingredient)
        {
            try
            {
                if (!ModelState.IsValid)
                    return StatusCode(StatusCodes.Status400BadRequest, ingredient);

                await _ingredientService.CreateAsync(_mapper.Map<Ingredient>(ingredient));

                return Created($"/{ingredient.Name}", ingredient);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ErrorVM { Message = ex.ToString() });
            }
        }

        // PUT api/<IngredientController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromQuery] int id, [FromBody] IngredientPostVM ingredient)
        {
            try
            {
                if (!ModelState.IsValid)
                    return StatusCode(StatusCodes.Status400BadRequest, ingredient);

                await _ingredientService.UpdateAsync(_mapper.Map<Ingredient>(ingredient));

                return Ok(ingredient);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ErrorVM { Message = ex.ToString() });
            }
        }

        // DELETE api/<IngredientController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            try
            {
                await _ingredientService.DeleteAsync(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ErrorVM { Message = ex.ToString() });
            }
        }
    }
}
