using Domain.Entitys;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrostracture.Repositories
{
    public class RepositExample : IRepositExample
    {
        private List<Example> _examples;
        public RepositExample()
        {
            _examples = new List<Example>();
        }

        private Example Find(int id)
        {
            foreach (var v in _examples)
                if (v.Id == id)
                    return v;
            return null;
        }
        public Task Create(Example element)
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
        public Task<List<Example>> ReadAll()
        {
            return Task.FromResult(_examples);
        }
        public Task<Example> ReadById(int id)
        {
            var find = Find(id);
            return Task.FromResult(find);
        }
        public Task<bool> Update(Example element)
        {
            var find = Find(element.Id);
            if (find != null)
            {
                find.Message = element.Message;
                find.Path_picture = element.Path_picture;
                find.Id_product = element.Id_product;
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }
    }
}
