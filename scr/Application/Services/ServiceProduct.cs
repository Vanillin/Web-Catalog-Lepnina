using Application.Dto;
using Application.Exception;
using Application.Request;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Repositories;
using Microsoft.Data.SqlClient;
using Npgsql;

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
        private NpgsqlConnection _connection;
        public ServiceProduct(IRepositProduct repositProduct, IRepositFavorites repositFavorites, IRepositReview repositReview
            , IRepositAttachment repositAttachment, IRepositSection repositSection, IMapper mapper, NpgsqlConnection connection)
        {
            _repositProduct = repositProduct;
            _repositFavorites = repositFavorites;
            _repositReview = repositReview;
            _repositAttachment = repositAttachment;
            _repositSection = repositSection;
            _mapper = mapper;
            _connection = connection;
        }

        public async Task<int?> Create(CreateProductRequest request)
        {
            var section = await _repositSection.ReadById(request.IdSection);
            if (section == null) throw new EntityNotFoundException("Section is not found");

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

            if (result == null) throw new EntityCreateException("Product is not create");
            return result;
        }
        public async Task<bool> Delete(int id)
        {
            ProductDto? element = await ReadById(id);
            if (element == null) throw new EntityNotFoundException("Product is not found");

            using (_connection)
            {
                await _connection.OpenAsync();

                using (var tran = _connection.BeginTransaction())
                {
                    try
                    {
                        var favorites = _repositFavorites.ReadAll().Result.Where(x => x.IdProduct == id).ToList();
                        foreach (var v in favorites)
                        {
                            var resultFavourite = await _repositFavorites.Delete(v.IdUser, v.IdProduct);
                            if (!resultFavourite) throw new System.Exception();
                        }

                        var reviews = _repositReview.ReadAll().Result.Where(x => x.IdProduct == id).ToList();
                        foreach (var v in reviews)
                        {
                            var resultReview = await _repositReview.Update(new Review() { Id = v.Id, IdProduct = null, IdUser = v.IdUser, Message = v.Message, PathPicture = v.PathPicture });
                            if (!resultReview) throw new System.Exception();
                        }

                        var attachments = _repositAttachment.ReadAll().Result.Where(x => x.IdProduct == id).ToList();
                        foreach (var v in attachments)
                        {
                            var resultAttach = await _repositAttachment.Delete(v.Id);
                            if (!resultAttach) throw new System.Exception();
                        }

                        var result = await _repositProduct.Delete(id);
                        if (!result) throw new System.Exception();

                        tran.Commit();
                        return true;
                    }
                    catch (System.Exception)
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
            var section = await _repositSection.ReadById(request.IdSection);
            if (section == null) throw new EntityNotFoundException("Section is not found");

            var result = await _repositProduct.Update(
                new Product()
                {
                    Id = request.Id,
                    IdSection = request.IdSection,
                    Length = request.Length,
                    Height = request.Height,
                    Width = request.Width,
                    Price = request.Price,
                    Discount = request.Discount,
                    PathPicture = request.PathPicture
                }
                );

            if (!result) throw new EntityUpdateException("Product is not updated");
            return true;
        }
    }
}
