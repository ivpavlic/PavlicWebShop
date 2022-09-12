using Microsoft.EntityFrameworkCore;
using PavlicWebShop.Data;
using PavlicWebShop.Services.Interface;

namespace PavlicWebShop.Services.Implementation
{
    public class ValidationService : IValidationService
    {
        private readonly ApplicationDbContext db;

        public ValidationService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<bool> EmailExists(string email)
        {
            return await db.Users.FirstOrDefaultAsync(x => x.NormalizedEmail == email.ToUpper()) != null;
        }
    }
}
