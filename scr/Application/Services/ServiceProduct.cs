using Application.Dto;
using Infrastructure.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ServiceProduct : IServiceProduct
    {
        private IRepositProduct _repositProduct;
        public ServiceProduct(IRepositProduct repositProduct)
        {
            _repositProduct = repositProduct;
        }

        public Task Create(ProductDto element)
        {
            throw new System.NotImplementedException();
        }
        public Task<bool> Delete(int id)
        {
            throw new System.NotImplementedException();
        }
        public Task<List<ProductDto>> ReadAll()
        {
            throw new System.NotImplementedException();
        }
        public Task<ProductDto> ReadById(int id)
        {
            throw new System.NotImplementedException();
        }
        public Task<bool> Update(ProductDto element)
        {
            throw new System.NotImplementedException();
        }
    }
}
