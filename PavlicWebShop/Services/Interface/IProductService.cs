using PavlicWebShop.Models.Binding;
using PavlicWebShop.Models.ViewModel;

namespace PavlicWebShop.Services.Interface
{
    public interface IProductService
    {
        Task<ProductViewModel> AddProduct(ProductBinding model);
        Task<ProductCategoryViewModel> AddProductCategory(ProductCategoryBinding model);
        Task<ProductViewModel> GetProduct(int id);
        Task<ProductCategoryViewModel> GetProductCategory(int id);
        Task<List<ProductCategoryViewModel>> GetProductCategorys();
        Task<List<ProductViewModel>> GetProducts();
    }
}