using Application.Dto;
using Application.Request;

namespace Application.Services
{
    public interface IServiceUser
    {
        Task<UserDto?> ReadById(int id);
        Task<IEnumerable<UserDto>> ReadAll();
        Task<bool> Update(UpdateUserRequest element);
        Task<bool> Delete(int id);
    }
}
