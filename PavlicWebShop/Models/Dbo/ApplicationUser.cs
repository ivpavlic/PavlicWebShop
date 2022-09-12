using Microsoft.AspNetCore.Identity;

namespace PavlicWebShop.Models.Dbo
{
    public class ApplicationUser : IdentityUser
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime BirthDate { get; set; }
        public ICollection<Adress> Adress { get; set; }
        public string? UserImgUrl { get; set; }
    }
}
