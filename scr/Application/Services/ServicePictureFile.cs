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
            if (file.Length == 0)
                throw new ArgumentIsNullException("File is required");

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
            var picture = await _repository.Get(id);
            if (picture == null || !_fileStorage.FileExists(picture.StoredPath))
                throw new EntityNotFoundException("PictureFile not found");

            return await _fileStorage.ReadFile(picture.StoredPath);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var picture = await _repository.Get(id);
            if (picture == null)
                return false;

            _fileStorage.DeleteFile(picture.StoredPath);
            return await _repository.Delete(id);
        }

        public async Task<string> GetPublicLinkAsync(int id)
        {
            var context = _httpContextAccessor.HttpContext;
            if (context == null)
                throw new ArgumentIsNullException("HttpContext is null");
            var request = context.Request;

            var picture = await _repository.Get(id);
            if (picture == null)
                throw new EntityNotFoundException("PictureFile not found");

            var baseUrl = $"{request.Scheme}://{request.Host}";
            return $"{baseUrl}/api/attachments/{id}/download";
        }

        public async Task<PictureFileResponce> GetMetadataAsync(int id)
        {
            PictureFile? pictureFile = await _repository.Get(id);
            if (pictureFile == null)
                throw new EntityNotFoundException("PictureFile not found");

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