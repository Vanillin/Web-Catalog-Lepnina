using Application.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public class RepositExampleDto : IRepositExampleDto
    {
        private List<ExampleDto> _examples;
        public RepositExampleDto()
        {
            _examples = new List<ExampleDto>();
        }

        private ExampleDto Find(int id)
        {
            foreach (var v in _examples)
                if (v.Id == id)
                    return v;
            return null;
        }
        public Task Create(ExampleDto element)
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
        public Task<List<ExampleDto>> ReadAll()
        {
            return Task.FromResult(_examples);
        }
        public Task<ExampleDto> ReadById(int id)
        {
            var find = Find(id);
            return Task.FromResult(find);
        }
        public Task<bool> Update(ExampleDto element)
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
