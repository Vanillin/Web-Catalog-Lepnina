using Application.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{

    public interface IServiceProduct
    {
        Task<ProductDto> ReadById(int id);
        Task<List<ProductDto>> ReadAll();
        Task Create(ProductDto element);
        Task<bool> Update(ProductDto element);
        Task<bool> Delete(int id);
    }
}
