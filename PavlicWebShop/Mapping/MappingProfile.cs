using AutoMapper;
using Microsoft.AspNetCore.Identity;
using PavlicWebShop.Models.Binding;
using PavlicWebShop.Models.Dbo;
using PavlicWebShop.Models.ViewModel;

namespace PavlicWebShop.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<IdentityRole, UserRolesViewModel>();

            CreateMap<ProductBinding, Product>();
            CreateMap<Product, ProductViewModel>();
            CreateMap<ProductViewModel, ProductUpdateBinding>();
            CreateMap<ProductUpdateBinding, Product>();

            CreateMap<ProductCategoryBinding, ProductCategory>();
            CreateMap<ProductCategory, ProductCategoryViewModel>();
            CreateMap<ProductCategoryViewModel, ProductCategoryUpdateBinding>();
            CreateMap<ProductCategoryUpdateBinding, ProductCategory>();

            CreateMap<ApplicationUser, ApplicationUserViewModel>();

          

            CreateMap<AdressBinding, Adress>();
            CreateMap<Adress, AdressViewModel>();
            CreateMap<UserBinding, ApplicationUser>()
                .ForMember(dst => dst.UserName, opts => opts.MapFrom(src => src.Email))
                .ForMember(dst => dst.EmailConfirmed, opts => opts.MapFrom(src => true));

            CreateMap<UserAdminBinding, ApplicationUser>()
                .ForMember(dst => dst.UserName, opts => opts.MapFrom(src => src.Email));


            CreateMap<FileStorage, FileStorageViewModel>();
            CreateMap<FileStorage, FileStorageExpendedViewModel>();


            CreateMap<FileStorageViewModel, FileStorage>().
                ForMember(dst => dst.Id, opts => opts.Ignore());
        }
    }
}
