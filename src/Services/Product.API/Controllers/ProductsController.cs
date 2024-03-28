using AutoMapper;
using Contracts.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Product.API.Entities;
using Product.API.Repositories.Interface;
using Shared.DTOs.Product;
using System.ComponentModel.DataAnnotations;

namespace Product.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public ProductsController(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        #region CRUD
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var result = await _repository.FindAll().ToArrayAsync();
            return Ok(result);
        }

        [HttpGet("get-product/{id:long}")]
        public async Task<IActionResult> GetProduct([Required]long id)
        {
            var product = await _repository.GetProduct(id);
            if(product == null)
                return NotFound();
            var result = _mapper.Map<ProductDto>(product);
            return Ok(result);
        }

        [HttpPost("create-product")]
        public async Task<IActionResult> CreateProducts([FromBody]CreateProductDto createProductDto)
        {
            var product = _mapper.Map<CatalogProduct>(createProductDto);
            await _repository.CreateProduct(product);
            await _repository.SaveChangesAsync();
            var result = _mapper.Map<ProductDto>(product);
            return Ok(result);
        }

        [HttpPut("update-product/{id:long}")]
        public async Task<IActionResult> UpdateProducts([Required] long id, [FromBody] UpdateProductDto updateProductDto)
        {
            var product = await _repository.GetProduct(id);
            if (product == null)
                return NotFound();
            var updateProduct = _mapper.Map(updateProductDto,product);
            await _repository.UpdateProduct(updateProduct);
            await _repository.SaveChangesAsync();
            var result = _mapper.Map<ProductDto>(product);
            return Ok(result);
        }

        [HttpDelete("delete-product/{id:long}")]
        public async Task<IActionResult> DeleteProduct([Required] long id)
        {
            var product = await _repository.GetProduct(id);
            if (product == null)
                return NotFound();
            
            await _repository.DeleteProduct(id);
            await _repository.SaveChangesAsync();
           
            return NoContent();
        }

        [HttpGet("get-product-by-no/{productNo}")]
        public async Task<IActionResult> GetProductByNo([Required] string productNo)
        {
            var product = await _repository.GetProductByNo(productNo);
            if (product == null)
                return NotFound();
            var result = _mapper.Map<ProductDto>(product);
            return Ok(result);
        }
        #endregion
    }
}
