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

        public async Task<int?> Create(SectionDto element)
        {
            var mapElem = _mapper.Map<Section>(element);
            if (mapElem == null) return null;
            return await _repositSection.Create(mapElem); //id is changed later
        }
        public async Task<bool> Delete(int id)
        {
            return await _repositSection.Delete(id);
        }
        public async Task<IEnumerable<SectionDto>> ReadAll()
        {
            var allElem = await _repositSection.ReadAll();
            var mapAllElem = allElem.Select(q => _mapper.Map<SectionDto>(q)).ToList();
            return mapAllElem;
        }
        public async Task<SectionDto?> ReadById(int id)
        {
            var element = await _repositSection.ReadById(id);
            if (element == null) return null;

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
