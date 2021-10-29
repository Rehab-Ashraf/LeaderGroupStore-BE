using LeaderGroupStore.Core.DomainEntities;
using LeaderGroupStore.Repositories.Categories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LeaderGroupStore.Services.Categories
{
    public class CategoriesService : ICategoryService
    {
        private readonly ICategoriesRepostiory categoriesRepostiory;
        public CategoriesService(ICategoriesRepostiory categoriesRepostiory)
        {
            this.categoriesRepostiory = categoriesRepostiory;
        }
        public Task<int> AddCategoryAsync(Category category)
        {
            return categoriesRepostiory.AddCategoryAsync(category);
        }

        public Task<List<Category>> GetCategoriesAsync()
        {
            return categoriesRepostiory.GetCategoriesAsync();
        }

        public Task<Category> GetCategoryByIdAsync(int Id)
        {
            return categoriesRepostiory.GetCategoryByIdAsync(Id);
        }

        public Task<int> UpdateCategoryAsync(Category Category)
        {
            return categoriesRepostiory.UpdateCategoryAsync(Category);
        }
    }
}
