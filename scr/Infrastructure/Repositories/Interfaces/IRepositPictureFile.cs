using Domain.Entities;

namespace Infrastructure.Repositories
{
    public interface IRepositPictureFile
    {
        Task<PictureFile?> Get(int id);
        Task<int?> Create(PictureFile file);
        Task<bool> Delete(int id);
    }
}
