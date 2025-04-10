using Application.Dto;
using Application.Exception;
using Application.Request;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Repositories;

namespace Application.Services
{
    public class ServiceAttachment : IServiceAttachment
    {
        private IRepositAttachment _repositAttachment;
        private IRepositProduct _repositProduct;
        private IMapper _mapper;
        public ServiceAttachment(IRepositAttachment repositExample, IRepositProduct repositProduct, IMapper mapper)
        {
            _repositAttachment = repositExample;
            _repositProduct = repositProduct;
            _mapper = mapper;
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
            return result;
        }
        public async Task<bool> Delete(int id)
        {
            return await _repositAttachment.Delete(id);
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
            return true;
        }
    }
}
