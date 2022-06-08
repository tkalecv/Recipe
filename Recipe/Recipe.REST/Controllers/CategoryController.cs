using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Recipe.ExceptionHandler.CustomExceptions;
using Recipe.Models;
using Recipe.Models.Common;
using Recipe.REST.ViewModels.Category;
using Recipe.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recipe.REST.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet("/category"), AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = _mapper.Map<IEnumerable<Category>>(await _categoryService.GetAllAsync());

                if (result == null || result.Count() <= 0)
                    throw new HttpStatusCodeException(StatusCodes.Status204NoContent, $"There are no Categories.");

                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("/category/{categoryId:int}"), AllowAnonymous]
        public async Task<IActionResult> GetOne(int categoryId)
        {
            try
            {
                var result = _mapper.Map<Category>(await _categoryService.GetByIdAsync(categoryId));

                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CategoryPostPutVM category)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new HttpStatusCodeException(StatusCodes.Status400BadRequest, category);

                await _categoryService.CreateAsync(_mapper.Map<Category>(category));

                return Ok(category);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] CategoryPostPutVM category)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new HttpStatusCodeException(StatusCodes.Status400BadRequest, category);

                await _categoryService.UpdateAsync(id, _mapper.Map<Category>(category));

                return Ok(category);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _categoryService.DeleteAsync(id);

                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
