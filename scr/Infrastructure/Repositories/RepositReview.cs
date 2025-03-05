using Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class RepositReview : IRepositReview
    {
        private List<Review> _reviews;
        public RepositReview()
        {
            _reviews = new List<Review>();
        }

        private Review Find(int id)
        {
            return _reviews.FirstOrDefault(v => v.Id == id);
        }
        public Task Create(Review element)
        {
            _reviews.Add(element);
            return Task.CompletedTask;
        }
        public Task<bool> Delete(int id)
        {
            var find = Find(id);
            if (find != null)
            {
                _reviews.Remove(find);
                return Task.FromResult(true);
            }
            else
                return Task.FromResult(false);
        }
        public Task<List<Review>> ReadAll()
        {
            return Task.FromResult(_reviews);
        }
        public Task<Review> ReadById(int id)
        {
            var find = Find(id);
            return Task.FromResult(find);
        }
        public Task<bool> Update(Review element)
        {
            var find = Find(element.Id);
            if (find != null)
            {
                find.Message = element.Message;
                find.PathPicture = element.PathPicture;
                find.IdUser = element.IdUser;
                find.IdProduct = element.IdProduct;
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }
    }
}
