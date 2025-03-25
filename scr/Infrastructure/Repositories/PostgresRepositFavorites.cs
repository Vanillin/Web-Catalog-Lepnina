﻿using Dapper;
using Domain.Entities;
using Npgsql;

namespace Infrastructure.Repositories
{
    public class PostgresRepositFavorites : IRepositFavorites
    {
        private readonly NpgsqlConnection _connection;
        public PostgresRepositFavorites(NpgsqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<(int, int)> Create(Favorites element)
        {
            (int, int) favoriteid;
            try
            {
                await _connection.OpenAsync();

                favoriteid = await _connection.QuerySingleAsync<(int, int)>(@"
                    INSERT INTO favorites (iduser, idproduct)
                    VALUES (@IdUser, @IdProduct)
                    RETURNING iduser, idproduct"
                , new { element.IdUser, element.IdProduct });
            }
            finally
            {
                await _connection.CloseAsync();
            }

            return favoriteid;
        }

        public async Task<bool> Delete(int idUser, int idProduct)
        {
            int affectedRows;
            try
            {
                await _connection.OpenAsync();

                affectedRows = await _connection.ExecuteAsync(@"
                    DELETE FROM favorites WHERE @IdUser = iduser and @IdProduct = idproduct
                    "
                , new { idUser, idProduct });
            }
            finally
            {
                await _connection.CloseAsync();
            }

            return affectedRows > 0;
        }

        public async Task<IEnumerable<Favorites>> ReadAll()
        {
            IEnumerable<Favorites> favorites;
            try
            {
                await _connection.OpenAsync();

                favorites = await _connection.QueryAsync<Favorites>(@"
                    SELECT iduser, idproduct FROM favorites
                    "
                );
            }
            finally
            {
                await _connection.CloseAsync();
            }

            return favorites;
        }

        public async Task<Favorites?> ReadById(int idUser, int idProduct)
        {
            Favorites? favorite;
            try
            {
                await _connection.OpenAsync();

                favorite = await _connection.QueryFirstOrDefaultAsync<Favorites>(@"
                    SELECT iduser, idproduct FROM favorites
                    WHERE @IdUser = iduser and @IdProduct = idproduct
                    "
                , new { idUser, idProduct });
            }
            finally
            {
                await _connection.CloseAsync();
            }

            return favorite;
        }
    }
}
