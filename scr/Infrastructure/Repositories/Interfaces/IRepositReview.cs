using Domain.Entities;

namespace Infrastructure.Repositories
{

    public interface IRepositReview
    {
        Task<Review?> ReadById(int id);
        Task<IEnumerable<Review>> ReadAll();
        Task<int?> Create(Review element);
        Task<bool> Update(Review element);
        Task<bool> Delete(int id);
    }
}
