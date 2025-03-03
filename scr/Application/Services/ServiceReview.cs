using Application.Dto;
using Infrastructure.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ServiceReview : IServiceReview
    {
        private IRepositReview _repositReview;
        public ServiceReview(IRepositReview repositReview)
        {
            _repositReview = repositReview;
        }

        public Task Create(ReviewDto element)
        {
            throw new System.NotImplementedException();
        }
        public Task<bool> Delete(int id)
        {
            throw new System.NotImplementedException();
        }
        public Task<List<ReviewDto>> ReadAll()
        {
            throw new System.NotImplementedException();
        }        
        public Task<ReviewDto> ReadById(int id)
        {
            throw new System.NotImplementedException();
        }
        public Task<bool> Update(ReviewDto element)
        {
            throw new System.NotImplementedException();
        }
    }
}
