using Application.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{

    public interface IRepositReviewDto
    {
        Task<ReviewDto> ReadById(int id);
        Task<List<ReviewDto>> ReadAll();
        Task Create(ReviewDto element);
        Task<bool> Update(ReviewDto element);
        Task<bool> Delete(int id);
    }
}
