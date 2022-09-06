using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PavlicWebShop.Models.Base
{
    public class ShoppingChartItemBase
    {
        [Required]
        [Display(Name = "Količina")]
        [Column(TypeName = "decimal(9, 2)")]
        public decimal Quantity { get; set; }

        [Required]
        [Display(Name = "Cijena")]
        [Column(TypeName = "decimal(9, 2)")]
        public decimal Price { get; set; }
    }
}
