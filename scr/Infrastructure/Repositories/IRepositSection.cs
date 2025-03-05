using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public interface IRepositSection
    {
        Task<Section> ReadById(int id);
        Task<List<Section>> ReadAll();
        Task Create(Section element);
        Task<bool> Update(Section element);
        Task<bool> Delete(int id);
    }
}
