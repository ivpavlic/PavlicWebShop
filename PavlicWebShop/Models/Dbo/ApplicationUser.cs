using Microsoft.AspNetCore.Identity;
using PavlicWebShop.Validation;

namespace PavlicWebShop.Models.Dbo
{
    public class ApplicationUser : IdentityUser
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }

        [UserValidation]
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public ICollection<Adress> Adress { get; set; }
        public string? UserImgUrl { get; set; }
    }
}
