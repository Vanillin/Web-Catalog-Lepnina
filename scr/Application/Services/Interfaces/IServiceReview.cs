using Application.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{

    public interface IServiceReview
    {
        Task<ReviewDto?> ReadById(int id);
        Task<IEnumerable<ReviewDto>> ReadAll();
        Task<int?> Create(ReviewDto element);
        Task<bool> Update(ReviewDto element);
        Task<bool> Delete(int id);
    }
}
