using Application.Dto;
using Application.Exception;
using Application.Request;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Repositories;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace Application.Services
{
    public class ServiceProduct : IServiceProduct
    {
        private IRepositProduct _repositProduct;
        private IRepositFavorites _repositFavorites;
        private IRepositReview _repositReview;
        private IRepositAttachment _repositAttachment;
        private IMapper _mapper;
        private NpgsqlConnection _connection;
        private ILogger<ServiceProduct> _logger;
        public ServiceProduct(IRepositProduct repositProduct, IRepositFavorites repositFavorites, IRepositReview repositReview
            , IRepositAttachment repositAttachment, IMapper mapper, NpgsqlConnection connection, ILogger<ServiceProduct> logger)
        {
            _repositProduct = repositProduct;
            _repositFavorites = repositFavorites;
            _repositReview = repositReview;
            _repositAttachment = repositAttachment;
            _mapper = mapper;
            _connection = connection;
            _logger = logger;
        }

        public async Task<int?> Create(CreateProductRequest request)
        {
            var result = await _repositProduct.Create(
                new Product()
                {
                    IdSection = request.IdSection,
                    Length = request.Length,
                    Height = request.Height,
                    Width = request.Width,
                    Price = request.Price,
                    Discount = request.Discount,
                    PathPicture = request.PathPicture
                }
                );

            if (result == null) throw new EntityCreateException("Product is not created");

            _logger.LogInformation("Product created with id {ProductId}", result);
            return result;
        }
        public async Task<bool> Delete(int id)
        {
            await _connection.OpenAsync();

            await using (var tran = _connection.BeginTransaction())
            {
                try
                {
                    var favorites = _repositFavorites.ReadAll().Result.Where(x => x.IdProduct == id).ToList();
                    foreach (var v in favorites)
                    {
                        var resultFavourite = await _repositFavorites.Delete(v.IdUser, v.IdProduct);
                        if (!resultFavourite) throw new EntityDeleteException("Favorite is not deleted");
                    }

                    var reviews = _repositReview.ReadAll().Result.Where(x => x.IdProduct == id).ToList();
                    foreach (var v in reviews)
                    {
                        v.IdProduct = null;
                        var resultReview = await _repositReview.Update(v);
                        if (!resultReview) throw new EntityUpdateException("Review is not updated");
                    }

                    var attachments = _repositAttachment.ReadAll().Result.Where(x => x.IdProduct == id).ToList();
                    foreach (var v in attachments)
                    {
                        var resultAttach = await _repositAttachment.Delete(v.Id);
                        if (!resultAttach) throw new EntityDeleteException("Attachment is not deleted");
                    }

                    var result = await _repositProduct.Delete(id);
                    if (!result) throw new EntityDeleteException("Product is not deleted");

                    tran.Commit();

                    _logger.LogInformation("Deleted product with id {ProductId} and all connections to it", id);
                    return true;
                }
                catch (BaseApplicationException)
                {
                    tran.Rollback();
                    return false;
                }
                finally
                {
                    await _connection.CloseAsync();
                }
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
            if (element == null) throw new EntityNotFoundException("Product is not found");

            var mapElem = _mapper.Map<ProductDto>(element);
            return mapElem;
        }
        public async Task<bool> Update(UpdateProductRequest request)
        {
            var element = await _repositProduct.ReadById(request.Id);
            if (element == null) throw new EntityNotFoundException("Product is not found");

            element.IdSection = request.IdSection;
            element.Length = request.Length;
            element.Height = request.Height;
            element.Width = request.Width;
            element.Price = request.Price;
            element.Discount = request.Discount;
            element.PathPicture = request.PathPicture;

            var result = await _repositProduct.Update(element);

            if (!result) throw new EntityUpdateException("Product is not updated");

            _logger.LogInformation("Product updated with id {ProductId}", element.Id);
            return true;
        }
    }
}
