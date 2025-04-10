﻿using Application.Dto;
using Application.Exception;
using Application.Request;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Repositories;
using Npgsql;

namespace Application.Services
{
    public class ServiceUser : IServiceUser
    {
        private IRepositUser _repositUser;
        private IRepositReview _repositReview;
        private IRepositFavorites _repositFavorites;
        private IMapper _mapper;
        private NpgsqlConnection _connection;
        public ServiceUser(IRepositUser repositUser, IRepositReview repositReview, IRepositFavorites repositFavorites, IMapper mapper, NpgsqlConnection connection)
        {
            _repositUser = repositUser;
            _repositReview = repositReview;
            _repositFavorites = repositFavorites;
            _mapper = mapper;
            _connection = connection;
        }

        public async Task<int?> Create(CreateUserRequest request)
        {
            var result = await _repositUser.Create(new User()
            {
                Name = request.Name,
                PathIcon = request.PathIcon,
            }
            );

            if (result == null) throw new EntityCreateException("User is not created");
            return result;
        }
        public async Task<bool> Delete(int id)
        {
            await _connection.OpenAsync();

            await using (var tran = _connection.BeginTransaction())
            {
                try
                {
                    var favorites = _repositFavorites.ReadAll().Result.Where(x => x.IdUser == id).ToList();
                    foreach (var v in favorites)
                    {
                        var resultFavourite = await _repositFavorites.Delete(v.IdUser, v.IdProduct);
                        if (!resultFavourite) throw new EntityDeleteException("Favorite is not deleted");
                    }

                    var reviews = _repositReview.ReadAll().Result.Where(x => x.IdUser == id).ToList();
                    foreach (var v in reviews)
                    {
                        var resultReview = await _repositReview.Delete(v.Id);
                        if (!resultReview) throw new EntityDeleteException("Review is not deleted");
                    }

                    var result = await _repositUser.Delete(id);
                    if (!result) throw new EntityDeleteException("User is not deleted");

                    tran.Commit();
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
            var element = await _repositUser.ReadById(request.Id);
            if (element == null) throw new EntityNotFoundException("User is not found");

            element.Name = request.Name;
            element.PathIcon = request.PathIcon;

            var result = await _repositUser.Update(element);

            if (!result) throw new EntityUpdateException("User is not updated");
            return true;
        }
    }
}
