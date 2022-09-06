using Microsoft.AspNetCore.Identity;
using PavlicWebShop.Models;
using PavlicWebShop.Models.Dbo;
using PavlicWebShop.Services.Interface;

namespace PavlicWebShop.Services.Implementation
{
    public class IdentityService : IIdentityService
    {
        private RoleManager<IdentityRole> roleManager;
        private UserManager<ApplicationUser> userManager;

        public IdentityService(IServiceScopeFactory scopeFactory)
        {

            using (var scope = scopeFactory.CreateScope())
            {
                this.userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                this.roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();


                CreateRoleAsync(Roles.Admin).Wait();
                CreateRoleAsync(Roles.BasicUser).Wait();
                CreateRoleAsync(Roles.Employee).Wait();

                //CreateUser()
                CreateUserAsync(new ApplicationUser
                {
                    Firstname = "Ivan",
                    Lastname = "Pavlić",
                    Email = "ivan.pavlic@gmail.com",
                    UserName = "ivan.pavlic@gmail.com",
                    BirthDate = DateTime.Now.AddYears(-35),
                    PhoneNumber = "+3859132543678",
                    Adress = new List<Adress>
                    {
                        new Adress
                        {
                            City = "Pregrada",
                            Street = "Ljudevita Gaja 16",
                            Country= "Hrvatska"
                        }
                    }

                }, "Pa$$word123", Roles.Admin).Wait();

                CreateUserAsync(new ApplicationUser
                {
                    Firstname = "Kristijan",
                    Lastname = "Špiljak",
                    Email = "kristijan.spiljak@gmail.com",
                    UserName = "kristijan.spiljak@gmail.com",
                    BirthDate = DateTime.Now.AddYears(-35),
                    PhoneNumber = "+3859152845123",
                    Adress = new List<Adress>
                    {
                        new Adress
                        {
                            City = "Krapina",
                            Street = "Janka Leskovara 13",
                            Country= "Hrvatska"
                        }
                    }

                }, "Pa$$word456", Roles.Employee).Wait();

                CreateUserAsync(new ApplicationUser
                {
                    Firstname = "Igor",
                    Lastname = "Križanec",
                    Email = "igor.krizanec@gmail.hr",
                    UserName = "igor.krizanec@gmail.hr",
                    BirthDate = DateTime.Now.AddYears(-35),
                    PhoneNumber = "+385983472979",
                    Adress = new List<Adress>
                    {
                        new Adress
                        {
                            City = "Zagreb",
                            Street = "Ulica Nikole Tavelića 2",
                            Country= "Hrvatska"
                        }
                    }

                }, "Ig0r&123", Roles.BasicUser).Wait();
            }
        }


        public async Task CreateRoleAsync(string role)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole
                {
                    Name = role

                });
            }

        }


        public async Task CreateUserAsync(ApplicationUser user, string password, string role)
        {

            //Prvo provjeri ima li korisnika sa istim emailom u bazi
            var find = await userManager.FindByEmailAsync(user.Email);
            if (find != null)
            {
                return;
            }

            user.EmailConfirmed = true;
            //Izraditi novog korisnika
            var createdUser = await userManager.CreateAsync(user, password);
            //Provjeriti jeli korisnik uspješno dodan
            if (createdUser.Succeeded)
            {
                //Dodati korisnika u rolu
                var userAddedToRole = await userManager.AddToRoleAsync(user, role);
                if (!userAddedToRole.Succeeded)
                {
                    throw new Exception("Korisnik nije dodan u rolu!");
                }
            }
        }
    }
}

