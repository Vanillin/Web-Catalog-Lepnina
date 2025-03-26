using Application.Dto;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Repositories;

namespace Application.Services
{
    public class ServiceUser : IServiceUser
    {
        private IRepositUser _repositUser;
        private IRepositReview _repositReview;
        private IRepositFavorites _repositFavorites;
        private IMapper _mapper;
        public ServiceUser(IRepositUser repositUser, IRepositReview repositReview, IRepositFavorites repositFavorites, IMapper mapper)
        {
            _repositUser = repositUser;
            _repositReview = repositReview;
            _repositFavorites = repositFavorites;
            _mapper = mapper;
        }

        public async Task<int?> Create(UserDto element)
        {
            var mapElem = _mapper.Map<User>(element);
            if (mapElem == null) return null;
            return await _repositUser.Create(mapElem); //id is changed later
        }
        public async Task<bool> Delete(int id) //must be transaction
        {
            UserDto? element = await ReadById(id);
            if (element == null) return false;
            List<Favorites> memoryFavor = new List<Favorites>();
            List<Review> memoryReviews = new List<Review>();

            try
            {
                var favorites = _repositFavorites.ReadAll().Result.Where(x => x.IdUser == id).ToList();
                foreach (var v in favorites)
                {
                    var resultFavourite = await _repositFavorites.Delete(v.IdUser, v.IdProduct);
                    if (!resultFavourite) throw new Exception();
                    memoryFavor.Add(v);
                }

                var reviews = _repositReview.ReadAll().Result.Where(x => x.IdUser == id).ToList();
                foreach (var v in reviews)
                {
                    var resultReview = await _repositReview.Delete(v.Id);
                    if (!resultReview) throw new Exception();
                    memoryReviews.Add(v);
                }

                var result = await _repositUser.Delete(id);
                if (!result) throw new Exception();

                return true;
            }
            catch (Exception)
            {
                foreach (var v in memoryFavor)
                {
                    await _repositFavorites.Create(v);
                }
                foreach (var v in memoryReviews)
                {
                    await _repositReview.Create(v);
                }

                return false;
            }
        }
        public async Task<IEnumerable<UserDto>> ReadAll()
        {
            var allElem = await _repositUser.ReadAll();
            var mapAllElem = allElem.Select(q => _mapper.Map<UserDto>(q)).ToList();
            return mapAllElem;
        }
        public async Task<UserDto?> ReadById(int id)
        {
            var element = await _repositUser.ReadById(id);
            if (element == null) return null;

            var mapElem = _mapper.Map<UserDto>(element);
            return mapElem;
        }
        public async Task<bool> Update(UserDto element)
        {
            var mapElem = _mapper.Map<User>(element);
            if ( mapElem == null) return false;
            return await _repositUser.Update(mapElem);
        }
    }
}
