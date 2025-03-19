using System.Collections;
using System.ComponentModel.DataAnnotations;
using Domain.Entities;

namespace Infrastructure.Repositories
{
    public class RepositAttachment : IRepositAttachment
    {
        private List<Attachment> _attachments;
        public RepositAttachment()
        {
            _attachments = new List<Attachment>();

            _attachments.Add(new Attachment()
            {
                Id = 1,
                Message = "one",
                PathPicture = "onepath",
                IdProduct = 1,
            });
            _attachments.Add(new Attachment()
            {
                Id = 2,
                Message = "second",
                PathPicture = "secondpath",
                IdProduct = 3,
            });
        }

        private Attachment? Find(int id)
        {
            return _attachments.FirstOrDefault(v => v.Id == id);
        }
        private int FindMaxId()
        {
            int max = 0;
            foreach (var v in  _attachments) 
                if (v.Id > max)
                    max = v.Id;
            return max;
        }
        public Task<int> Create(Attachment element)
        {
            element.Id = FindMaxId() + 1;
            _attachments.Add(element);
            return Task.FromResult(element.Id);
        }
        public Task<bool> Delete(int id)
        {
            var find = Find(id);
            if (find != null)
            {
                _attachments.Remove(find);
                return Task.FromResult(true);
            }
            else
                return Task.FromResult(false);
        }
        public Task<IEnumerable<Attachment>> ReadAll()
        {
            IEnumerable<Attachment> retur = _attachments;
            return Task.FromResult(retur);
        }
        public Task<Attachment?> ReadById(int id)
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
