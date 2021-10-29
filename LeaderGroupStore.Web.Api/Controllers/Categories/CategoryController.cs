using AutoMapper;
using LeaderGroupStore.Core.DomainEntities;
using LeaderGroupStore.Models.Categories;
using LeaderGroupStore.Services.Categories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaderGroupStore.Web.Api.Controllers.Categories
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly IMapper mapper;
        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            this.mapper = mapper;
            this.categoryService = categoryService;
        }

        [HttpPost]
        public async Task<IActionResult> AddCategoryAsync([FromBody] CategoryInputModel model)
        {
            var category = mapper.Map<Category>(model);
            var result = categoryService.AddCategoryAsync(category);
            if (result != null)
            {
                return Ok("Category created successfully!");
            }

            return BadRequest(ModelState);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCountriesAsync()
        {
            var countries = await categoryService.GetCategoriesAsync();
            var result = mapper.Map<List<CategoryModel>>(countries);
       
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateCategoryAsync([FromBody] CategoryInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var category = mapper.Map<Category>(model);
            var result = await categoryService.UpdateCategoryAsync(category);
            if (result == 0)
            {
                return BadRequest($"there is no country with id {category.Id}");
            }
            return Ok(result);
        }
    }
}
