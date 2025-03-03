using Application.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IServiceSection
    {
        Task<SectionDto> ReadById(int id);
        Task<List<SectionDto>> ReadAll();
        Task Create(SectionDto element);
        Task<bool> Update(SectionDto element);
        Task<bool> Delete(int id);
    }
}
