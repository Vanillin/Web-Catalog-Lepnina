using Application.Dto;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Repositories;

namespace Application.Services
{
    public class ServiceSection : IServiceSection
    {
        private IRepositSection _repositSection;
        private IMapper _mapper;
        public ServiceSection(IRepositSection repositSection, IMapper mapper)
        {
            _repositSection = repositSection;
            _mapper = mapper;
        }

        public async Task Create(SectionDto element)
        {
            var mapElem = _mapper.Map<Section>(element);
            if (mapElem != null) await _repositSection.Create(mapElem);
        }
        public async Task<bool> Delete(int id)
        {
            return await _repositSection.Delete(id);
        }
        public async Task<List<SectionDto>> ReadAll()
        {
            var allElem = await _repositSection.ReadAll();
            var mapAllElem = allElem.Select(q => _mapper.Map<SectionDto>(q)).ToList();
            return mapAllElem;
        }
        public async Task<SectionDto> ReadById(int id)
        {
            var element = await _repositSection.ReadById(id);
            var mapElem = _mapper.Map<SectionDto>(element);
            return mapElem;
        }
        public async Task<bool> Update(SectionDto element)
        {
            var mapElem = _mapper.Map<Section>(element);
            return await _repositSection.Update(mapElem);
        }
    }
}
