using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PavlicWebShop.Data;
using PavlicWebShop.Models.Binding;
using PavlicWebShop.Models.Dbo;
using PavlicWebShop.Models.Dto;
using PavlicWebShop.Models.ViewModel;
using PavlicWebShop.Services.Interface;
using System.Security.Claims;

namespace PavlicWebShop.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IMapper mapper;
        private SignInManager<ApplicationUser> signInManager;
        private readonly ApplicationDbContext db;
        private readonly AppConfig appSettings;

        public UserService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper,
            SignInManager<ApplicationUser> signInManager, ApplicationDbContext db, IOptions<AppConfig> appSettings)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.mapper = mapper;
            this.signInManager = signInManager;
            this.db = db;
            this.appSettings = appSettings.Value;
        }

        public async Task<List<UserRolesViewModel>> GetUserRoles()
        {

            var roles = await db.Roles.ToListAsync();
            if (!roles.Any())
            {
                return new List<UserRolesViewModel>();
            }

            return roles.Select(x => mapper.Map<UserRolesViewModel>(x)).ToList();
        }


        public async Task<List<ApplicationUserViewModel>> GetUsers()
        {
            var dboUsers = await db.Users
                .Include(x => x.Adress)
                .ToListAsync();
            var response = dboUsers.Select(x => mapper.Map<ApplicationUserViewModel>(x)).ToList();
            response.ForEach(x => x.Role = GetUserRole(x.Id).Result);
            return response;

        }


        public async Task<string> GetUserRole(string id)
        {
            var dboUser = await db.Users.FindAsync(id);
            if (dboUser == null)
            {
                return String.Empty;
            }
            var roles = await userManager.GetRolesAsync(dboUser);
            return roles.First();

        }


        public async Task<ApplicationUserViewModel> UpdateUser(UserAdminUpdateBinding model)
        {
            var dboUser = await db.Users
                .Include(x => x.Adress)
                .FirstOrDefaultAsync(x => x.Id == model.Id);
            var role = await db.Roles.FindAsync(model.RoleId);


            if (dboUser == null || role == null)
            {
                return null;
            }


            await DeleteAllUserRoles(dboUser);
            await userManager.AddToRoleAsync(dboUser, role.Name);

            dboUser.Firstname = model.Firstname;
            dboUser.Lastname = model.Lastname;
            dboUser.BirthDate = model.BirthDate;
            await db.SaveChangesAsync();


            var response = mapper.Map<ApplicationUserViewModel>(dboUser);
            return response;
        }


        private async Task DeleteAllUserRoles(ApplicationUser user)
        {
            var userRoles = await userManager.GetRolesAsync(user);
            foreach (var item in userRoles)
            {
                await userManager.RemoveFromRoleAsync(user, item);
            }
        }



        public async Task<ApplicationUserViewModel> GetUser(string id)
        {
            var dboUser = await db.Users
                .Include(x => x.Adress)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (dboUser == null)
            {
                return null;
            }

            var response = mapper.Map<ApplicationUserViewModel>(dboUser);
            response.Role = await GetUserRole(id);
            return response;
        }


        public async Task<ApplicationUserViewModel> GetUser(ClaimsPrincipal user)
        {
            var userId = userManager.GetUserId(user);
            return await GetUser(userId);
        }

        public async Task DeleteUserAsync(string id)
        {
            var user = await db.Users.FindAsync(id);
            await userManager.DeleteAsync(user);
            return;
        }


        public async Task<ApplicationUserViewModel?> CreateUserAsync(UserAdminBinding model)
        {
            var find = await userManager.FindByEmailAsync(model.Email);
            if (find != null)
            {
                return null;
            }

            var user = new ApplicationUser
            {
                Email = model.Email,
                UserName = model.Email,
                Firstname = model.Firstname,
                Lastname = model.Lastname,
                BirthDate = model.BirthDate

            };

            var roles = await GetUserRoles();
            var userRole = roles.FirstOrDefault(x => x.Id == model.RoleId);

            var createdUser = await userManager.CreateAsync(user, model.Password);
            if (createdUser.Succeeded)
            {
                var userAddedToRole = await userManager.AddToRoleAsync(user, userRole.Name);
                if (!userAddedToRole.Succeeded)
                {
                    throw new Exception("Korisnik nije dodan u rolu!");
                }
            }
            return mapper.Map<ApplicationUserViewModel>(user);
        }


        //public async Task<ApplicationUser?> CreateUserAsync(ApiBasicDataUser model, string role)
        //{
        //    var find = await userManager.FindByEmailAsync(model.Email);
        //    if (find != null)
        //    {
        //        return null;
        //    }

        //    var user = new ApplicationUser
        //    {
        //        Email = model.Email,
        //        UserName = model.Email,
        //    };


        //    var createdUser = await userManager.CreateAsync(user, model.Password);
        //    if (createdUser.Succeeded)
        //    {
        //        var userAddedToRole = await userManager.AddToRoleAsync(user, role);
        //        if (!userAddedToRole.Succeeded)
        //        {
        //            throw new Exception("Korisnik nije dodan u rolu!");
        //        }
        //    }
        //    return user;
        //}


        public async Task<ApplicationUser?> CreateUserAsync(UserBinding model, string role)
        {
            var find = await userManager.FindByEmailAsync(model.Email);
            if (find != null)
            {
                return null;
            }
            var user = mapper.Map<ApplicationUser>(model);
            var adress = mapper.Map<Adress>(model.UserAdress);
            user.Adress = new List<Adress>() { adress };
            var createdUser = await userManager.CreateAsync(user, model.Password);
            if (createdUser.Succeeded)
            {
                var userAddedToRole = await userManager.AddToRoleAsync(user, role);
                if (!userAddedToRole.Succeeded)
                {
                    throw new Exception("Korisnik nije dodan u rolu!");
                }
            }
            return user;
        }

    }
}
