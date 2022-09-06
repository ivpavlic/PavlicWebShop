using AutoMapper;
using PavlicWebShop.Models.Binding;
using PavlicWebShop.Models.Dbo;
using PavlicWebShop.Models.ViewModel;

namespace PavlicWebShop.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductBinding, Product>();
            CreateMap<Product, ProductViewModel>();
            CreateMap<ProductCategoryBinding, ProductCategory>();
            CreateMap<ProductCategory, ProductCategoryViewModel>();
            //CreateMap<ProductCategoryUpdateBinding, ProductCategory>();
            //CreateMap<ShoppingCartItemBinding, ShoppingCartItem>();
            CreateMap<ShoppingCartItem, ShoppingCartItemViewModel>();
            CreateMap<ApplicationUser, ApplicationUserViewModel>();
            CreateMap<ShoppingCart, ShoppingCartViewModel>();
            CreateMap<Order, OrderViewModel>();

            CreateMap<ProductViewModel, ProductUpdateBinding>();
            CreateMap<ProductUpdateBinding, Product>();

            CreateMap<AdressBinding, Adress>();
            CreateMap<Adress, AdressViewModel>();
        }
    }
}
