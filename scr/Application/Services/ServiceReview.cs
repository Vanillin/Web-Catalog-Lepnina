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

        public async Task<int?> Create(CreateReviewRequest request)
        {
            var user = await _repositUser.ReadById(request.IdUser);
            if (user == null) throw new EntityNotFoundException("User is not found");

            if (request.IdProduct != null)
            {
                var product = await _repositProduct.ReadById((int)request.IdProduct);
                if (product == null) throw new EntityNotFoundException("Product is not found");
            }

            var result = await _repositReview.Create(new Review()
            {
                Message = request.Message,
                PathPicture = request.PathPicture,
                IdUser = request.IdUser,
                IdProduct = request.IdProduct,
            }
            );

            if (result == null) throw new EntityCreateException("Review is not create");
            return result;
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
            if (element == null) throw new EntityNotFoundException("Review is not found");

            var mapElem = _mapper.Map<ReviewDto>(element);
            return mapElem;
        }
        public async Task<bool> Update(UpdateReviewRequest request)
        {
            var user = await _repositUser.ReadById(request.IdUser);
            if (user == null) throw new EntityNotFoundException("User is not found");

            if (request.IdProduct != null)
            {
                var product = await _repositProduct.ReadById((int)request.IdProduct);
                if (product == null) throw new EntityNotFoundException("Product is not found");
            }

            var result = await _repositReview.Update(new Review()
            {
                Id = request.Id,
                Message = request.Message,
                PathPicture = request.PathPicture,
                IdUser = request.IdUser,
                IdProduct = request.IdProduct,
            }
            );

            if (!result) throw new EntityUpdateException("Review is not updated");
            return true;
        }
    }
}
