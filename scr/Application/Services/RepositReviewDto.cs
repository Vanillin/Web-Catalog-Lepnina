using Application.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public class RepositReviewDto : IRepositReviewDto
    {
        private List<ReviewDto> _reviews;
        public RepositReviewDto()
        {
            _reviews = new List<ReviewDto>();
        }

        private ReviewDto Find(int id)
        {
            foreach (var v in _reviews)
                if (v.Id == id)
                    return v;
            return null;
        }
        public Task Create(ReviewDto element)
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
        public Task<List<ReviewDto>> ReadAll()
        {
            return Task.FromResult(_reviews);
        }
        public Task<ReviewDto> ReadById(int id)
        {
            var find = Find(id);
            return Task.FromResult(find);
        }
        public Task<bool> Update(ReviewDto element)
        {
            var find = Find(element.Id);
            if (find != null)
            {
                find.Message = element.Message;
                find.Path_picture = element.Path_picture;
                find.Id_user = element.Id_user;
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }
    }
}
