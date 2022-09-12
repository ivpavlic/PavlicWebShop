﻿using Microsoft.AspNetCore.Identity;
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
        public HomeController(ILogger<HomeController> logger, IProductService productService, UserManager<ApplicationUser> userManager, 
            IUserService userService, SignInManager<ApplicationUser> signInManager)
        {
            this.productService = productService;
            this.userManager = userManager;
            this.userService = userService;
            this.signInManager = signInManager;
            _logger = logger;
        }


        public IActionResult Index()
        {
            return View(productService.GetProductsRandom().Result);
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