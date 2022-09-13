using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PavlicWebShop.Data;
using PavlicWebShop.Models;
using PavlicWebShop.Models.Binding;
using PavlicWebShop.Models.Dbo;
using PavlicWebShop.Models.Dto;
using PavlicWebShop.Models.ViewModel;
using PavlicWebShop.Services.Interface;
using System.Globalization;

namespace PavlicWebShop.Services.Implementation
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext db;
        private readonly IMapper mapper;
        private readonly AppConfig appConfig;
        private readonly IFileStorageService fileStorageService;

        public ProductService(ApplicationDbContext db,
           IMapper mapper, IOptions<AppConfig> appConfig,IFileStorageService fileStorageService)
        {
            this.appConfig = appConfig.Value;
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
            var dbo = await db.Product.ToListAsync();
            return dbo.Select(x => mapper.Map<ProductViewModel>(x)).ToList();

        }

        /// <summary>
        /// Dohvati sve proizvode
        /// </summary>
        /// <returns></returns>
        public async Task<List<ProductViewModel>> GetProductsRandom()
        {
            var dbo = await db.Product
               .Include(x => x.ProductCategory)
               .ToListAsync();

            //var model = dbo.Select(x => mapper.Map<ProductViewModel>(x)).ToList();

            //var randomByQuery = model.OrderBy(x => Guid.NewGuid()).ToList();

            // var response = new List<ProductViewModel>();
            //var randomList = webShopCommonSharedService.GetRandomNumberList(model.First().Id, model.Last().Id);

            // foreach (var item in randomList)
            // {
            //     var find = model.FirstOrDefault(x => x.Id == item);
            //     if(find != null)
            //     {
            //         response.Add(find);
            //     }
            // }


            //return response;

            //return dbo.Select(x => mapper.Map<ProductViewModel>(x)).OrderBy(x => Guid.NewGuid()).ToList();

            return dbo.Select(x => mapper.Map<ProductViewModel>(x)).ToList();

            //return dbo.Select(x => mapper.Map<ProductViewModel>(x)).OrderBy(x => Guid.NewGuid()).ToList();
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

        public List<int>? GetRandomNumberList(int start, int end)
        {
            if (start > end)
            {
                return null;
            }

            if (end == 0)
            {
                return null;
            }
            Random random = new Random();
            List<int> list = new List<int>();
            while (list.Count() < 6)
            {
                var randomNumber = random.Next(start, end);
                var find = list.FirstOrDefault(x => x == randomNumber);
                if (find != null)
                {
                    list.Add(randomNumber);
                }
            }
            return list;
        }
    }
}
