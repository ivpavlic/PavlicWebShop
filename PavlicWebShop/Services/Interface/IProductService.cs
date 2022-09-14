using PavlicWebShop.Models.Binding;
using PavlicWebShop.Models.ViewModel;

namespace PavlicWebShop.Services.Interface
{
    public interface IProductService
    {
        Task<ProductViewModel> UpdateProduct(ProductUpdateBinding model);
        Task<ProductViewModel> AddProduct(ProductBinding model);
        Task<ProductViewModel> GetProduct(int id);
        Task<List<ProductViewModel>> GetProducts();
        Task<List<ProductViewModel>> GetProductByCategory(int productCategoryId);
        Task DeleteProduct(int id);
        Task<ProductCategoryViewModel> UpdateProductCategory(ProductCategoryUpdateBinding model);
        Task<List<ProductCategoryViewModel>> GetProductCategorys();
        Task<ProductCategoryViewModel> GetProductCategory(int id);
        Task<ProductCategoryViewModel> AddProductCategory(ProductCategoryBinding model);
    }
}