using PavlicWebShop.Models.Binding;
using PavlicWebShop.Models.ViewModel;

namespace PavlicWebShop.Services.Interface
{
    public interface IProductService
    {
        Task<OrderViewModel> SuspendOrder(int id);
        Task<ProductViewModel> UpdateProduct(ProductUpdateBinding model);
        Task UpdateShoppinCartStatus();
        Task<OrderViewModel> GetOrder(int id);
        Task<List<OrderViewModel>> GetOrders();
        Task<OrderViewModel> AddOrder(OrderBinding model);
        Task<ShoppingCartViewModel> GetShoppingCart(string userId);
        Task<ProductViewModel> AddProduct(ProductBinding model);
        Task<ProductViewModel> GetProduct(int id);
        Task<List<ProductViewModel>> GetProducts();
        Task<List<ProductViewModel>> GetProductsRandom();
        Task DeleteProduct(int id);
        Task<ProductCategoryViewModel> UpdateProductCategory(ProductCategoryUpdateBinding model);
        Task<List<ProductCategoryViewModel>> GetProductCategorys();
        Task<ProductCategoryViewModel> GetProductCategory(int id);
        Task<ProductCategoryViewModel> AddProductCategory(ProductCategoryBinding model);
        Task<ShoppingCartViewModel> AddShoppingCart(ShoppingCartBinding model);
    }
}