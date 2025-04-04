using Dapper;
using Domain.Entities;
using Npgsql;

namespace Infrastructure.Repositories
{
    public class PostgresRepositReview : IRepositReview
    {
        private readonly NpgsqlConnection _connection;
        public PostgresRepositReview(NpgsqlConnection connection)
        {
            _connection = connection;
        }
        public async Task<int?> Create(Review review)
        {
            int? reviewId = null;
            try
            {
                await _connection.OpenAsync();

                reviewId = await _connection.QuerySingleAsync<int>(@"
                INSERT INTO reviews (message, path_picture, id_user, id_product)
                VALUES (@Message, @PathPicture, @IdUser, @IdProduct)
                RETURNING id"
                , review);
            }
            finally
            {
                await _connection.CloseAsync();
            }

            return reviewId;
        }
        public async Task<bool> Delete(int id)
        {
            int affectedRows;
            try
            {
                await _connection.OpenAsync();

                affectedRows = await _connection.ExecuteAsync(@"
                DELETE FROM reviews WHERE id = @Id
                "
                , new { Id = id });
            }
            finally
            {
                await _connection.CloseAsync();
            }

            return affectedRows > 0;
        }
        public async Task<IEnumerable<Review>> ReadAll()
        {
            IEnumerable<Review> reviews;
            try
            {
                await _connection.OpenAsync();

                reviews = await _connection.QueryAsync<Review>(@"
                SELECT id, message, path_picture as PathPicture, id_user as IsUser, id_product as IdProduct FROM reviews
                "
                );
            }
            finally
            {
                await _connection.CloseAsync();
            }

            return reviews;
        }
        public async Task<Review?> ReadById(int id)
        {
            Review? review;
            try
            {
                await _connection.OpenAsync();

                review = await _connection.QueryFirstOrDefaultAsync<Review>(@"
                SELECT id, message, path_picture as PathPicture, id_user as IsUser, id_product as IdProduct FROM reviews
                WHERE id = @Id
                "
                , new { Id = id });
            }
            finally
            {
                await _connection.CloseAsync();
            }

            return review;
        }
        public async Task<bool> Update(Review review)
        {
            int affectedRow;
            try
            {
                await _connection.OpenAsync();

                affectedRow = await _connection.ExecuteAsync(@"
                UPDATE reviews
                SET 
                    message = @Message,
                    path_picture =  @PathPicture,
                    id_user = @IdUser,
                    id_product = @IdProduct
                WHERE id = @Id
                "
                , review);
            }
            finally
            {
                await _connection.CloseAsync();
            }

            return affectedRow > 0;
        }
    }
}
