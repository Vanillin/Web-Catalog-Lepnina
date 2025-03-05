using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public interface IRepositAttachment
    {
        Task<Attachment> ReadById(int id);
        Task<List<Attachment>> ReadAll();
        Task Create(Attachment element);
        Task<bool> Update(Attachment element);
        Task<bool> Delete(int id);
    }
}
