using Application.Dto;
using Application.Exception;
using Application.Request;
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

        public async Task<int?> Create(CreateUserRequest request)
        {
            var result = await _repositUser.Create(new User()
            {
                Name = request.Name,
                PathIcon = request.PathIcon,
            }
            );

            if (result == null) throw new EntityCreateException("User is not create");
            return result;
        }
        public async Task<bool> Delete(int id) //must be transaction
        {
            UserDto? element = await ReadById(id);
            if (element == null) throw new EntityNotFoundException("User is not found");
            List<Favorites> memoryFavor = new List<Favorites>();
            List<Review> memoryReviews = new List<Review>();

            try
            {
                var favorites = _repositFavorites.ReadAll().Result.Where(x => x.IdUser == id).ToList();
                foreach (var v in favorites)
                {
                    var resultFavourite = await _repositFavorites.Delete(v.IdUser, v.IdProduct);
                    if (!resultFavourite) throw new System.Exception();
                    memoryFavor.Add(v);
                }

                var reviews = _repositReview.ReadAll().Result.Where(x => x.IdUser == id).ToList();
                foreach (var v in reviews)
                {
                    var resultReview = await _repositReview.Delete(v.Id);
                    if (!resultReview) throw new System.Exception();
                    memoryReviews.Add(v);
                }

                var result = await _repositUser.Delete(id);
                if (!result) throw new System.Exception();

                return true;
            }
            catch (System.Exception)
            {
                foreach (var v in memoryFavor)
                {
                    await _repositFavorites.Create(v);
                }
                foreach (var v in memoryReviews)
                {
                    await _repositReview.Create(v);
                }

                throw new EntityDeleteException("Something gone wrong");
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
            if (element == null) throw new EntityNotFoundException("User is not found");

            var mapElem = _mapper.Map<UserDto>(element);
            return mapElem;
        }
        public async Task<bool> Update(UpdateUserRequest request)
        {
            var result = await _repositUser.Update(new User()
            {
                Id = request.Id,
                Name = request.Name,
                PathIcon = request.PathIcon,
            }
            );

            if (!result) throw new EntityUpdateException("User is not updated");
            return true;
        }
    }
}
