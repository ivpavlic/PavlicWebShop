using PavlicWebShop.Models.Base;
using PavlicWebShop.Models.Dbo.Base;

namespace PavlicWebShop.Models.Dbo
{
    public class FileStorage : FileStorageBase, IEntityBase
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
    }
}
