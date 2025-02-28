using Application.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public class RepositUserDto : IRepositUserDto
    {
        private List<UserDto> _users;
        public RepositUserDto()
        {
            _users = new List<UserDto>();
        }

        private UserDto Find(int id)
        {
            foreach (var v in _users)
                if (v.Id == id)
                    return v;
            return null;
        }
        public Task Create(UserDto element)
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
        public Task<List<UserDto>> ReadAll()
        {
            return Task.FromResult(_users);
        }
        public Task<UserDto> ReadById(int id)
        {
            var find = Find(id);
            return Task.FromResult(find);
        }
        public Task<bool> Update(UserDto element)
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
