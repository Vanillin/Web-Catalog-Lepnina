using Application.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IRepositUserProductDto
    {
        Task<UserProductDto> ReadById(int id_user, int id_product);
        Task<List<UserProductDto>> ReadAll();
        Task Create(UserProductDto element);
        Task<bool> Delete(int id_user, int id_product);
    }
}
