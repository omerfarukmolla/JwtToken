using Microsoft.EntityFrameworkCore;
using OFM.WebApi.Data;
using OFM.WebApi.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OFM.WebApi.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductContext _context;

        public ProductRepository(ProductContext context)
        {
            _context = context;
        }

        public async Task<Product> CreateAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetAsync(int id)
        {
            return await _context.Products.AsNoTracking().SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task RemoveAsync(int id)
        {
            var removerdEntity = await _context.Products.FindAsync(id);
            _context.Products.Remove(removerdEntity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            var unchangedEntity = await _context.Products.FindAsync(product.Id); 
            _context.Entry(unchangedEntity).CurrentValues.SetValues(product);
            await _context.SaveChangesAsync();
        }
    }

}
