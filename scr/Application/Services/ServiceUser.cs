using Application.Dto;
using Infrastructure.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ServiceUser : IServiceUser
    {
        private IRepositUser _repositUser;
        public ServiceUser(IRepositUser repositUser)
        {
            _repositUser = repositUser;
        }

        public Task Create(UserDto element)
        {
            throw new System.NotImplementedException();
        }
        public Task<bool> Delete(int id)
        {
            throw new System.NotImplementedException();
        }
        public Task<List<UserDto>> ReadAll()
        {
            throw new System.NotImplementedException();
        }
        public Task<UserDto> ReadById(int id)
        {
            throw new System.NotImplementedException();
        }
        public Task<bool> Update(UserDto element)
        {
            throw new System.NotImplementedException();
        }
    }
}
