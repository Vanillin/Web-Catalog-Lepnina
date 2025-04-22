using Application.Dto;
using Application.Request;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IServiceSection
    {
        Task<SectionDto?> ReadById(int id);
        Task<IEnumerable<SectionDto>> ReadAll();
        Task<int?> Create(CreateSectionRequest element);
        Task<bool> Update(UpdateSectionRequest element);
        Task<bool> Delete(int id);
    }
}
