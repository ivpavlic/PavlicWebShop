using PavlicWebShop.Models.Base;

namespace PavlicWebShop.Models.ViewModel
{
    public class ApplicationUserViewModel : ApplicationUserBase
    {
        public string Id { get; set; }
        public string Role { get; set; }
        public List<AdressViewModel> Adress { get; set; }
    }
}
