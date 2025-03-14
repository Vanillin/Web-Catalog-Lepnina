using Application.Dto;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Repositories;

namespace Application.Services
{
    public class ServiceProduct : IServiceProduct
    {
        private IRepositProduct _repositProduct;
        private IMapper _mapper;
        public ServiceProduct(IRepositProduct repositProduct, IMapper mapper)
        {
            _repositProduct = repositProduct;
            _mapper = mapper;
        }

        public async Task Create(ProductDto element)
        {
            var mapElem = _mapper.Map<Product>(element);
            if (mapElem != null) await _repositProduct.Create(mapElem);
        }
        public async Task<bool> Delete(int id)
        {
            return await _repositProduct.Delete(id);
        }
        public async Task<List<ProductDto>> ReadAll()
        {
            var allElem = await _repositProduct.ReadAll();
            var mapAllElem = allElem.Select(q => _mapper.Map<ProductDto>(q)).ToList();
            return mapAllElem;
        }
        public async Task<ProductDto> ReadById(int id)
        {
            var element = await _repositProduct.ReadById(id);
            var mapElem = _mapper.Map<ProductDto>(element);
            return mapElem;
        }
        public async Task<bool> Update(ProductDto element)
        {
            var mapElem = _mapper.Map<Product>(element);
            return await _repositProduct.Update(mapElem);
        }
    }
}
