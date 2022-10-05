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

        public async Task<IActionResult> DetailsProduct(int id)
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

        [HttpPost]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await productService.DeleteProduct(id);
            return RedirectToAction("ProductsList");
        }

        [HttpPost]
        public async Task<IActionResult> AddProductCategory()
        {
            return View();
        }

        //[HttpGet]
        //public async Task<IActionResult> ProductsByCategory(int productCategoryId)
        //{
        //    var products = await productService.GetProductByCategory(productCategoryId);
        //    return View(products);
        //}

        [HttpGet]
        public async Task<IActionResult> ProductsByCategory(int id)
        {
            var products = await productService.GetProductByCategory(id);
            return View(products);
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
