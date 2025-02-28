using Application.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public class RepositSectionDto : IRepositSectionDto
    {
        private List<SectionDto> _sections;
        public RepositSectionDto()
        {
            _sections = new List<SectionDto>();
        }

        private SectionDto Find(int id)
        {
            foreach (var v in _sections)
                if (v.Id == id)
                    return v;
            return null;
        }
        public Task Create(SectionDto element)
        {
            _sections.Add(element);
            return Task.CompletedTask;
        }
        public Task<bool> Delete(int id)
        {
            var find = Find(id);
            if (find != null)
            {
                _sections.Remove(find);
                return Task.FromResult(true);
            }
            else
                return Task.FromResult(false);
        }
        public Task<List<SectionDto>> ReadAll()
        {
            return Task.FromResult(_sections);
        }
        public Task<SectionDto> ReadById(int id)
        {
            var find = Find(id);
            return Task.FromResult(find);
        }
        public Task<bool> Update(SectionDto element)
        {
            var find = Find(element.Id);
            if (find != null)
            {
                find.Name = element.Name;
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }
    }
}
