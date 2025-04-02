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

            var user = await _repositUser.ReadById(mapElem.IdUser);
            if (user == null) throw new NotFoundApplicationException("User is not found");

            var product = await _repositProduct.ReadById(mapElem.IdProduct);
            if (product == null) throw new NotFoundApplicationException("Product is not found");

            FavoritesDto? id = await ReadById(element.IdUser, element.IdProduct);
            if (id == null) return await _repositFavorites.Create(mapElem);
            else throw new AlreadyExistsApplicationException("Favorite already exists");
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
            if (element == null) throw new NotFoundApplicationException("Favorite is not found");

            var mapElem = _mapper.Map<FavoritesDto>(element);
            return mapElem;
        }
    }
}
