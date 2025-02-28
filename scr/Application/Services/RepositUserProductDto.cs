using Application.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public class RepositUserProductDto : IRepositUserProductDto
    {
        private List<UserProductDto> _userproducts;
        public RepositUserProductDto()
        {
            _userproducts = new List<UserProductDto>();
        }

        private UserProductDto Find(int id_user, int id_product)
        {
            foreach (var v in _userproducts)
                if (v.Id_user == id_user && v.Id_product == id_product)
                    return v;
            return null;
        }
        public Task Create(UserProductDto element)
        {
            _userproducts.Add(element);
            return Task.CompletedTask;
        }
        public Task<bool> Delete(int id_user, int id_product)
        {
            var find = Find(id_user, id_product);
            if (find != null)
            {
                _userproducts.Remove(find);
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }
        public Task<List<UserProductDto>> ReadAll()
        {
            return Task.FromResult(_userproducts);
        }
        public Task<UserProductDto> ReadById(int id_user, int id_product)
        {
            var find = Find(id_user, id_product);
            return Task.FromResult(find);
        }
    }
}
