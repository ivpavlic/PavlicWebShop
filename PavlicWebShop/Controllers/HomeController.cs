using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PavlicWebShop.Models;
using PavlicWebShop.Models.Binding;
using PavlicWebShop.Models.Dbo;
using PavlicWebShop.Services.Implementation;
using PavlicWebShop.Services.Interface;
using System.Diagnostics;

namespace PavlicWebShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService productService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUserService userService;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IMapper mapper;
        public HomeController(ILogger<HomeController> logger, IProductService productService, UserManager<ApplicationUser> userManager, 
            IUserService userService, SignInManager<ApplicationUser> signInManager, IMapper mapper)
        {
            this.productService = productService;
            this.userManager = userManager;
            this.userService = userService;
            this.signInManager = signInManager;
            _logger = logger;
            this.mapper = mapper;
        }


        public IActionResult Index()
        {
            return View(productService.GetProducts().Result);
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

        [HttpGet]
        public async Task<IActionResult> ProductsByCategory(int id)
        {
            var products = await productService.GetProductByCategory(id);
            return View(products);
        }

        public async Task<IActionResult> DetailsProduct(int id)
        {
            var product = await productService.GetProduct(id);
            var model = mapper.Map<ProductUpdateBinding>(product);
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



    }
}