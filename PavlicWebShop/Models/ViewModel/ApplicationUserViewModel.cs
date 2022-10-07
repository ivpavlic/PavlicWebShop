using PavlicWebShop.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace PavlicWebShop.Models.ViewModel
{
    public class ApplicationUserViewModel : ApplicationUserBase
    {
        public string Id { get; set; }
        public string Role { get; set; }
        public AdressViewModel UserAdress { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Country { get; set; }
        public string RoleId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        [Display(Name = "Active")]
        public bool EmailConfirmed { get; set; }

    }
}
