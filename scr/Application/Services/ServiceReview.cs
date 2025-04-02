using Application.Dto;
using Application.Exception;
using Application.Request;
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

        public async Task<int?> Create(CreateReviewRequest element)
        {
            var user = await _repositUser.ReadById(element.IdUser);
            if (user == null) throw new NotFoundApplicationException("User is not found");

            if (element.IdProduct != null)
            {
                var product = await _repositProduct.ReadById((int)element.IdProduct);
                if (product == null) throw new NotFoundApplicationException("Product is not found");
            }

            return await _repositReview.Create(new Review()
            {
                Message = element.Message,
                PathPicture = element.PathPicture,
                IdUser = element.IdUser,
                IdProduct = element.IdProduct,
            }
            );
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
            if (element == null) throw new NotFoundApplicationException("Review is not found");

            var mapElem = _mapper.Map<ReviewDto>(element);
            return mapElem;
        }
        public async Task<bool> Update(UpdateReviewRequest element)
        {
            var user = await _repositUser.ReadById(element.IdUser);
            if (user == null) throw new NotFoundApplicationException("User is not found");

            if (element.IdProduct != null)
            {
                var product = await _repositProduct.ReadById((int)element.IdProduct);
                if (product == null) throw new NotFoundApplicationException("Product is not found");
            }

            return await _repositReview.Update(new Review()
            {
                Id = element.Id,
                Message = element.Message,
                PathPicture = element.PathPicture,
                IdUser = element.IdUser,
                IdProduct = element.IdProduct,
            }
            );
        }
    }
}
