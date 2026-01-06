using Business.Abstract.Products;
using Business.DTOs;
using Business.DTOs.Product;
using Infrastructure.Result.Abstract;
using Infrastructure.Security.Attributes;
using Microsoft.AspNetCore.Mvc;
using Presentation_API.Controllers.Base;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Presentation_API.Controllers.Products
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseController
    {
        IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: api/Product
        [HttpGet]
        public IActionResult Get()
        {
            IDataResult<List<ProductDto>> result = _productService.GetAll();
            return HandleDataResult(result, successStatusCode: 200, errorStatusCode: 404);
        }

        // POST api/Product
        [HttpPost]
        [HasPermission("Product.FullControl")]
        public IActionResult Post([FromBody] ProductAddDto dto)
        {
            IDataResult<ProductAddDto> result = _productService.CreateProduct(dto);
            return HandleDataResult(result, successStatusCode: 201, errorStatusCode: 400);
        }

        // PUT api/Product/5
        [HttpPut("{id}")]
        [HasPermission("Product.FullControl")]
        public IActionResult Put(int id, ProductUpdateDto dto)
        {
            IDataResult<ProductUpdateDto> result = _productService.UpdateProduct(id, dto);
            return HandleDataResult(result, successStatusCode: 200, errorStatusCode: 400);
        }



        // DELETE api/Product/5
        [HttpDelete("{id}")]
        [HasPermission("Product.FullControl")]
        public IActionResult Delete(int id)
        {
            var result = _productService.DeleteProduct(id);
            return HandleResult(result, 200, 404);
        }

        // MY ISTEKLERI
        // POST api/my/Product
        [HttpPost("my")]
        [HasPermission("Product.FullControl")]
        public IActionResult PostMyProduct([FromBody] ProductAddDto dto)
        {
            IDataResult<ProductAddDto> result = _productService.CreateProduct(dto);
            return HandleDataResult(result, successStatusCode: 201, errorStatusCode: 400);
        }

        // PUT api/Product/5
        [HttpPut("my")]
        [HasPermission("Product.FullControl")]
        public IActionResult PutMyProduct(int id, ProductUpdateDto dto)
        {
            IDataResult<ProductUpdateDto> result = _productService.UpdateProduct(id, dto);
            return HandleDataResult(result, successStatusCode: 200, errorStatusCode: 400);
        }

        // DELETE api/Product/5
        [HttpDelete("my")]
        [HasPermission("Product.FullControl")]
        public IActionResult DeleteMyProduct(int id)
        {
            var result = _productService.DeleteProduct(id);
            return HandleResult(result, 200, 404);
        }

        // GET api/products/search
        [HttpGet("search")]
        [ProducesResponseType(typeof(ApiResponseDto<List<ProductDto>>), 200)]
        [ProducesResponseType(typeof(ApiResponseDto<object>), 404)]
        public IActionResult GetSearchProduct()
        {
            // şuanlık tüm ürünleri dönüyor.. 
            /* 
             {
                Name: []
                Description: []    
                Brand: []
             }
             */
            IDataResult<List<ProductDto>> result = _productService.GetAll();
            return HandleDataResult(result, successStatusCode: 200, errorStatusCode: 404);
        }
    }
}
