using Application.Dto;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Repositories;

namespace Application.Services
{
    public class ServiceReview : IServiceReview
    {
        private IRepositReview _repositReview;
        private IRepositUser _repositUser;
        private IRepositProduct _repositProduct;
        private IMapper _mapper;
        public ServiceReview(IRepositReview repositReview, IRepositUser repositUser, IRepositProduct repositProduct, IMapper mapper)
        {
            _repositReview = repositReview;
            _repositUser = repositUser;
            _repositProduct = repositProduct;
            _mapper = mapper;
        }

        public async Task<int?> Create(ReviewDto element)
        {
            var user = _repositUser.ReadById(element.IdUser).Result;
            if (user == null) return null;

            if (element.IdProduct != null)
            {
                var product = _repositProduct.ReadById((int)element.IdProduct).Result;
                if (product == null) return null;
            }

            var mapElem = _mapper.Map<Review>(element);
            if (mapElem == null) return null;
            return await _repositReview.Create(mapElem); //id is changed later
        }
        public async Task<bool> Delete(int id)
        {
            return await _repositReview.Delete(id);
        }
        public async Task<IEnumerable<ReviewDto>> ReadAll()
        {
            var allElem = await _repositReview.ReadAll();
            var mapAllElem = allElem.Select(q => _mapper.Map<ReviewDto>(q)).ToList();
            return mapAllElem;
        }
        public async Task<ReviewDto?> ReadById(int id)
        {
            var element = await _repositReview.ReadById(id);
            if (element == null) return null;

            var mapElem = _mapper.Map<ReviewDto>(element);
            return mapElem;
        }
        public async Task<bool> Update(ReviewDto element)
        {
            var mapElem = _mapper.Map<Review>(element);
            if (mapElem == null) return false;

            var user = _repositUser.ReadById(mapElem.IdUser).Result;
            if (user == null) return false;

            if (mapElem.IdProduct != null)
            {
                var product = _repositProduct.ReadById((int)mapElem.IdProduct).Result;
                if (product == null) return false;
            }

            return await _repositReview.Update(mapElem);
        }
    }
}
