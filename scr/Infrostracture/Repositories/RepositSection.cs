using Domain.Entitys;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrostracture.Repositories
{
    public class RepositSection : IRepositSection
    {
        private List<Section> _sections;
        public RepositSection()
        {
            _sections = new List<Section>();
        }

        private Section Find(int id)
        {
            foreach (var v in _sections)
                if (v.Id == id)
                    return v;
            return null;
        }
        public Task Create(Section element)
        {
            _sections.Add(element);
            return Task.CompletedTask;
        }
        public Task<bool> Delete(int id)
        {
            var find = Find(id);
            if (find != null)
            {
                _sections.Remove(find);
                return Task.FromResult(true);
            }
            else
                return Task.FromResult(false);
        }
        public Task<List<Section>> ReadAll()
        {
            return Task.FromResult(_sections);
        }
        public Task<Section> ReadById(int id)
        {
            var find = Find(id);
            return Task.FromResult(find);
        }
        public Task<bool> Update(Section element)
        {
            var find = Find(element.Id);
            if (find != null)
            {
                find.Name = element.Name;
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }
    }
}
