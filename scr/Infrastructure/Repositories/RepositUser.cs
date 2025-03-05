using Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class RepositUser : IRepositUser
    {
        private List<User> _users;
        public RepositUser()
        {
            _users = new List<User>();
        }

        private User Find(int id)
        {
            return _users.FirstOrDefault(v => v.Id == id);
        }
        public Task Create(User element)
        {
            _users.Add(element);
            return Task.CompletedTask;
        }
        public Task<bool> Delete(int id)
        {
            var find = Find(id);
            if (find != null)
            {
                _users.Remove(find);
                return Task.FromResult(true);
            }
            else
                return Task.FromResult(false);
        }
        public Task<List<User>> ReadAll()
        {
            return Task.FromResult(_users);
        }
        public Task<User> ReadById(int id)
        {
            var find = Find(id);
            return Task.FromResult(find);
        }
        public Task<bool> Update(User element)
        {
            var find = Find(element.Id);
            if (find != null)
            {
                find.Name = element.Name;
                find.PathIcon = element.PathIcon;
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }
    }
}
