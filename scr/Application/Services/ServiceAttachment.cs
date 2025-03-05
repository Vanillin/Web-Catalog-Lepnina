using Application.Dto;
using Infrastructure.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ServiceAttachment : IServiceAttachment
    {
        private IRepositAttachment _repositExample;
        public ServiceAttachment(IRepositAttachment repositExample)
        {
            _repositExample = repositExample;
        }

        public Task Create(AttachmentDto element)
        {
            throw new System.NotImplementedException();
        }
        public Task<bool> Delete(int id)
        {
            throw new System.NotImplementedException();
        }
        public Task<List<AttachmentDto>> ReadAll()
        {
            throw new System.NotImplementedException();
        }
        public Task<AttachmentDto> ReadById(int id)
        {
            throw new System.NotImplementedException();
        }
        public Task<bool> Update(AttachmentDto element)
        {
            throw new System.NotImplementedException();
        }
    }
}
