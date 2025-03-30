using Dapper;
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
            (int, int) favoriteId;
            try
            {
                await _connection.OpenAsync();

                favoriteId = await _connection.QuerySingleAsync<(int, int)>(@"
                    INSERT INTO favorites (id_user, id_product)
                    VALUES (@IdUser, @IdProduct)
                    RETURNING iduser, idproduct"
                , element);
            }
            finally
            {
                await _connection.CloseAsync();
            }

            return favoriteId;
        }

        public async Task<bool> Delete(int idUser, int idProduct)
        {
            int affectedRows;
            try
            {
                await _connection.OpenAsync();

                affectedRows = await _connection.ExecuteAsync(@"
                    DELETE FROM favorites WHERE id_user = @IdUser and id_product = @IdProduct
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
                    SELECT id_user as IdUser, id_product as IdProduct FROM favorites
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
                    SELECT id_user as IdUser, id_product as IdProduct FROM favorites
                    WHERE iduser = @IdUser and idproduct = @IdProduct
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
