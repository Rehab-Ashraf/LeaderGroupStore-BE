using LeaderGroupStore.Core.DomainEntities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LeaderGroupStore.Services.Products
{
    public interface IProductService
    {
        Task<int> AddProductAsync(Product product);
        Task<List<Product>> GetAllProductsAsync();
        Task<Product> GetProductById(int id);
        Task<int> UpdateProduct(Product product);
    }
}
