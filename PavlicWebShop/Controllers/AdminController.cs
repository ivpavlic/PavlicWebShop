using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PavlicWebShop.Models;
using PavlicWebShop.Models.Binding;
using PavlicWebShop.Services.Interface;

namespace PavlicWebShop.Controllers
{
    [Authorize(Roles = Roles.Admin)]
    public class AdminController : Controller
    {
        private readonly IProductService productService;
        private readonly IMapper mapper;

        public AdminController(IProductService productService, IMapper mapper)
        {
            this.mapper = mapper;
            this.productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> ProductList()
        {
            var products = await productService.GetProducts();
            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> AddProduct()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductBinding model)
        {
            await productService.AddProduct(model);
            return RedirectToAction("ProductList");
        }

        [HttpGet]
        public async Task<IActionResult> AddProductCategory()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddProductCategory(ProductCategoryBinding model)
        {
            await productService.AddProductCategory(model);
            return RedirectToAction("ProductList");
        }
    }
}
