using Application.Dto;
using Infrastructure.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ServiceSection : IServiceSection
    {
        private IRepositSection _repositSection;
        public ServiceSection(IRepositSection repositSection)
        {
            _repositSection = repositSection;
        }

        public Task Create(SectionDto element)
        {
            throw new System.NotImplementedException();
        }
        public Task<bool> Delete(int id)
        {
            throw new System.NotImplementedException();
        }
        public Task<List<SectionDto>> ReadAll()
        {
            throw new System.NotImplementedException();
        }
        public Task<SectionDto> ReadById(int id)
        {
            throw new System.NotImplementedException();
        }
        public Task<bool> Update(SectionDto element)
        {
            throw new System.NotImplementedException();
        }
    }
}
