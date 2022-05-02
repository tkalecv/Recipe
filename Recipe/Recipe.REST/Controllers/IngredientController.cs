using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Recipe.Models;
using Recipe.Models.Common;
using Recipe.REST.ViewModels;
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
        public async Task<IEnumerable<IngredientPostVM>> Get()
        {
            try
            {
                var result = _mapper.Map<IEnumerable<IngredientPostVM>>(await _ingredientService.GetAllAsync());

                if(result == null || result.Count() <= 0)
                    return HttpResponseException

                var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
                {

                };

                return;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // GET api/<IngredientController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id) //TODO: is it better to use IActionResult?
        {
            try
            {
                var result = await _ingredientService.FindByIDAsync(id);

                if (result == null)
                    return StatusCode(StatusCodes.Status404NotFound, new { message = $"Ingredient with id '{id}' does not exist." }); //TODO: create model for respose with message, datetime now...

                //add validations like if it returns empty and stuff like that
                return StatusCode(StatusCodes.Status200OK, result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { message = ex });
            }
        }

        // POST api/<IngredientController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] IngredientPostVM ingredient)
        {
            //if (!ModelState.IsValid)               
            try
            {
                if (!ModelState.IsValid)
                    return StatusCode(StatusCodes.Status400BadRequest, ingredient);

                return StatusCode(StatusCodes.Status201Created, await _ingredientService.CreateAsync(_mapper.Map<Ingredient>(ingredient)));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { message = ex });
            }
        }

        // PUT api/<IngredientController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<IngredientController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
