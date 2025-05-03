using Domain.Entities;

namespace Infrastructure.Repositories
{
    public interface IRepositUser
    {
        Task<User?> ReadById(int id);
        Task<IEnumerable<User>> ReadAll();
        Task<int?> Create(User element);
        Task<bool> Update(User element);
        Task<User?> ReadByEmail(string email);
        Task<bool> Delete(int id);
    }
}
