using Application.Dto;
using Application.Response;

namespace Application.Services
{
    public interface IServiceFavorites
    {
        Task<FavoritesDto?> ReadById(int idUser, int idProduct);
        Task<IEnumerable<FavoritesDto>> ReadAll();
        Task<CreateFavoriteResponse?> Create(FavoritesDto element);
        Task<bool> Delete(int idUser, int idProduct);
    }
}
