using PavlicWebShop.Models.Base;
using PavlicWebShop.Models.Dbo.Base;
using System.ComponentModel.DataAnnotations;

namespace PavlicWebShop.Models.Dbo
{
    public class Adress : AdressBase, IEntityBase
    {
        [Key]
        public int Id { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public DateTime Created { get; set; }
    }
}
