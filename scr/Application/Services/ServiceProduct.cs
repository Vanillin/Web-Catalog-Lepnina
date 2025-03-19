using Application.Dto;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Repositories;

namespace Application.Services
{
    public class ServiceProduct : IServiceProduct
    {
        private IRepositProduct _repositProduct;
        private IRepositFavorites _repositFavorites;
        private IRepositReview _repositReview;
        private IRepositAttachment _repositAttachment;
        private IRepositSection _repositSection;
        private IMapper _mapper;
        public ServiceProduct(IRepositProduct repositProduct, IRepositFavorites repositFavorites, IRepositReview repositReview
            , IRepositAttachment repositAttachment, IRepositSection repositSection, IMapper mapper)
        {
            _repositProduct = repositProduct;
            _repositFavorites = repositFavorites;
            _repositReview = repositReview;
            _repositAttachment = repositAttachment;
            _repositSection = repositSection;
            _mapper = mapper;
        }

        public async Task<int?> Create(ProductDto element)
        {
            var section = _repositSection.ReadById(element.IdSection);
            if (section == null) return null;

            var mapElem = _mapper.Map<Product>(element);
            if (mapElem == null) return null;
            return await _repositProduct.Create(mapElem); //id is changed later
        }
        public async Task<bool> Delete(int id) //must be transaction
        {
            ProductDto? element = ReadById(id).Result;
            if (element == null) return false;
            List<Favorites> memoryFavor = new List<Favorites>();
            List<Review> memoryReviews = new List<Review>();
            List<Attachment> memoryAttach = new List<Attachment>();

            try
            {
                var favorites = _repositFavorites.ReadAll().Result.Where(x => x.IdProduct == id).ToList();
                foreach (var v in favorites)
                {
                    var resultFavourite = _repositFavorites.Delete(v.IdUser, v.IdProduct);
                    if (resultFavourite == null) throw new Exception();
                    memoryFavor.Add(v);
                }

                var reviews = _repositReview.ReadAll().Result.Where(x => x.IdProduct == id).ToList();
                foreach (var v in reviews)
                {
                    var resultReview = _repositReview.Update(new Review() { Id = v.Id, IdProduct = null, IdUser = v.IdUser, Message = v.Message, PathPicture = v.PathPicture }).Result;
                    if (!resultReview) throw new Exception();
                    memoryReviews.Add(v);
                }

                var attachments = _repositAttachment.ReadAll().Result.Where(x => x.IdProduct == id).ToList();
                foreach (var v in attachments)
                {
                    var resultAttach = _repositAttachment.Delete(v.Id);
                    if (resultAttach == null) throw new Exception();
                    memoryAttach.Add(v);
                }

                var result = _repositProduct.Delete(id).Result;
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
                    await _repositReview.Update(new Review() { Id = v.Id, IdProduct = id, IdUser = v.IdUser, Message = v.Message, PathPicture = v.PathPicture });
                }
                foreach (var v in memoryAttach)
                {
                    await _repositAttachment.Create(v);
                }

                return false;
            }
        }
        public async Task<IEnumerable<ProductDto>> ReadAll()
        {
            var allElem = await _repositProduct.ReadAll();
            var mapAllElem = allElem.Select(q => _mapper.Map<ProductDto>(q)).ToList();
            return mapAllElem;
        }
        public async Task<ProductDto?> ReadById(int id)
        {
            var element = await _repositProduct.ReadById(id);
            if (element == null) return null;

            var mapElem = _mapper.Map<ProductDto>(element);
            return mapElem;
        }
        public async Task<bool> Update(ProductDto element)
        {
            var mapElem = _mapper.Map<Product>(element);
            if (mapElem == null) return false;

            var section = _repositSection.ReadById(mapElem.IdSection);
            if (section == null) return false;

            return await _repositProduct.Update(mapElem);
        }
    }
}
