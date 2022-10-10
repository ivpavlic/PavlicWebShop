using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PavlicWebShop.Models;
using PavlicWebShop.Models.Binding;
using PavlicWebShop.Models.Dbo;
using PavlicWebShop.Services.Interface;

namespace PavlicWebShop.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService userService;
        private readonly IProductService productService;
        private readonly SignInManager<ApplicationUser> signInManager;
        public UserController(IUserService userSevice, SignInManager<ApplicationUser> signInManager, IProductService productService)
        {
            this.userService = userSevice;
            this.signInManager = signInManager;
            this.productService = productService;   
        }

        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registration(UserBinding model)
        {
            var result = await userService.CreateUserAsync(model, Roles.BasicUser);
            if (result != null)
            {
                await signInManager.SignInAsync(result, true);
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public IActionResult Index()
        {
            return View(productService.GetProducts().Result);
        }

    }
}
