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
        }

        private Product Find(int id)
        {
            return _products.FirstOrDefault(v => v.Id == id);
        }
        public Task Create(Product element)
        {
            _products.Add(element);
            return Task.CompletedTask;
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
        public Task<List<Product>> ReadAll()
        {
            return Task.FromResult(_products);
        }
        public Task<Product> ReadById(int id)
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
