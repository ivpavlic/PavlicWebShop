using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PavlicWebShop.Models;
using PavlicWebShop.Models.Binding;
using PavlicWebShop.Services.Implementation;
using PavlicWebShop.Services.Interface;
using Microsoft.AspNetCore.Identity;
using PavlicWebShop.Models.Dbo;

namespace PavlicWebShop.Controllers
{
    [Authorize(Roles = Roles.Admin)]
    public class AdminController : Controller
    {
        private readonly IProductService productService;
        private readonly IMapper mapper;
        private readonly IUserService userSevice;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AdminController(IProductService productService, IMapper mapper, IUserService userSevice, SignInManager<ApplicationUser> signInManager)
        {
            this.mapper = mapper;
            this.productService = productService;
            this.userSevice = userSevice;
            this.signInManager = signInManager;
        }

        [HttpGet]
        public async Task<IActionResult> Users()
        {
            var users = await userSevice.GetUsers();
            return View(users);
        }


        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await userSevice.GetUser(id);
            return View(user);
        }


        [HttpPost]
        public async Task<IActionResult> EditUser(UserAdminUpdateBinding model)
        {
            await userSevice.UpdateUser(model);
            return RedirectToAction("Users");
        }

        [HttpGet]
        public async Task<IActionResult> UserDetails(string id)
        {
            var user = await userSevice.GetUser(id);
            return View(user);
        }


        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id)
        {
            await userSevice.DeleteUserAsync(id);
            return RedirectToAction("Users");
        }

        //[HttpPost]
        //public async Task<IActionResult> CreateNewUser(UserAdminBinding model)
        //{
        //    await userSevice.CreateUserAsync(model);
        //    return RedirectToAction("Users");
        //}

        [HttpGet]
        public async Task<IActionResult> CreateNewUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewUser(UserAdminBinding model)
        {
            await userSevice.CreateUserAsync(model);
            return RedirectToAction("Users");
        }





        [HttpGet]
        public async Task<IActionResult> ProductAdministration()
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
            return RedirectToAction("ProductAdministration");
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
            return RedirectToAction("ProductAdministration");
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
            return RedirectToAction("ProductAdministration");
        }
    }
}
