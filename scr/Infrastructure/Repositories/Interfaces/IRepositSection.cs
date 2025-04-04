using Domain.Entities;

namespace Infrastructure.Repositories
{
    public interface IRepositSection
    {
        Task<Section?> ReadById(int id);
        Task<IEnumerable<Section>> ReadAll();
        Task<int?> Create(Section element);
        Task<bool> Update(Section element);
        Task<bool> Delete(int id);
    }
}
