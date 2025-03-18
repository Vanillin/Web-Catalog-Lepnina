using Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class RepositFavorites : IRepositFavorites
    {
        private List<Favorites> _userFavorites;
        public RepositFavorites()
        {
            _userFavorites = new List<Favorites>();

            _userFavorites.Add(new Favorites()
            {
                IdUser = 1,
                IdProduct = 2,
            });
            _userFavorites.Add(new Favorites()
            {
                IdUser = 2,
                IdProduct = 3,
            });
        }

        private Favorites? Find(int idUser, int idProduct)
        {
            return _userFavorites.FirstOrDefault(v => v.IdUser == idUser && v.IdProduct == idProduct);
        }
        public Task<(int,int)> Create(Favorites element)
        {
            _userFavorites.Add(element);
            return Task.FromResult((element.IdUser, element.IdProduct));
        }
        public Task<bool> Delete(int idUser, int idProduct)
        {
            var find = Find(idUser, idProduct);
            if (find != null)
            {
                _userFavorites.Remove(find);
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }
        public Task<IEnumerable<Favorites>> ReadAll()
        {
            IEnumerable<Favorites> favorites = _userFavorites;
            return Task.FromResult(favorites);
        }
        public Task<Favorites?> ReadById(int idUser, int idProduct)
        {
            var find = Find(idUser, idProduct);
            return Task.FromResult(find);
        }
    }
}
