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
        [Authorize]
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

    }
}
