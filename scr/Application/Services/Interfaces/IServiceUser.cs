using Application.Dto;
using Application.Request;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IServiceUser
    {
        Task<UserDto?> ReadById(int id);
        Task<IEnumerable<UserDto>> ReadAll();
        Task<int?> Create(CreateUserRequest element);
        Task<bool> Update(UpdateUserRequest element);
        Task<bool> Delete(int id);
    }
}
