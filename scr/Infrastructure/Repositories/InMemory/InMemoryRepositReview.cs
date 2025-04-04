//using Domain.Entities;

//namespace Infrastructure.Repositories
//{
//    public class InMemoryRepositReview : IRepositReview
//    {
//        private List<Review> _reviews;
//        public InMemoryRepositReview()
//        {
//            _reviews = new List<Review>();

//            _reviews.Add(new Review()
//            {
//                Id = 1,
//                Message = "firstmessage",
//                PathPicture = "firstpath",
//                IdUser = 1,
//                IdProduct = 2,
//            });
//            _reviews.Add(new Review()
//            {
//                Id = 2,
//                Message = "secondmessage",
//                PathPicture = "secondpath",
//                IdUser = 3,
//                IdProduct = 1,
//            });
//        }

//        private Review? Find(int id)
//        {
//            return _reviews.FirstOrDefault(v => v.Id == id);
//        }
//        private int FindMaxId()
//        {
//            int max = 0;
//            foreach (var v in _reviews)
//                if (v.Id > max)
//                    max = v.Id;
//            return max;
//        }
//        public Task<int> Create(Review element)
//        {
//            element.Id = FindMaxId() + 1;
//            _reviews.Add(element);
//            return Task.FromResult(element.Id);
//        }
//        public Task<bool> Delete(int id)
//        {
//            var find = Find(id);
//            if (find != null)
//            {
//                _reviews.Remove(find);
//                return Task.FromResult(true);
//            }
//            else
//                return Task.FromResult(false);
//        }
//        public Task<IEnumerable<Review>> ReadAll()
//        {
//            IEnumerable<Review> reviews = _reviews;
//            return Task.FromResult(reviews);
//        }
//        public Task<Review?> ReadById(int id)
//        {
//            var find = Find(id);
//            return Task.FromResult(find);
//        }
//        public Task<bool> Update(Review element)
//        {
//            var find = Find(element.Id);
//            if (find != null)
//            {
//                find.Message = element.Message;
//                find.PathPicture = element.PathPicture;
//                find.IdUser = element.IdUser;
//                find.IdProduct = element.IdProduct;
//                return Task.FromResult(true);
//            }
//            return Task.FromResult(false);
//        }
//    }
//}
