using System.ComponentModel.DataAnnotations;

namespace PavlicWebShop.Models.Base
{
    public class ApplicationUserBase
    {
        [Display(Name = "First name")]
        public string Firstname { get; set; }
        [Display(Name = "Last name")]
        public string Lastname { get; set; }
        [Display(Name = "Birth date")]
        public DateTime BirthDate { get; set; }
        //public string? ProductPhoto { get; set; }
        public string? UserImgUrl { get; set; }
    }
}
