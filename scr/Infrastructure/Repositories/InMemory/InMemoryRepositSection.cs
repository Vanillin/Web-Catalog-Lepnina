//using Domain.Entities;

//namespace Infrastructure.Repositories
//{
//    public class InMemoryRepositSection : IRepositSection
//    {
//        private List<Section> _sections;
//        public InMemoryRepositSection()
//        {
//            _sections = new List<Section>();

//            _sections.Add(new Section()
//            {
//                Id = 1,
//                Name = "first"
//            });
//            _sections.Add(new Section()
//            {
//                Id = 2,
//                Name = "second"
//            });
//        }

//        private Section? Find(int id)
//        {
//            return _sections.FirstOrDefault(v => v.Id == id);
//        }
//        private int FindMaxId()
//        {
//            int max = 0;
//            foreach (var v in _sections)
//                if (v.Id > max)
//                    max = v.Id;
//            return max;
//        }
//        public Task<int> Create(Section element)
//        {
//            element.Id = FindMaxId() + 1;
//            _sections.Add(element);
//            return Task.FromResult(element.Id);
//        }
//        public Task<bool> Delete(int id)
//        {
//            var find = Find(id);
//            if (find != null)
//            {
//                _sections.Remove(find);
//                return Task.FromResult(true);
//            }
//            else
//                return Task.FromResult(false);
//        }
//        public Task<IEnumerable<Section>> ReadAll()
//        {
//            IEnumerable<Section> sections = _sections;
//            return Task.FromResult(sections);
//        }
//        public Task<Section?> ReadById(int id)
//        {
//            var find = Find(id);
//            return Task.FromResult(find);
//        }
//        public Task<bool> Update(Section element)
//        {
//            var find = Find(element.Id);
//            if (find != null)
//            {
//                find.Name = element.Name;
//                return Task.FromResult(true);
//            }
//            return Task.FromResult(false);
//        }
//    }
//}
