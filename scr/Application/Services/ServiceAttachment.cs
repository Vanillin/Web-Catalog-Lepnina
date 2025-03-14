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

        public async Task Create(AttachmentDto element)
        {
            var mapElem = _mapper.Map<Attachment>(element);
            if (mapElem != null) await _repositExample.Create(mapElem);
        }
        public async Task<bool> Delete(int id)
        {
            return await _repositExample.Delete(id);
        }
        public async Task<List<AttachmentDto>> ReadAll()
        {
            var allElem = await _repositExample.ReadAll();
            var mapAllElem = allElem.Select(q => _mapper.Map<AttachmentDto>(q)).ToList();
            return mapAllElem;
        }
        public async Task<AttachmentDto> ReadById(int id)
        {
            var elem = await _repositExample.ReadById(id);
            var mapElem = _mapper.Map<AttachmentDto>(elem);
            return mapElem;
        }
        public async Task<bool> Update(AttachmentDto element)
        {
            var mapElem = _mapper.Map<Attachment>(element);
            return await _repositExample.Update(mapElem);
        }
    }
}
