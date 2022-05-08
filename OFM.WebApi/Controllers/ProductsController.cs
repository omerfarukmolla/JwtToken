using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OFM.WebApi.Data;
using OFM.WebApi.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace OFM.WebApi.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _productRepository.GetAllAsync();
            return Ok(result);

        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _productRepository.GetAsync(id);
            if (data == null)
            {
                return NotFound(id);
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProudct(Product product)
        {
            var addedProduct = await _productRepository.CreateAsync(product);
            return Created("", product);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProudct(Product product)
        {
            var checkproduct = await _productRepository.GetAsync(product.Id);
            if (checkproduct == null) return NotFound(product.Id);
            await _productRepository.UpdateAsync(product);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProudct(int id)
        {
            var product = await _productRepository.GetAsync(id);
            if (product == null) return NotFound(id);
            await _productRepository.RemoveAsync(id);
            return NoContent();
        }
        [HttpPost("upload")]
        public async Task<IActionResult> upload(IFormFile formFile)
        {
            var newName = Guid.NewGuid() + "." + Path.GetExtension(formFile.FileName);
            var path = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot",newName);
            var stream = new FileStream(path, FileMode.Create);
            await formFile.CopyToAsync(stream);
            return Created(string.Empty, formFile);

        }

    }
}
