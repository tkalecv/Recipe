using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Recipe.Models;
using Recipe.Models.Common;
using Recipe.REST.ViewModels;
using Recipe.Service.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Recipe.REST.Controllers
{
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
                return _mapper.Map<IEnumerable<IngredientPostVM>>(await _ingredientService.GetAllAsync());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // GET api/<IngredientController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<IngredientController>
        [HttpPost]
        public async Task Post([FromBody] IngredientPostVM ingredient)
        {
            //if (!ModelState.IsValid)               
            try
            {
                await _ingredientService.CreateAsync(_mapper.Map<Ingredient>(ingredient));
            }
            catch (Exception ex)
            {
                throw ex;
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
