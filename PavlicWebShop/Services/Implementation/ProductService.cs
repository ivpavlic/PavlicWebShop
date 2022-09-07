﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PavlicWebShop.Data;
using PavlicWebShop.Models;
using PavlicWebShop.Models.Binding;
using PavlicWebShop.Models.Dbo;
using PavlicWebShop.Models.Dto;
using PavlicWebShop.Models.ViewModel;
using PavlicWebShop.Services.Interface;

namespace PavlicWebShop.Services.Implementation
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext db;
        private readonly IMapper mapper;
        private readonly AppConfig appConfig;

        public ProductService(ApplicationDbContext db, IMapper mapper, IOptions<AppConfig> appConfig)
        {
            this.appConfig = appConfig.Value;
            this.db = db;
            this.mapper = mapper;
        }

        /// <summary>
        /// Storniraj po id-u narudzbu
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<OrderViewModel> SuspendOrder(int id)
        {
            var order = await db.Order
                .Include(x => x.ShoppingCart)
                  .ThenInclude(x => x.ShoppingCartItems)
                   .ThenInclude(x => x.Product)
                .FirstOrDefaultAsync(x => x.Id == id);
            SuspendShoppingCart(order.ShoppingCart);
            await db.SaveChangesAsync();
            return mapper.Map<OrderViewModel>(order);
        }

        /// <summary>
        /// Dodavanje narudzbe
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<OrderViewModel> AddOrder(OrderBinding model)
        {
            var sc = await db.ShoppingCart.FirstOrDefaultAsync(x => x.Id == model.ShoppingCartId);
            sc.ShoppingCartStatus = Models.ShoppingCartStatus.Succeeded;
            if (sc == null)
            {
                return null;
            }

            var dbo = new Order
            {
                Paid = true,
                ShoppingCart = sc

            };
            db.Order.Add(dbo);
            await db.SaveChangesAsync();
            return mapper.Map<OrderViewModel>(dbo);
        }
        /// <summary>
        /// Dohvati sve narudzbe
        /// </summary>
        /// <returns></returns>
        public async Task<List<OrderViewModel>> GetOrders()
        {
            var orders = await db.Order
                .Include(x => x.ShoppingCart)
                .ThenInclude(x => x.ApplicationUser)
                .Include(x => x.ShoppingCart)
                .ThenInclude(x => x.ShoppingCartItems)
                .ThenInclude(x => x.Product)
                .ToListAsync();

            return orders.Select(x => mapper.Map<OrderViewModel>(x)).ToList();

        }
        /// <summary>
        /// Dohvati narudzbu
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<OrderViewModel> GetOrder(int id)
        {
            var order = await db.Order
                .Include(x => x.ShoppingCart)
                .ThenInclude(x => x.ApplicationUser)
                .Include(x => x.ShoppingCart)
                .ThenInclude(x => x.ShoppingCartItems)
                .ThenInclude(x => x.Product)
                .FirstOrDefaultAsync(x => x.Id == id);

            return mapper.Map<OrderViewModel>(order);

        }
        /// <summary>
        /// Dohvati košarice prema statusu
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public async Task<List<ShoppingCartViewModel>> GetShoppingCarts(ShoppingCartStatus status)
        {
            var shoppingCarts = await db.ShoppingCart
                .Include(x => x.ShoppingCartItems)
                .ThenInclude(x => x.Product)
                .ThenInclude(x => x.ProductCategory)

                .Where(x => x.ShoppingCartStatus == status).ToListAsync();

            if (!shoppingCarts.Any())
            {
                return new List<ShoppingCartViewModel>();
            }

            return shoppingCarts.Select(x => mapper.Map<ShoppingCartViewModel>(x)).ToList();

        }
        /// <summary>
        /// Dohvati košaricu na temelju id korisnika
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<ShoppingCartViewModel> GetShoppingCart(string userId)
        {
            var shoppingCart = await db.ShoppingCart
                .Include(x => x.ShoppingCartItems)
                .ThenInclude(x => x.Product)
                .ThenInclude(x => x.ProductCategory)

                .FirstOrDefaultAsync(x => x.ApplicationUser.Id == userId && x.ShoppingCartStatus == Models.ShoppingCartStatus.Pending);

            if (shoppingCart == null)
            {
                return null;
            }

            return mapper.Map<ShoppingCartViewModel>(shoppingCart);

        }
        /// <summary>
        /// Dodavanje nove košarice
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ShoppingCartViewModel> AddShoppingCart(ShoppingCartBinding model)
        {
            if (model.ShoppingCartId.HasValue)
            {
                return await AddItemToShoppingCart(model);
            }

            var product = await db.Product.FindAsync(model.ProductId);
            product.Quantity -= model.Quantity;

            var user = await db.Users.FirstOrDefaultAsync(x => x.Id == model.UserId);
            if (product == null || user == null)
            {
                return null;
            }

            var shoppingCartItem = new ShoppingCartItem
            {
                Price = model.Price,
                Product = product,
                Quantity = model.Quantity
            };


            var dbo = new ShoppingCart
            {
                ShoppingCartItems = new List<ShoppingCartItem> { shoppingCartItem },
                ApplicationUser = user,
                ShoppingCartStatus = Models.ShoppingCartStatus.Pending

            };
            db.ShoppingCart.Add(dbo);
            await db.SaveChangesAsync();
            return mapper.Map<ShoppingCartViewModel>(dbo);

        }
        private async Task<ShoppingCartViewModel> AddItemToShoppingCart(ShoppingCartBinding model)
        {
            var product = await db.Product.FindAsync(model.ProductId);
            product.Quantity -= model.Quantity;

            var user = await db.Users.FirstOrDefaultAsync(x => x.Id == model.UserId);
            if (product == null || user == null)
            {
                return null;
            }



            var shoppingCartItem = new ShoppingCartItem
            {
                Price = model.Price,
                Product = product,
                Quantity = model.Quantity
            };


            var dbo = await db.ShoppingCart
                .Include(x => x.ShoppingCartItems)
                .FirstOrDefaultAsync(x => x.Id == model.ShoppingCartId.GetValueOrDefault());
            if (dbo == null)
            {
                return null;
            }

            dbo.ShoppingCartItems.Add(shoppingCartItem);

            await db.SaveChangesAsync();
            return mapper.Map<ShoppingCartViewModel>(dbo);

        }
        /// <summary>
        /// Dodavanje proizvoda
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ProductViewModel> AddProduct(ProductBinding model)
        {
            var dbo = mapper.Map<Product>(model);
            var productCategory = await db.ProductCategory.FindAsync(model.ProductCategoryId);
            if (productCategory == null)
            {
                return null;
            }
            dbo.ProductCategory = productCategory;
            db.Product.Add(dbo);
            await db.SaveChangesAsync();
            return mapper.Map<ProductViewModel>(dbo);
        }
        /// <summary>
        /// Dohvati proizvod putem id-1
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ProductViewModel> GetProduct(int id)
        {
            var dbo = await db.Product
                .Include(x => x.ProductCategory)
                .FirstOrDefaultAsync(x => x.Id == id);
            return mapper.Map<ProductViewModel>(dbo);

        }
        /// <summary>
        /// Dohvati sve proizvode
        /// </summary>
        /// <returns></returns>
        public async Task<List<ProductViewModel>> GetProducts()
        {
            var dbo = await db.Product.ToListAsync();
            return dbo.Select(x => mapper.Map<ProductViewModel>(x)).ToList();

        }
        /// <summary>
        /// Dodaj kategoriju proizvoda
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ProductCategoryViewModel> AddProductCategory(ProductCategoryBinding model)
        {
            var dbo = mapper.Map<ProductCategory>(model);
            db.ProductCategory.Add(dbo);
            await db.SaveChangesAsync();
            return mapper.Map<ProductCategoryViewModel>(dbo);
        }
        /// <summary>
        /// Dohvati kategoriju proivzvoda
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ProductCategoryViewModel> GetProductCategory(int id)
        {
            var dbo = await db.ProductCategory.FindAsync(id);
            return mapper.Map<ProductCategoryViewModel>(dbo);

        }
        /// <summary>
        /// Dohvati sve kategorije proizvoda
        /// </summary>
        /// <returns></returns>
        public async Task<List<ProductCategoryViewModel>> GetProductCategorys()
        {
            var dbo = await db.ProductCategory.ToListAsync();
            return dbo.Select(x => mapper.Map<ProductCategoryViewModel>(x)).ToList();

        }
        /// <summary>
        /// Ako je shoppingcart u statusu active npr 2h.
        /// Prebaciti status u suspended
        /// Napraviti povrat robe na odg kolicinu
        /// </summary>
        /// <returns></returns>
        public async Task UpdateShoppinCartStatus()
        {
            var shoppingCarts = await db.ShoppingCart
                .Include(x => x.ShoppingCartItems)
                .ThenInclude(x => x.Product)
                .Where(x => x.ShoppingCartStatus == ShoppingCartStatus.Pending && x.Created < DateTime.Now.AddHours(appConfig.ShoppingCartOffset))
                .ToListAsync();

            if (!shoppingCarts.Any())
            {
                return;
            }

            SuspendShoppingCarts(shoppingCarts);

            await db.SaveChangesAsync();

        }

        private List<ShoppingCart> SuspendShoppingCarts(List<ShoppingCart> shoppingCarts)
        {
            foreach (var shoppingCart in shoppingCarts)
            {
                SuspendShoppingCart(shoppingCart);
            }

            return shoppingCarts;
        }

        private static ShoppingCart SuspendShoppingCart(ShoppingCart shoppingCart)
        {
            shoppingCart.ShoppingCartStatus = ShoppingCartStatus.Suspended;
            foreach (var cartItems in shoppingCart.ShoppingCartItems)
            {
                cartItems.Product.Quantity += cartItems.Quantity;
            }

            return shoppingCart;
        }

        /// <summary>
        /// Update product
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ProductViewModel> UpdateProduct(ProductUpdateBinding model)
        {
            var category = await db.ProductCategory.FirstOrDefaultAsync(x => x.Id == model.ProductCategoryId);
            var dbo = await db.Product.FindAsync(model.Id);
            mapper.Map(model, dbo);
            dbo.ProductCategory = category;
            await db.SaveChangesAsync();
            return mapper.Map<ProductViewModel>(dbo);
        }
        public async Task<ProductCategoryViewModel> UpdateProductCategory(ProductCategoryUpdateBinding model)
        {
            var dbo = await db.ProductCategory.FindAsync(model.Id);
            mapper.Map(model, dbo);
            await db.SaveChangesAsync();
            return mapper.Map<ProductCategoryViewModel>(dbo);
        }
        /// <summary>
        /// Dodaj predmet u košaricu
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ShoppingCartItemViewModel> AddShoppingCartItem(ShoppingCartItemBinding model)
        {
            var dbo = mapper.Map<ShoppingCartItem>(model);
            db.ShoppingCartItem.Add(dbo);
            await db.SaveChangesAsync();
            return mapper.Map<ShoppingCartItemViewModel>(dbo);
        }
        /// <summary>
        /// dohvati proizvod iz košarice
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ShoppingCartItemViewModel> GetShoppingCartItem(int id)
        {
            var dbo = await db.ShoppingCartItem.FindAsync(id);
            return mapper.Map<ShoppingCartItemViewModel>(dbo);

        }
        /// <summary>
        /// dohvati sve proizvode iz košarice
        /// </summary>
        /// <returns></returns>
        public async Task<List<ShoppingCartItemViewModel>> GetShoppingCartItems()
        {
            var dbo = await db.ShoppingCartItem.ToListAsync();
            return dbo.Select(x => mapper.Map<ShoppingCartItemViewModel>(x)).ToList();

        }
    }
}
