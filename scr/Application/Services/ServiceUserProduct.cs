using Application.Dto;
using Infrastructure.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ServiceUserProduct : IServiceUserProduct
    {
        private IRepositUserProduct _repositUserProduct;
        public ServiceUserProduct(IRepositUserProduct repositUserProduct)
        {
            _repositUserProduct = repositUserProduct;
        }

        public Task Create(UserProductDto element)
        {
            throw new System.NotImplementedException();
        }
        public Task<bool> Delete(int idUser, int idProduct)
        {
            throw new System.NotImplementedException();
        }                
        public Task<List<UserProductDto>> ReadAll()
        {
            throw new System.NotImplementedException();
        }
        public Task<UserProductDto> ReadById(int idUser, int idProduct)
        {
            throw new System.NotImplementedException();
        }
    }
}
