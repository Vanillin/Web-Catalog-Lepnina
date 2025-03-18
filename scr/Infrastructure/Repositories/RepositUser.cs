using Domain.Entities;

namespace Infrastructure.Repositories
{
    public class RepositUser : IRepositUser
    {
        private List<User> _users;
        public RepositUser()
        {
            _users = new List<User>();

            _users.Add(new User()
            {
                Id = 1,
                Name = "firstuser",
                PathIcon = "firstusericon"
            });
            _users.Add(new User()
            {
                Id = 2,
                Name = "seconduser",
                PathIcon = "secondusericon"
            });
            _users.Add(new User()
            {
                Id = 3,
                Name = "thirduser",
                PathIcon = "thirdusericon"
            });
        }

        private User? Find(int id)
        {
            return _users.FirstOrDefault(v => v.Id == id);
        }
        public Task<int> Create(User element)
        {
            _users.Add(element);
            return Task.FromResult(element.Id);
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
        public Task<IEnumerable<User>> ReadAll()
        {
            IEnumerable<User> users = _users;
            return Task.FromResult(users);
        }
        public Task<User?> ReadById(int id)
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
