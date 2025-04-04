using Application.Dto;
using Application.Request;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IServiceAttachment
    {
        Task<AttachmentDto?> ReadById(int id);
        Task<IEnumerable<AttachmentDto>> ReadAll();
        Task<int?> Create(CreateAttachmentRequest element);
        Task<bool> Update(UpdateAttachmentRequest element);
        Task<bool> Delete(int id);
    }
}
