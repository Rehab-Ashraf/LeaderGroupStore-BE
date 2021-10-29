using LeaderGroupStore.Core.DomainEntities;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

namespace LeaderGroupStore.Repositories.Categories
{
    public class CategoriesRepository : ICategoriesRepostiory
    {
        private readonly LeaderGroupStore_dbContext _LeaderContext;
        public CategoriesRepository(LeaderGroupStore_dbContext context)
        {
            _LeaderContext = context;
        }
        public async Task<int> AddCategoryAsync(Category category)
        {
            await _LeaderContext.Categories.AddAsync(category);
            var result = await _LeaderContext.SaveChangesAsync();
            return category.Id;
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            return await _LeaderContext.Categories.ToListAsync();
        }

        public Task<Category> GetCategoryByIdAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<int> UpdateCategoryAsync(Category Category)
        {
            var countryToBeUpdate = await _LeaderContext.Categories.Where(c => c.Id == Category.Id).AsNoTracking().FirstOrDefaultAsync();
            if (countryToBeUpdate == null)
            {
                return 0;
            }

            await _LeaderContext.SaveChangesAsync();
            return Category.Id;
        }
    }
}
