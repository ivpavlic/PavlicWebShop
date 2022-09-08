using PavlicWebShop.Models.Binding;
using PavlicWebShop.Models.Dbo;
using PavlicWebShop.Models.ViewModel;
using System.Security.Claims;

namespace PavlicWebShop.Services.Interface
{
    public interface IUserService
    {
        Task<ApplicationUserViewModel> GetUser(ClaimsPrincipal user);
        Task<ApplicationUserViewModel> GetUser(string id);
        Task<string> GetUserRole(string id);
        Task<List<UserRolesViewModel>> GetUserRoles();
        Task<List<ApplicationUserViewModel>> GetUsers();
        Task<ApplicationUserViewModel> UpdateUser(UserAdminUpdateBinding model);
        Task DeleteUserAsync(string id);
        Task<ApplicationUserViewModel?> CreateUserAsync(UserAdminBinding model);
        Task<ApplicationUser?> CreateUserAsync(UserBinding model, string role);
    }
}