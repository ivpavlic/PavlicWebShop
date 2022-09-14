using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PavlicWebShop.Models.ViewModel;
using PavlicWebShop.Services.Interface;

namespace PavlicWebShop.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductApiController : ControllerBase
    {
        private readonly IProductService productService;
        private readonly IMapper mapper;

        public ProductApiController(IProductService productService, IMapper mapper)
        {
            this.mapper = mapper;
            this.productService = productService;
        }

        [HttpGet]
        [Route("products")]
        public async Task<IActionResult> Products()
        {
            return Ok(await productService.GetProducts());
        }

        [HttpGet]
        [Route("product/{id}")]
        [ProducesResponseType(typeof(ProductViewModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProductAsync(int id)
        {
            return Ok(await productService.GetProduct(id));
        }
    }
}
