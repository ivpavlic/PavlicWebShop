using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PavlicWebShop.Models.Binding;
using PavlicWebShop.Services.Interface;

namespace PavlicWebShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService productService;
        private readonly IMapper mapper;

        public ProductController(IProductService productService, IMapper mapper)
        {
            this.mapper = mapper;
            this.productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> SuspendOrder(int id)
        {
            var order = await productService.SuspendOrder(id);
            return RedirectToAction("Orders");
        }


        [HttpGet]
        public async Task<IActionResult> Order(int id)
        {
            var order = await productService.GetOrder(id);
            return View(order);
        }


        [HttpGet]
        public async Task<IActionResult> Orders()
        {
            var orders = await productService.GetOrders();
            return View(orders);
        }


        [HttpGet]
        public async Task<IActionResult> ProductsList()
        {
            var products = await productService.GetProducts();
            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateProduct(int id)
        {
            var product = await productService.GetProduct(id);
            var model = mapper.Map<ProductUpdateBinding>(product);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProduct(ProductUpdateBinding model)
        {
            var product = await productService.UpdateProduct(model);
            return RedirectToAction("ProductsList");
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
            return RedirectToAction("ProductsList");
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
            return RedirectToAction("ProductCategorysList");
        }

        [HttpGet]
        public async Task<IActionResult> ProductCategorysList()
        {
            var productCategorys = await productService.GetProductCategorys();
            return View(productCategorys);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateProductCategory(int id)
        {
            var productCategorys = await productService.GetProductCategory(id);
            var model = mapper.Map<ProductCategoryUpdateBinding>(productCategorys);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProductCategory(ProductCategoryUpdateBinding model)
        {
            var productCategorys = await productService.UpdateProductCategory(model);
            return RedirectToAction("ProductCategorysList");
        }
    }
}
