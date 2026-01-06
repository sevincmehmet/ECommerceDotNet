using Business.Abstract.Products;
using Business.DTOs;
using Business.DTOs.Product;
using Business.DTOs.Users;
using Infrastructure.Security.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation_API.Controllers.Base;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Presentation_API.Controllers.Products
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : BaseController
    {
        IBrandService _brandService;

        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        // POST api/<BrandController>
        [HttpPost]
        [HasPermission("Product.Create")]
        public IActionResult Post([FromBody] BrandDto dto)
        {
            var result = _brandService.AddBrand(dto);
            return HandleDataResult(result, successStatusCode: 201, errorStatusCode: 400);
        }

        // PUT api/<BrandController>/5
        [HttpPut("{id}")]
        [Authorize(Roles ="Admin")]
        public IActionResult Put(int id, [FromBody] BrandDto dto)
        {
            var result = _brandService.UpdateBrand(id, dto);
            return HandleDataResult(result, successStatusCode: 201, errorStatusCode: 400);
        }

        // DELETE api/<BrandController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var result = _brandService.DeleteBrand(id);
            return HandleResult(result, successStatusCode: 201, errorStatusCode: 400);
        }
    }
}
