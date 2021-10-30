using LeaderGroupStore.Core.DomainEntities;
using LeaderGroupStore.Repositories.Products;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LeaderGroupStore.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;
        public ProductService(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }
        public Task<int> AddProductAsync(Product product)
        {
            return productRepository.AddProductAsync(product);
        }

        public Task<List<Product>> GetAllProductsAsync()
        {
            return productRepository.GetAllProductsAsync();
        }

        public Task<Product> GetProductById(int id)
        {
            return productRepository.GetProductById(id);
        }

        public Task<int> UpdateProduct(Product product)
        {
            return productRepository.UpdateProduct(product);
        }
    }
}
