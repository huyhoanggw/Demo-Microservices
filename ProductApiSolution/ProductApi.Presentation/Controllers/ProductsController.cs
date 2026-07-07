using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Application.DTOs;
using ProductApi.Application.DTOs.Conversion;
using ProductApi.Application.Interfaces;
using ProductApi.Domain.Entities;

namespace ProductApi.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class ProductsController : Controller
    {
        private readonly IProduct _repository;

        public ProductsController(IProduct repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _repository.GetAllAsync();
            var (_, list) = ProductConversion.FromEntity(null!, products);
            if (!products.Any())
            {
                return NotFound("No Products detected in the database");
            }
            return list!.Any() ? Ok(list) : NotFound("No product found");
        }
        [HttpGet("{id}")] 
        public async Task<ActionResult<Product>> GetProduct(int id) 
        {
            var product = await _repository.FindByIdAsync(id);
            var (productEntity, list) = ProductConversion.FromEntity(product, null!);
            if (product is null) return NotFound("No Products detected in the database");
            return productEntity is not null ? Ok(product) : NotFound("No Product not found");
        }
        [HttpPost]
        [Authorize(Roles ="Admin")]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] ProductDTO request) 
        {   
            if(!ModelState.IsValid) return BadRequest(ModelState);
            var productEntity = ProductConversion.ToEntity(request);
            var result = await _repository.CreateAsync(productEntity);
            return result.Flag ? Ok(result) : NotFound(result.message);
        }
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Product>> UpdateProduct([FromBody] ProductDTO request) 
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var productEntity = ProductConversion.ToEntity(request);
            var result = await _repository.UpdateAsync(productEntity);
            return result.Flag ? Ok(result) : NotFound(result.message);
        }
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Product>> DeleteProduct(int id) 
        {
            var result = await _repository.DeleteAsync(id);
            return result.Flag ? Ok(result) : NotFound(result.message);
        }
    }
}
