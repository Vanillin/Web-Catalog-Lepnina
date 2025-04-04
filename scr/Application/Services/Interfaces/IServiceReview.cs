using Application.Dto;
using Application.Request;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{

    public interface IServiceReview
    {
        Task<ReviewDto?> ReadById(int id);
        Task<IEnumerable<ReviewDto>> ReadAll();
        Task<int?> Create(CreateReviewRequest element);
        Task<bool> Update(UpdateReviewRequest element);
        Task<bool> Delete(int id);
    }
}
