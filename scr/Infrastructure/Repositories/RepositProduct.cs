using Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class RepositProduct : IRepositProduct
    {
        private List<Product> _products;
        public RepositProduct()
        {
            _products = new List<Product>();

            _products.Add(new Product()
            {
                Id = 1,
                Length = 50,
                Height = 50,
                Width = 50,
                Priсe = 100,
                Discount = 0,
                PathPicture = "firsticon",
                IdSection = 1,
            });
            _products.Add(new Product()
            {
                Id = 2,
                Length = 50,
                Height = 50,
                Width = 50,
                Priсe = 100,
                Discount = 0,
                PathPicture = "secondicon",
                IdSection = 1,
            });
            _products.Add(new Product()
            {
                Id = 3,
                Length = 50,
                Height = 50,
                Width = 50,
                Priсe = 100,
                Discount = 0,
                PathPicture = "thirdicon",
                IdSection = 2,
            });
        }

        private Product? Find(int id)
        {
            return _products.FirstOrDefault(v => v.Id == id);
        }
        public Task<int> Create(Product element)
        {
            _products.Add(element);
            return Task.FromResult(element.Id);
        }
        public Task<bool> Delete(int id)
        {
            var find = Find(id);
            if (find != null)
            {
                _products.Remove(find);
                return Task.FromResult(true);
            }
            else
                return Task.FromResult(false);
        }
        public Task<IEnumerable<Product>> ReadAll()
        {
            IEnumerable<Product> products = _products;
            return Task.FromResult(products);
        }
        public Task<Product?> ReadById(int id)
        {
            var find = Find(id);
            return Task.FromResult(find);
        }
        public Task<bool> Update(Product element)
        {
            var find = Find(element.Id);
            if (find != null)
            {
                find.Length = element.Length;
                find.Height = element.Height;
                find.Width = element.Width;
                find.Priсe = element.Priсe;
                find.Discount = element.Discount;
                find.PathPicture = element.PathPicture;
                find.IdSection = element.IdSection;
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }
    }
}
