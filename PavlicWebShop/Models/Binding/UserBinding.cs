using PavlicWebShop.Models.ViewModel;
using PavlicWebShop.Validation;
using System.ComponentModel.DataAnnotations;

namespace PavlicWebShop.Models.Binding
{
    public class UserBinding : UserBaseBinding
    {

    }

    public class UserBaseBinding
    {
        [Display(Name = "Role")]
        public string RoleId { get; set; }
        //Validirati empty
        [Display(Name = "First name")]
        public string Firstname { get; set; }

        [Display(Name = "Last name")]
        public string Lastname { get; set; }

        [Display(Name = "Birth date")]
        public DateTime BirthDate { get; set; }
        public AdressBinding UserAdress { get; set; }
        [UserValidation]
        public string Email { get; set; }
        [StringGreaterThanThanSeven]
        public string Password { get; set; }
        public IFormFile UserImg { get; set; }

    }

    public class UserAdminBinding : UserBaseBinding
    {
        public bool EmailConfirmed { get; set; }
        public AdressViewModel UserAdress { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Country { get; set; }
        public string RoleId { get; set; }

    }

    public class UserAdminUpdateBinding : UserAdminBinding
    {
        public string Id { get; set; }
        

    }

}
