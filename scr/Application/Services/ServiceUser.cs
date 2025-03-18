using Application.Dto;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Repositories;

namespace Application.Services
{
    public class ServiceUser : IServiceUser
    {
        private IRepositUser _repositUser;
        private IMapper _mapper;
        public ServiceUser(IRepositUser repositUser, IMapper mapper)
        {
            _repositUser = repositUser;
            _mapper = mapper;
        }

        public async Task<int?> Create(UserDto element)
        {
            var mapElem = _mapper.Map<User>(element);
            if (mapElem == null) return null;
            if (ReadById(element.Id) == null) return await _repositUser.Create(mapElem);
            else return null;
        }
        public async Task<bool> Delete(int id)
        {
            return await _repositUser.Delete(id);
        }
        public async Task<IEnumerable<UserDto>> ReadAll()
        {
            var allElem = await _repositUser.ReadAll();
            var mapAllElem = allElem.Select(q => _mapper.Map<UserDto>(q)).ToList();
            return mapAllElem;
        }
        public async Task<UserDto?> ReadById(int id)
        {
            var element = await _repositUser.ReadById(id);
            if (element == null) return null;

            var mapElem = _mapper.Map<UserDto>(element);
            return mapElem;
        }
        public async Task<bool> Update(UserDto element)
        {
            var mapElem = _mapper.Map<User>(element);
            return await _repositUser.Update(mapElem);
        }
    }
}
