using Application.Dto;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Repositories;

namespace Application.Services
{
    public class ServiceReview : IServiceReview
    {
        private IRepositReview _repositReview;
        private IMapper _mapper;
        public ServiceReview(IRepositReview repositReview, IMapper mapper)
        {
            _repositReview = repositReview;
            _mapper = mapper;
        }

        public async Task Create(ReviewDto element)
        {
            var mapElem = _mapper.Map<Review>(element);
            if (mapElem != null) await _repositReview.Create(mapElem);
        }
        public async Task<bool> Delete(int id)
        {
            return await _repositReview.Delete(id);
        }
        public async Task<List<ReviewDto>> ReadAll()
        {
            var allElem = await _repositReview.ReadAll();
            var mapAllElem = allElem.Select(q => _mapper.Map<ReviewDto>(q)).ToList();
            return mapAllElem;
        }
        public async Task<ReviewDto> ReadById(int id)
        {
            var element = await _repositReview.ReadById(id);
            var mapElem = _mapper.Map<ReviewDto>(element);
            return mapElem;
        }
        public async Task<bool> Update(ReviewDto element)
        {
            var mapElem = _mapper.Map<Review>(element);
            return await _repositReview.Update(mapElem);
        }
    }
}
