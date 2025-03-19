using System.Xml.Linq;
using Application.Dto;
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

        public async Task<int?> Create(AttachmentDto element)
        {
            var mapElem = _mapper.Map<Attachment>(element);
            if (mapElem == null) return null;

            var product = _repositProduct.ReadById(mapElem.IdProduct);
            if (product == null) return null;

            return await _repositAttachment.Create(mapElem); //id is changed later
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
            if (element == null) return null;

            var mapElem = _mapper.Map<AttachmentDto>(element);
            return mapElem;
        }
        public async Task<bool> Update(AttachmentDto element)
        {
            var mapElem = _mapper.Map<Attachment>(element);
            if (mapElem == null) return false;

            var product = _repositProduct.ReadById(mapElem.IdProduct);
            if (product == null) return false;

            return await _repositAttachment.Update(mapElem);
        }
    }
}
