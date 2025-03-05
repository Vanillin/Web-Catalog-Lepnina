using Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class RepositAttachment : IRepositAttachment
    {
        private List<Attachment> _examples;
        public RepositAttachment()
        {
            _examples = new List<Attachment>();
        }

        private Attachment Find(int id)
        {
            return _examples.FirstOrDefault(v => v.Id == id);
        }
        public Task Create(Attachment element)
        {
            _examples.Add(element);
            return Task.CompletedTask;
        }
        public Task<bool> Delete(int id)
        {
            var find = Find(id);
            if (find != null)
            {
                _examples.Remove(find);
                return Task.FromResult(true);
            }
            else
                return Task.FromResult(false);
        }
        public Task<List<Attachment>> ReadAll()
        {
            return Task.FromResult(_examples);
        }
        public Task<Attachment> ReadById(int id)
        {
            var find = Find(id);
            return Task.FromResult(find);
        }
        public Task<bool> Update(Attachment element)
        {
            var find = Find(element.Id);
            if (find != null)
            {
                find.Message = element.Message;
                find.PathPicture = element.PathPicture;
                find.IdProduct = element.IdProduct;
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }
    }
}
