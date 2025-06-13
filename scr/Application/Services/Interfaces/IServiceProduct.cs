using Application.Dto;
using Application.Exception;
using Application.Request;
using Infrastructure.Repositories;
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
        Task<IEnumerable<ProductDto>> GetBySection(int id);
        Task<IEnumerable<ProductDto>> GetByFavorite(int idUser);
    }
}
