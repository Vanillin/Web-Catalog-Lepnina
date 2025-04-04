using Dapper;
using Domain.Entities;
using Npgsql;

namespace Infrastructure.Repositories
{
    public class PostgresRepositSection : IRepositSection
    {
        private readonly NpgsqlConnection _connection;
        public PostgresRepositSection(NpgsqlConnection connection)
        {
            _connection = connection;
        }
        public async Task<int?> Create(Section section)
        {
            int? sectionId = null;
            try
            {
                await _connection.OpenAsync();

                sectionId = await _connection.QuerySingleAsync<int>(@"
                INSERT INTO sections (name)
                VALUES (@Name)
                RETURNING id"
                , section);
            }
            finally
            {
                await _connection.CloseAsync();
            }

            return sectionId;
        }
        public async Task<bool> Delete(int id)
        {
            int affectedRows;
            try
            {
                await _connection.OpenAsync();

                affectedRows = await _connection.ExecuteAsync(@"
                DELETE FROM sections WHERE id = @Id
                "
                , new { Id = id });
            }
            finally
            {
                await _connection.CloseAsync();
            }

            return affectedRows > 0;
        }
        public async Task<IEnumerable<Section>> ReadAll()
        {
            IEnumerable<Section> sections;
            try
            {
                await _connection.OpenAsync();

                sections = await _connection.QueryAsync<Section>(@"
                SELECT id, name FROM sections
                "
                );
            }
            finally
            {
                await _connection.CloseAsync();
            }

            return sections;
        }
        public async Task<Section?> ReadById(int id)
        {
            Section? section;
            try
            {
                await _connection.OpenAsync();

                section = await _connection.QueryFirstOrDefaultAsync<Section>(@"
                SELECT id, name FROM sections
                WHERE id = @Id
                "
                , new { Id = id });
            }
            finally
            {
                await _connection.CloseAsync();
            }

            return section;
        }
        public async Task<bool> Update(Section section) 
        {
            int affectedRow;
            try
            {
                await _connection.OpenAsync();

                affectedRow = await _connection.ExecuteAsync(@"
                UPDATE sections
                SET 
                    name = @Name
                WHERE id = @Id
                "
                , section);
            }
            finally
            {
                await _connection.CloseAsync();
            }

            return affectedRow > 0;
        }
    }
}
