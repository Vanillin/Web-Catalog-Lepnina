using Application.Dto;
using Application.Exception;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Repositories;

namespace Application.Services
{
    public class ServiceFavorites : IServiceFavorites
    {
        private IRepositFavorites _repositFavorites;
        private IRepositUser _repositUser;
        private IRepositProduct _repositProduct;
        private IMapper _mapper;
        public ServiceFavorites(IRepositFavorites repositUserProduct, IRepositUser repositUser, IRepositProduct repositProduct, IMapper mapper)
        {
            _repositFavorites = repositUserProduct;
            _repositUser = repositUser;
            _repositProduct = repositProduct;
            _mapper = mapper;
        }

        public async Task<(int,int)?> Create(FavoritesDto element)
        {
            var mapElem = _mapper.Map<Favorites>(element);
            if (mapElem == null) throw new MappingApplicationException("Create element is not correct");

            var result = await _repositFavorites.Create(mapElem);

            if (result == null) throw new EntityCreateException("Favorite is not created");
            return result;
        }
        public async Task<bool> Delete(int idUser, int idProduct)
        {
            return await _repositFavorites.Delete(idUser, idProduct);
        }
        public async Task<IEnumerable<FavoritesDto>> ReadAll()
        {
            var allElem = await _repositFavorites.ReadAll();
            var mapAllElem = allElem.Select(q => _mapper.Map<FavoritesDto>(q)).ToList();
            return mapAllElem;
        }
        public async Task<FavoritesDto?> ReadById(int idUser, int idProduct)
        {
            var element = await _repositFavorites.ReadById(idUser, idProduct);
            if (element == null) throw new EntityNotFoundException("Favorite is not found");

            var mapElem = _mapper.Map<FavoritesDto>(element);
            return mapElem;
        }
    }
}
