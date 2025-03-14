using Application.Dto;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Repositories;

namespace Application.Services
{
    public class ServiceFavorites : IServiceFavorites
    {
        private IRepositFavorites _repositFavorites;
        private IMapper _mapper;
        public ServiceFavorites(IRepositFavorites repositUserProduct, IMapper mapper)
        {
            _repositFavorites = repositUserProduct;
            _mapper = mapper;
        }

        public async Task Create(FavoritesDto element)
        {
            var mapElem = _mapper.Map<Favorites>(element);
            if (mapElem != null) await _repositFavorites.Create(mapElem);
        }
        public async Task<bool> Delete(int idUser, int idProduct)
        {
            return await _repositFavorites.Delete(idUser, idProduct);
        }
        public async Task<List<FavoritesDto>> ReadAll()
        {
            var allElem = await _repositFavorites.ReadAll();
            var mapAllElem = allElem.Select(q => _mapper.Map<FavoritesDto>(q)).ToList();
            return mapAllElem;
        }
        public async Task<FavoritesDto> ReadById(int idUser, int idProduct)
        {
            var element = await _repositFavorites.ReadById(idUser, idProduct);
            var mapElem = _mapper.Map<FavoritesDto>(element);
            return mapElem;
        }
    }
}
