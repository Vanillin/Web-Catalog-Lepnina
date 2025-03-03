using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{

    public interface IRepositProduct
    {
        Task<Product> ReadById(int id);
        Task<List<Product>> ReadAll();
        Task Create(Product element);
        Task<bool> Update(Product element);
        Task<bool> Delete(int id);
    }
}
