using Application.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IServiceFavorites
    {
        Task<FavoritesDto> ReadById(int idUser, int idProduct);
        Task<List<FavoritesDto>> ReadAll();
        Task Create(FavoritesDto element);
        Task<bool> Delete(int idUser, int idProduct);
    }
}
