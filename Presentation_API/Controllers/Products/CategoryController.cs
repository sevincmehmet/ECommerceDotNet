using Business.Abstract.Products;
using Business.DTOs;
using Business.DTOs.Product;
using Business.DTOs.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation_API.Controllers.Base;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Presentation_API.Controllers.Products
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : BaseController
    {

        ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // POST api/Category
        [HttpPost]
        [Authorize]
        [ProducesResponseType(typeof(ApiResponseDto<object>), 201)]
        [ProducesResponseType(typeof(ApiResponseDto<object>), 400)]
        public IActionResult Post([FromBody] CategoriyAddDto dto)
        {
            var result = _categoryService.AddCategory(dto);
            return HandleDataResult(result, successStatusCode: 201, errorStatusCode: 400);
        }

        // PUT api/Category
        [HttpPut("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(ApiResponseDto<object>), 200)]
        [ProducesResponseType(typeof(ApiResponseDto<object>), 400)]
        public IActionResult Put(int id, [FromBody] CategoriyUpdateDto dto)
        {
            var result = _categoryService.UpdateCategory(id, dto);
            return HandleDataResult(result, successStatusCode: 200, errorStatusCode: 400);
        }

        // DELETE api/Category
        [HttpDelete("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(ApiResponseDto<object>), 200)]
        [ProducesResponseType(typeof(ApiResponseDto<object>), 404)]
        public IActionResult Delete(int id)
        {
            var result = _categoryService.DeleteCategory(id);
            return HandleResult(result, successStatusCode: 200, errorStatusCode: 404);
        }

    }
}
