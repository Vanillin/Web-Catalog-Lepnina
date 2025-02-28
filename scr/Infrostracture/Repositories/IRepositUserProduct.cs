using Domain.Entitys;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrostracture.Repositories
{
    public interface IRepositUserProduct
    {
        Task<UserProduct> ReadById(int id_user, int id_product);
        Task<List<UserProduct>> ReadAll();
        Task Create(UserProduct element);
        Task<bool> Delete(int id_user, int id_product);
    }
}
