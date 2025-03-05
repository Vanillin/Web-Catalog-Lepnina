using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public interface IRepositUserProduct
    {
        Task<UserProduct> ReadById(int idUser, int idProduct);
        Task<List<UserProduct>> ReadAll();
        Task Create(UserProduct element);
        Task<bool> Delete(int idUser, int idProduct);
    }
}
