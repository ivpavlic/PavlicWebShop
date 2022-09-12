namespace PavlicWebShop.Services.Interface
{
    public interface IValidationService
    {
        Task<bool> EmailExists(string email);
    }
}