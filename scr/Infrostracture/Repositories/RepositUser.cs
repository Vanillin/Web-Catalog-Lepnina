using Domain.Entitys;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrostracture.Repositories
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
            foreach (var v in _users)
                if (v.Id == id)
                    return v;
            return null;
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
                find.Path_icon = element.Path_icon;
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }
    }
}
