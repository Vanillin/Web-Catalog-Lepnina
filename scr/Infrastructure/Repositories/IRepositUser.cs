using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public interface IRepositUser
    {
        Task<User> ReadById(int id);
        Task<List<User>> ReadAll();
        Task Create(User element);
        Task<bool> Update(User element);
        Task<bool> Delete(int id);
    }
}
