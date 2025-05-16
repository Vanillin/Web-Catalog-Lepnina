using Dapper;
using Domain.Entities;
using Npgsql;

namespace Infrastructure.Repositories
{
    public class PostgresRepositPictureFile : IRepositPictureFile
    {
        private readonly NpgsqlConnection _connection;
        public PostgresRepositPictureFile(NpgsqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<bool> Delete(int id)
        {
            int affectedRows;
            try
            {
                await _connection.OpenAsync();

                affectedRows = await _connection.ExecuteAsync(@"
                DELETE FROM picture_file WHERE id = @Id
                "
                , new { Id = id });
            }
            finally
            {
                await _connection.CloseAsync();
            }

            return affectedRows > 0;
        }

        public async Task<PictureFile?> Get(int id)
        {
            PictureFile? pictureFile;
            try
            {
                await _connection.OpenAsync();

                pictureFile = await _connection.QueryFirstOrDefaultAsync<PictureFile>(@"
                SELECT file_name as FileName, stored_path as StoredPath, content_type as ContentType, size FROM picture_file
                WHERE id = @Id
                "
                , new { Id = id });
            }
            finally
            {
                await _connection.CloseAsync();
            }

            return pictureFile;
        }

        public async Task<int?> Create(PictureFile file)
        {
            int? pictureFileId = null;
            try
            {
                await _connection.OpenAsync();

                pictureFileId = await _connection.QuerySingleAsync<int>(@"
                INSERT INTO picture_file (file_name, stored_path, content_type, size)
                VALUES (@FileName, @StoredPath, @ContentType, @Size)
                RETURNING id"
                , file);
            }
            finally
            {
                await _connection.CloseAsync();
            }

            return pictureFileId;
        }
    }
}
