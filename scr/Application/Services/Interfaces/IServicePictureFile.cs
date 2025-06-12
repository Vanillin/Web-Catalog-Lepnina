using Application.Response;
using Microsoft.AspNetCore.Http;

namespace Application.Services
{
    public interface IServicePictureFile
    {
        Task<PictureFileResponce> UploadAsync(IFormFile file, string category);
        Task<byte[]> GetFileContentAsync(int id);
        Task<bool> DeleteAsync(int id);
        Task<string> GetPublicLinkAsync(int id);
        Task<PictureFileResponce> GetMetadataAsync(int id);
    }
}
