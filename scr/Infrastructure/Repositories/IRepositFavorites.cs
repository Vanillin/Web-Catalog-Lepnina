using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public interface IRepositFavorites
    {
        Task<Favorites> ReadById(int idUser, int idProduct);
        Task<List<Favorites>> ReadAll();
        Task Create(Favorites element);
        Task<bool> Delete(int idUser, int idProduct);
    }
}
