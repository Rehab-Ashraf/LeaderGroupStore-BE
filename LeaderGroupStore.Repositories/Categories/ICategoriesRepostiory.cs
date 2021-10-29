using LeaderGroupStore.Core.DomainEntities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LeaderGroupStore.Repositories.Categories
{
    public interface ICategoriesRepostiory
    {
        Task<int> AddCategoryAsync(Category category);
        Task<List<Category>> GetCategoriesAsync();
        Task<Category> GetCategoryByIdAsync(int Id);

        Task<int> UpdateCategoryAsync(Category Category);
    }
}
