using AutoMapper;
using LeaderGroupStore.Core.DomainEntities;
using LeaderGroupStore.Models.Products;
using LeaderGroupStore.Services.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaderGroupStore.Web.Api.Controllers.Products
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IMapper mapper;
        private readonly IProductService productService;
        public ProductController(IProductService  productService, IMapper mapper)
        {
            this.mapper = mapper;
            this.productService = productService;
        }
        [Authorize(Policy  = "Admin")]
        [Authorize(Policy = "Manager")]
        [HttpPost]
        public async Task<IActionResult> AddCategoryAsync([FromBody] ProductInoutModel model)
        {
            var product = mapper.Map<Product>(model);
            var result = productService.AddProductAsync(product);
            if (result != null)
            {
                return Ok("product created successfully!");
            }

            return BadRequest(ModelState);
        }


        [Authorize(Roles = "Admin")]
        [Authorize(Roles = "Manager")]
        [Authorize(Roles = "Customer")]
        [HttpGet]
        public async Task<IActionResult> AllCategoriesAsync()
        {
            var products = await productService.GetAllProductsAsync();
            var result = mapper.Map<List<ProductInoutModel>>(products);

            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [Authorize(Policy = "Manager")]
        [HttpPut]
        public async Task<IActionResult> UpdateCategoryAsync([FromBody] ProductInoutModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var product = mapper.Map<Product>(model);
            var result = await productService.UpdateProduct(product);
            if (result == 0)
            {
                return BadRequest($"there is no country with id {product.Id}");
            }
            return Ok(result);
        }

    }
}
