using Dapper;
using Domain.Entities;
using Npgsql;

namespace Infrastructure.Repositories
{
    public class PostgresRepositUser : IRepositUser
    {
        private readonly NpgsqlConnection _connection;
        public PostgresRepositUser(NpgsqlConnection connection)
        {
            _connection = connection;
        }
        public async Task<int> Create(User user)
        {
            int userId;
            try
            {
                await _connection.OpenAsync();

                userId = await _connection.QuerySingleAsync<int>(@"
                INSERT INTO users (name, pathicon)
                VALUES (@Name, @PathIcon)
                RETURNING id"
                , user);
            }
            finally
            {
                await _connection.CloseAsync();
            }

            return userId;
        }
        public async Task<bool> Delete(int id)
        {
            int affectedRows;
            try
            {
                await _connection.OpenAsync();

                affectedRows = await _connection.ExecuteAsync(@"
                DELETE FROM users WHERE id = @Id
                "
                , new { Id = id });
            }
            finally
            {
                await _connection.CloseAsync();
            }

            return affectedRows > 0;
        }
        public async Task<IEnumerable<User>> ReadAll()
        {
            IEnumerable<User> users;
            try
            {
                await _connection.OpenAsync();

                users = await _connection.QueryAsync<User>(@"
                SELECT id, name, pathicon FROM users
                "
                );
            }
            finally
            {
                await _connection.CloseAsync();
            }

            return users;
        }
        public async Task<User?> ReadById(int id)
        {
            User? user;
            try
            {
                await _connection.OpenAsync();

                user = await _connection.QueryFirstOrDefaultAsync<User>(@"
                SELECT id, name, pathicon FROM users
                WHERE id = @Id
                "
                , new { Id = id });
            }
            finally
            {
                await _connection.CloseAsync();
            }

            return user;
        }
        public async Task<bool> Update(User user)
        {
            int affectedRow;
            try
            {
                await _connection.OpenAsync();

                affectedRow = await _connection.ExecuteAsync(@"
                UPDATE users
                SET name = @Name,
                    pathicon =  @PathIcon
                WHERE id = @Id
                "
                , user);
            }
            finally
            {
                await _connection.CloseAsync();
            }

            return affectedRow > 0;
        }
    }
}
