using Application.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IServiceUserProduct
    {
        Task<UserProductDto> ReadById(int idUser, int idProduct);
        Task<List<UserProductDto>> ReadAll();
        Task Create(UserProductDto element);
        Task<bool> Delete(int idUser, int idProduct);
    }
}
