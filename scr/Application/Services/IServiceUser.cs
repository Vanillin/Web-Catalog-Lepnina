using Application.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IServiceUser
    {
        Task<UserDto> ReadById(int id);
        Task<List<UserDto>> ReadAll();
        Task Create(UserDto element);
        Task<bool> Update(UserDto element);
        Task<bool> Delete(int id);
    }
}
