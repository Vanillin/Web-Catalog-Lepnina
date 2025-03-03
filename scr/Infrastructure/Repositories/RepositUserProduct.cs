using Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class RepositUserProduct : IRepositUserProduct
    {
        private List<UserProduct> _userProducts;
        public RepositUserProduct()
        {
            _userProducts = new List<UserProduct>();
        }

        private UserProduct Find(int idUser, int idProduct)
        {
            return _userProducts.FirstOrDefault(v => v.IdUser == idUser && v.IdProduct == idProduct);
        }
        public Task Create(UserProduct element)
        {
            _userProducts.Add(element);
            return Task.CompletedTask;
        }
        public Task<bool> Delete(int idUser, int idProduct)
        {
            var find = Find(idUser, idProduct);
            if (find != null)
            {
                _userProducts.Remove(find);
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }
        public Task<List<UserProduct>> ReadAll()
        {
            return Task.FromResult(_userProducts);
        }
        public Task<UserProduct> ReadById(int idUser, int idProduct)
        {
            var find = Find(idUser, idProduct);
            return Task.FromResult(find);
        }
    }
}
