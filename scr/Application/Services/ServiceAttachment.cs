using System.Xml.Linq;
using Application.Dto;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Repositories;

namespace Application.Services
{
    public class ServiceAttachment : IServiceAttachment
    {
        private IRepositAttachment _repositExample;
        private IMapper _mapper;
        public ServiceAttachment(IRepositAttachment repositExample, IMapper mapper)
        {
            _repositExample = repositExample;
            _mapper = mapper;
        }

        public async Task<int?> Create(AttachmentDto element)
        {
            var mapElem = _mapper.Map<Attachment>(element);
            if (mapElem == null) return null;
            if (ReadById(element.Id) == null) return await _repositExample.Create(mapElem);
            else return null;
        }
        public async Task<bool> Delete(int id)
        {
            return await _repositExample.Delete(id);
        }
        public async Task<IEnumerable<AttachmentDto>> ReadAll()
        {
            var allElem = await _repositExample.ReadAll();
            var mapAllElem = allElem.Select(q => _mapper.Map<AttachmentDto>(q)).ToList();
            return mapAllElem;
        }
        public async Task<AttachmentDto?> ReadById(int id)
        {
            var element = await _repositExample.ReadById(id);
            if (element == null) return null;

            var mapElem = _mapper.Map<AttachmentDto>(element);
            return mapElem;
        }
        public async Task<bool> Update(AttachmentDto element)
        {
            var mapElem = _mapper.Map<Attachment>(element);
            return await _repositExample.Update(mapElem);
        }
    }
}
