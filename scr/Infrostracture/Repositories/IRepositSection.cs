using Domain.Entitys;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrostracture.Repositories
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
