using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PavlicWebShop.Data;
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

    }
}
