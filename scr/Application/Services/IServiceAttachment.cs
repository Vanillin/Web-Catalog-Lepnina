using Application.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IServiceAttachment
    {
        Task<AttachmentDto> ReadById(int id);
        Task<List<AttachmentDto>> ReadAll();
        Task Create(AttachmentDto element);
        Task<bool> Update(AttachmentDto element);
        Task<bool> Delete(int id);
    }
}
