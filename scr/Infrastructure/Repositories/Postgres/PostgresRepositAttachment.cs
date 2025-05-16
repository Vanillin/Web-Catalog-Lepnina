using Dapper;
using Domain.Entities;
using Npgsql;

namespace Infrastructure.Repositories
{
    public class PostgresRepositAttachment : IRepositAttachment
    {
        private readonly NpgsqlConnection _connection;
        public PostgresRepositAttachment(NpgsqlConnection connection)
        {
            _connection = connection;
        }
        public async Task<int?> Create(Attachment attachment)
        {
            int? attachmentId = null;
            try
            {
                await _connection.OpenAsync();

                attachmentId = await _connection.QuerySingleAsync<int>(@"
                INSERT INTO attachments (message, id_picture, id_product)
                VALUES (@Message, @IdPicture, @IdProduct)
                RETURNING id"
                , attachment);
            }
            finally
            {
                await _connection.CloseAsync();
            }

            return attachmentId;
        }
        public async Task<bool> Delete(int id)
        {
            int affectedRows;
            try
            {
                await _connection.OpenAsync();

                affectedRows = await _connection.ExecuteAsync(@"
                DELETE FROM attachments WHERE id = @Id
                "
                , new { Id = id });
            }
            finally
            {
                await _connection.CloseAsync();
            }

            return affectedRows > 0;
        }
        public async Task<IEnumerable<Attachment>> ReadAll()
        {
            IEnumerable<Attachment> attachments;
            try
            {
                await _connection.OpenAsync();

                attachments = await _connection.QueryAsync<Attachment>(@"
                SELECT id, message, id_picture as IdPicture, id_product as IdProduct FROM attachments
                "
                );
            }
            finally
            {
                await _connection.CloseAsync();
            }

            return attachments;
        }
        public async Task<Attachment?> ReadById(int id)
        {
            Attachment? attachment;
            try
            {
                await _connection.OpenAsync();

                attachment = await _connection.QueryFirstOrDefaultAsync<Attachment>(@"
                SELECT id, message, id_picture as IdPicture, id_product as IdProduct FROM attachments
                WHERE id = @Id
                "
                , new { Id = id });
            }
            finally
            {
                await _connection.CloseAsync();
            }

            return attachment;
        }
        public async Task<bool> Update(Attachment attachment)
        {
            int affectedRow;
            try
            {
                await _connection.OpenAsync();

                affectedRow = await _connection.ExecuteAsync(@"
                UPDATE attachments
                SET message = @Message,
                    id_picture =  @IdPicture,
                    id_product = @IdProduct
                WHERE id = @Id
                "
                , attachment);
            }
            finally
            {
                await _connection.CloseAsync();
            }

            return affectedRow > 0;
        }
    }
}
