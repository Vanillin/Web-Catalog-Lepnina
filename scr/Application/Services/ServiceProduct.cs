﻿using Application.Dto;
using Application.Exception;
using Application.Request;
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

        public async Task<int?> Create(CreateProductRequest element)
        {
            var section = await _repositSection.ReadById(element.IdSection);
            if (section == null) throw new NotFoundApplicationException("Section is not found");

            return await _repositProduct.Create(
                new Product()
                {
                    IdSection = element.IdSection,
                    Length = element.Length,
                    Height = element.Height,
                    Width = element.Width,
                    Price = element.Price,
                    Discount = element.Discount,
                    PathPicture = element.PathPicture
                }
                );
        }
        public async Task<bool> Delete(int id) //must be transaction
        {
            ProductDto? element = await ReadById(id);
            if (element == null) throw new NotFoundApplicationException("Product is not found");
            List<Favorites> memoryFavor = new List<Favorites>();
            List<Review> memoryReviews = new List<Review>();
            List<Attachment> memoryAttach = new List<Attachment>();

            try
            {
                var favorites = _repositFavorites.ReadAll().Result.Where(x => x.IdProduct == id).ToList();
                foreach (var v in favorites)
                {
                    var resultFavourite = await _repositFavorites.Delete(v.IdUser, v.IdProduct);
                    if (!resultFavourite) throw new System.Exception();
                    memoryFavor.Add(v);
                }

                var reviews = _repositReview.ReadAll().Result.Where(x => x.IdProduct == id).ToList();
                foreach (var v in reviews)
                {
                    var resultReview = await _repositReview.Update(new Review() { Id = v.Id, IdProduct = null, IdUser = v.IdUser, Message = v.Message, PathPicture = v.PathPicture });
                    if (!resultReview) throw new System.Exception();
                    memoryReviews.Add(v);
                }

                var attachments = _repositAttachment.ReadAll().Result.Where(x => x.IdProduct == id).ToList();
                foreach (var v in attachments)
                {
                    var resultAttach = await _repositAttachment.Delete(v.Id);
                    if (!resultAttach) throw new System.Exception();
                    memoryAttach.Add(v);
                }

                var result = await _repositProduct.Delete(id);
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
                    await _repositReview.Update(new Review() { Id = v.Id, IdProduct = id, IdUser = v.IdUser, Message = v.Message, PathPicture = v.PathPicture });
                }
                foreach (var v in memoryAttach)
                {
                    await _repositAttachment.Create(v);
                }

                throw new TransactionApplicationException("Something gone wrong");
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
            if (element == null) throw new NotFoundApplicationException("Product is not found");

            var mapElem = _mapper.Map<ProductDto>(element);
            return mapElem;
        }
        public async Task<bool> Update(UpdateProductRequest element)
        {
            var section = await _repositSection.ReadById(element.IdSection);
            if (section == null) throw new NotFoundApplicationException("Section is not found");

            return await _repositProduct.Update(
                new Product()
                {
                    Id = element.Id,
                    IdSection = element.IdSection,
                    Length = element.Length,
                    Height = element.Height,
                    Width = element.Width,
                    Price = element.Price,
                    Discount = element.Discount,
                    PathPicture = element.PathPicture
                }
                );
        }
    }
}
