using Dapper;
using Domain.Entities;
using Infrastructure.DataBase.TypeMappings;
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
        public async Task<int?> Create(User user)
        {
            int? userId = null;
            try
            {
                await _connection.OpenAsync();

                userId = await _connection.QuerySingleAsync<int>(@"
                INSERT INTO users (name, path_icon, email, password_hash, role)
                VALUES (@Name, @PathIcon, @Email, @PasswordHash, @Role::user_role)
                RETURNING id"
                , user.AsDapperParams());
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
                SELECT id, name, path_icon as PathIcon, email, password_hash as PasswordHash, role FROM users
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
                SELECT id, name, path_icon as PathIcon, email, password_hash as PasswordHash, role FROM users
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
        public async Task<User?> ReadByEmail(string email)
        {
            User? user;
            try
            {
                await _connection.OpenAsync();

                user = await _connection.QueryFirstOrDefaultAsync<User>(@"
                SELECT * FROM users WHERE email = @Email
                "
                , new { Email = email });
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
                    path_icon =  @PathIcon,
                    email = @Email,
                    password_hash = @PasswordHash,
                    role = @Role::user_role
                WHERE id = @Id
                "
                , user.AsDapperParams());
            }
            finally
            {
                await _connection.CloseAsync();
            }

            return affectedRow > 0;
        }
    }
}
