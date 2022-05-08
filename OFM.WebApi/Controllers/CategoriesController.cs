using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OFM.WebApi.Data;
using System.Linq;

namespace OFM.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ProductContext _context;

        public CategoriesController(ProductContext context)
        {
            _context = context;
        }

        [HttpGet("{id}/products")]
        public IActionResult GetWithProducts(int id)
        {
            var categoryWithProducts = _context.Categories.Include(x => x.Products).SingleOrDefault(x => x.Id == id);
            if(categoryWithProducts == null)return NotFound(id);
            return Ok(categoryWithProducts);
        }
    }
}
