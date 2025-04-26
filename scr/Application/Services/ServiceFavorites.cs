using Application.Dto;
using Application.Exception;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Repositories;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public class ServiceFavorites : IServiceFavorites
    {
        private IRepositFavorites _repositFavorites;
        private IMapper _mapper;
        private ILogger<ServiceFavorites> _logger;
        public ServiceFavorites(IRepositFavorites repositUserProduct, IMapper mapper, ILogger<ServiceFavorites> logger)
        {
            _repositFavorites = repositUserProduct;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<(int, int)?> Create(FavoritesDto element)
        {
            var mapElem = _mapper.Map<Favorites>(element);
            if (mapElem == null) throw new MappingApplicationException("Create element is not correct");

            var result = await _repositFavorites.Create(mapElem);

            if (result == null) throw new EntityCreateException("Favorite is not created");

            _logger.LogInformation("Favorite created with user id {UserId} and product id {ProductId}", result.Value.Item1, result.Value.Item2);
            return result;
        }
        public async Task<bool> Delete(int idUser, int idProduct)
        {
            var result = await _repositFavorites.Delete(idUser, idProduct);

            if (result) _logger.LogInformation("Favorite delete with user id {UserId} and product id {ProductId}", idUser, idProduct);
            return result;
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
