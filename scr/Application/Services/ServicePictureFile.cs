using Application.Exception;
using Application.Response;
using Domain.Entities;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;

namespace Application.Services
{
    public class ServicePictureFile : IServicePictureFile
    {
        private IRepositPictureFile _repository;
        private IServiceFileStorage _fileStorage;
        private IHttpContextAccessor _httpContextAccessor;
        public ServicePictureFile(IRepositPictureFile repository, IServiceFileStorage fileStorage, IHttpContextAccessor httpContextAccessor)
        {
            _repository = repository;
            _fileStorage = fileStorage;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<PictureFileResponce> UploadAsync(IFormFile file, string category)
        {
            var extension = Path.GetExtension(file.FileName);
            var fileName = $"{Guid.NewGuid()}{extension}";
            var storedPath = await _fileStorage.SaveFile(file, category, fileName);

            var pictureFile = new PictureFile
            {
                FileName = file.FileName,
                StoredPath = storedPath,
                ContentType = file.ContentType,
                Size = file.Length
            };

            int? id = await _repository.Create(pictureFile);
            if (id == null)
                throw new EntityCreateException("PictureFile not create");

            return new PictureFileResponce
            {
                Id = (int)id,
                FileName = pictureFile.FileName,
                StoredPath = pictureFile.StoredPath,
                ContentType = pictureFile.ContentType,
                Size = pictureFile.Size
            };
        }

        public async Task<byte[]> GetFileContentAsync(int id)
        {
            var attachment = await _repository.Get(id);
            if (attachment == null || !_fileStorage.FileExists(attachment.StoredPath))
                throw new FileNotFoundException("PictureFile not found");

            return await _fileStorage.ReadFile(attachment.StoredPath);
        }

        public async Task DeleteAsync(int id)
        {
            var attachment = await _repository.Get(id);
            if (attachment == null)
                return;

            _fileStorage.DeleteFile(attachment.StoredPath);
            await _repository.Delete(id);
        }

        public async Task<string> GetPublicLinkAsync(int id)
        {
            var context = _httpContextAccessor.HttpContext;
            if (context == null)
                throw new SystemException("HttpContext is null");
            var request = context.Request;

            var attachment = await _repository.Get(id);
            if (attachment == null)
                throw new FileNotFoundException("PictureFile not found");

            var baseUrl = $"{request.Scheme}://{request.Host}";
            return $"{baseUrl}/api/attachments/{id}/download";
        }

        public async Task<PictureFileResponce?> GetMetadataAsync(int id)
        {
            PictureFile? pictureFile = await _repository.Get(id);
            if (pictureFile == null)
                return null;
            else
                return new PictureFileResponce
                {
                    Id = pictureFile.Id,
                    FileName = pictureFile.FileName,
                    StoredPath = pictureFile.StoredPath,
                    ContentType = pictureFile.ContentType,
                    Size = pictureFile.Size
                };
        }
    }
}