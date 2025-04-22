using Application.Dto;
using Application.Request;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{

    public interface IServiceProduct
    {
        Task<ProductDto?> ReadById(int id);
        Task<IEnumerable<ProductDto>> ReadAll();
        Task<int?> Create(CreateProductRequest element);
        Task<bool> Update(UpdateProductRequest element);
        Task<bool> Delete(int id);
    }
}
