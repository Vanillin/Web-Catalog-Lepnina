using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{

    public interface IRepositReview
    {
        Task<Review> ReadById(int id);
        Task<List<Review>> ReadAll();
        Task Create(Review element);
        Task<bool> Update(Review element);
        Task<bool> Delete(int id);
    }
}
