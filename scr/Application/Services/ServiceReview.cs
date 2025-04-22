using Application.Dto;
using Application.Exception;
using Application.Request;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Repositories;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public class ServiceReview : IServiceReview
    {
        private IRepositReview _repositReview;
        private IMapper _mapper;
        private ILogger<ServiceReview> _logger;
        public ServiceReview(IRepositReview repositReview, IMapper mapper, ILogger<ServiceReview> logger)
        {
            _repositReview = repositReview;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<int?> Create(CreateReviewRequest request)
        {
            var result = await _repositReview.Create(new Review()
            {
                Message = request.Message,
                PathPicture = request.PathPicture,
                IdUser = request.IdUser,
                IdProduct = request.IdProduct,
            }
            );

            if (result == null) throw new EntityCreateException("Review is not created");

            _logger.LogInformation($"Review created with id {result}");
            return result;
        }
        public async Task<bool> Delete(int id)
        {
            var result = await _repositReview.Delete(id);

            if (result) _logger.LogInformation($"Review deleted with id {id}");
            return result;
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
            var element = await _repositReview.ReadById(request.Id);
            if (element == null) throw new EntityNotFoundException("Review is not found");

            element.Message = request.Message;
            element.PathPicture = request.PathPicture;
            element.IdUser = request.IdUser;
            element.IdProduct = request.IdProduct;

            var result = await _repositReview.Update(element);

            if (!result) throw new EntityUpdateException("Review is not updated");

            _logger.LogInformation($"Review updated with id {element.Id}");
            return true;
        }
    }
}
