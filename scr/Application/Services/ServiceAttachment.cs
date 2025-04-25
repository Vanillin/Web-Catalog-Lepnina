using Application.Dto;
using Application.Exception;
using Application.Request;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Repositories;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public class ServiceAttachment : IServiceAttachment
    {
        private IRepositAttachment _repositAttachment;
        private IMapper _mapper;
        private ILogger<ServiceAttachment> _logger;
        public ServiceAttachment(IRepositAttachment repositExample, IMapper mapper, ILogger<ServiceAttachment> logger)
        {
            _repositAttachment = repositExample;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<int?> Create(CreateAttachmentRequest request)
        {
            var result = await _repositAttachment.Create(
                new Attachment()
                {
                    IdProduct = request.IdProduct,
                    Message = request.Message,
                    PathPicture = request.PathPicture,
                }
                );

            if (result == null) throw new EntityCreateException("Attachment is not created");

            _logger.LogInformation("Attachment created with id {AttachmentId}", result);
            return result;
        }
        public async Task<bool> Delete(int id)
        {
            var result = await _repositAttachment.Delete(id);

            if (result) _logger.LogInformation("Attachment deleted with id {AttachmentId}", id);
            return result;
        }
        public async Task<IEnumerable<AttachmentDto>> ReadAll()
        {
            var allElem = await _repositAttachment.ReadAll();
            var mapAllElem = allElem.Select(q => _mapper.Map<AttachmentDto>(q)).ToList();
            return mapAllElem;
        }
        public async Task<AttachmentDto?> ReadById(int id)
        {
            var element = await _repositAttachment.ReadById(id);
            if (element == null) throw new EntityNotFoundException("Attachment is not found"); ;

            var mapElem = _mapper.Map<AttachmentDto>(element);
            return mapElem;
        }
        public async Task<bool> Update(UpdateAttachmentRequest request)
        {
            var element = await _repositAttachment.ReadById(request.Id);
            if (element == null) throw new EntityNotFoundException("Attachment is not found");

            element.IdProduct = request.IdProduct;
            element.Message = request.Message;
            element.PathPicture = request.PathPicture;

            var result = await _repositAttachment.Update(element);

            if (!result) throw new EntityUpdateException("Attachment is not updated");

            _logger.LogInformation("Attachment updated with id {AttachmentId}", element.Id);
            return true;
        }
    }
}
