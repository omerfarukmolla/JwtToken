using OFM.WebApi.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OFM.WebApi.Interfaces
{
    public interface IProductRepository
    {
        public Task<List<Product>> GetAllAsync();

        public Task<Product> GetAsync(int id);
        public Task<Product> CreateAsync(Product product);

        public Task UpdateAsync(Product product);
        public Task RemoveAsync(int id);
    }
}
