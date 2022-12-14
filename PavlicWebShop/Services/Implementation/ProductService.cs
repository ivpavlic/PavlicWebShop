using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PavlicWebShop.Data;
using PavlicWebShop.Models;
using PavlicWebShop.Models.Binding;
using PavlicWebShop.Models.Dbo;
using PavlicWebShop.Models.ViewModel;
using PavlicWebShop.Services.Interface;
using System.Globalization;

namespace PavlicWebShop.Services.Implementation
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext db;
        private readonly IMapper mapper;
        private readonly IFileStorageService fileStorageService;

        public ProductService(ApplicationDbContext db,
           IMapper mapper,IFileStorageService fileStorageService)
        {
            this.db = db;
            this.mapper = mapper;
            this.fileStorageService = fileStorageService;
        }

        
        /// <summary>
        /// Dodavanje proizvoda
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ProductViewModel> AddProduct(ProductBinding model)
        {
            var dbo = mapper.Map<Product>(model);

            if (model.ProductImg != null)
            {
                var fileResponse = await fileStorageService.AddFileToStorage(model.ProductImg);
                if (fileResponse != null)
                {
                    dbo.ProductImgUrl = fileResponse.DownloadUrl;
                }

            }


            var productCategory = await db.ProductCategory.FindAsync(model.ProductCategoryId);
            if (productCategory == null)
            {
                return null;
            }

            var price = decimal.Parse(model.Price.ToString(), CultureInfo.InvariantCulture);
            dbo.Price = price;
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

        public async Task<List<ProductViewModel>> GetProductByCategory(int productCategoryId)
        {
            var dbo = await db.Product.Where(x=>x.ProductCategory.Id == productCategoryId)
               .Include(x => x.ProductCategory)
               .ToListAsync();

            return dbo.Select(x => mapper.Map<ProductViewModel>(x)).ToList();

        }

        /// <summary>
        /// Dohvati sve proizvode
        /// </summary>
        /// <returns></returns>
        public async Task<List<ProductViewModel>> GetProducts()
        {
            var dbo = await db.Product
                .Include(x => x.ProductCategory)
                .ToListAsync();
            return dbo.Select(x => mapper.Map<ProductViewModel>(x)).ToList();

        }

        /// <summary>
        /// Brisanje proizvoda
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteProduct(int id)
        {
            var product = await db.Product
                //.Include(x => x.ProductCategory)
                .FirstOrDefaultAsync(x => x.Id == id);
            db.Product.Remove(product);
            await db.SaveChangesAsync();
            return;

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
            var dbo = await db.ProductCategory
               .FirstOrDefaultAsync(x => x.Id == id);
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
        /// Update product
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ProductViewModel> UpdateProduct(ProductUpdateBinding model)
        {
            var category = await db.ProductCategory.FirstOrDefaultAsync(x => x.Id == model.ProductCategoryId);
            var dbo = await db.Product.FindAsync(model.Id);
            if(model.ProductImgUrl==null)
            {
                model.ProductImgUrl = dbo.ProductImgUrl;
            }
            mapper.Map(model, dbo);
            dbo.ProductCategory = category;
            await db.SaveChangesAsync();
            return mapper.Map<ProductViewModel>(dbo);
        }

        /// <summary>
        /// Update product category
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ProductCategoryViewModel> UpdateProductCategory(ProductCategoryUpdateBinding model)
        {
            var dbo = await db.ProductCategory.FindAsync(model.Id);
            mapper.Map(model, dbo);
            await db.SaveChangesAsync();
            return mapper.Map<ProductCategoryViewModel>(dbo);
        }
    }
}
