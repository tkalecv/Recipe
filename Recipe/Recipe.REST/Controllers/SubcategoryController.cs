using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Recipe.ExceptionHandler.CustomExceptions;
using Recipe.Models;
using Recipe.REST.ViewModels.Subcategory;
using Recipe.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recipe.REST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubcategoryController : Controller
    {
        private readonly ISubcategoryService _subcategoryService;
        private readonly IMapper _mapper;

        public SubcategoryController(ISubcategoryService subcategoryService, IMapper mapper)
        {
            _subcategoryService = subcategoryService;
            _mapper = mapper;
        }

        [HttpGet("/subcategory")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = _mapper.Map<IEnumerable<SubcategoryReturnVM>>(await _subcategoryService.GetAllAsync());

                if (result == null || result.Count() <= 0)
                    throw new HttpStatusCodeException(StatusCodes.Status204NoContent, $"There are no Categories.");

                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("/subcategory/{subcategoryId:int}")]
        public async Task<IActionResult> GetOne(int subcategoryId)
        {
            try
            {
                var result = _mapper.Map<SubcategoryReturnVM>(await _subcategoryService.GetByIDAsync(subcategoryId));

                if (result == null)
                    throw new HttpStatusCodeException(StatusCodes.Status204NoContent, $"There is no Subcategory with id {subcategoryId}.");

                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("subcategory/category/{categoryId:int}")]
        public async Task<IActionResult> GetByCategory(int categoryId)
        {
            try
            {
                var result = _mapper.Map<IEnumerable<Subcategory>>(await _subcategoryService.GetByCategoryIDAsync(categoryId));

                if (result == null || result.Count() <= 0)
                    throw new HttpStatusCodeException(StatusCodes.Status204NoContent, $"There is no Subcategories for category with id {categoryId}.");

                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SubcategoryPostPutVM subcategory)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new HttpStatusCodeException(StatusCodes.Status400BadRequest, subcategory);

                await _subcategoryService.CreateAsync(_mapper.Map<Subcategory>(subcategory));

                return Ok(subcategory);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] SubcategoryPostPutVM subcategory)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new HttpStatusCodeException(StatusCodes.Status400BadRequest, subcategory);

                await _subcategoryService.UpdateAsync(id, _mapper.Map<Subcategory>(subcategory));

                return Ok(subcategory);
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
                await _subcategoryService.DeleteAsync(id);

                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
