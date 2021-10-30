using LeaderGroupStore.Core.DomainEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LeaderGroupStore.Repositories.Products
{
    public class ProductRepository : IProductRepository
    {
        private readonly LeaderGroupStore_dbContext dbContext;
        public ProductRepository(LeaderGroupStore_dbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<int> AddProductAsync(Product product)
        {
            product.CreateAt();
            var category = await dbContext.Categories.FindAsync(product.Category.Id);
            product.UpdateCategory(category);
            await dbContext.Products.AddAsync(product);
            await dbContext.SaveChangesAsync();
            return product.Id;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await dbContext.Products.ToListAsync();
        }

        public Task<Product> GetProductById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateProduct(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
