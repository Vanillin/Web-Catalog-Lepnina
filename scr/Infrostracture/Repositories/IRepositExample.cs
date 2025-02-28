using Domain.Entitys;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrostracture.Repositories
{
    public interface IRepositExample
    {
        Task<Example> ReadById(int id);
        Task<List<Example>> ReadAll();
        Task Create(Example element);
        Task<bool> Update(Example element);
        Task<bool> Delete(int id);
    }
}
