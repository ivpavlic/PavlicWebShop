using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PavlicWebShop.Models;
using PavlicWebShop.Models.Dbo;
using PavlicWebShop.Services.Interface;
using System.Diagnostics;

namespace PavlicWebShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService productService;
        private readonly UserManager<ApplicationUser> userManager;
        public HomeController(ILogger<HomeController> logger, IProductService productService, UserManager<ApplicationUser> userManager)
        {
            this.productService = productService;
            this.userManager = userManager;
            _logger = logger;
        }


        public IActionResult Index()
        {
            return View(productService.GetProductsRandom().Result);
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