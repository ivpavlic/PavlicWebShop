using PavlicWebShop.Models.ViewModel;

namespace PavlicWebShop.Services.Interface
{
    public interface IFileStorageService
    {
        Task<bool> DeleteFile(int id);
        Task<FileStorageViewModel> AddFileToStorage(IFormFile file);
        Task<FileStorageExpendedViewModel> GetFile(long id);
    }
}