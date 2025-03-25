using Dapper;
using Domain.Entities;
using Npgsql;

namespace Infrastructure.Repositories
{
    public class PostgresRepositProduct : IRepositProduct
    {
        private readonly NpgsqlConnection _connection;
        public PostgresRepositProduct(NpgsqlConnection connection)
        {
            _connection = connection;
        }
        public async Task<int> Create(Product product) //column "price" does not exist ?????????   column "priсe" of relation "products" does not exist ?????????
        {
            int productid;
            try
            {
                await _connection.OpenAsync();

                productid = await _connection.QuerySingleAsync<int>(@"
                INSERT INTO products (length, height, width, priсe, discount, pathpicture, idsection)
                VALUES (@Length, @Height, @Width, @Priсe, @Discount, @PathPicture, @IdSection)
                RETURNING id"
                , product /*new { product.Length, product.Height, product.Width, product.Priсe, product.Discount, product.PathPicture, product.IdSection }*/);
            }
            finally
            {
                await _connection.CloseAsync();
            }

            return productid;
        }
        public async Task<bool> Delete(int id)
        {
            int affectedRows;
            try
            {
                await _connection.OpenAsync();

                affectedRows = await _connection.ExecuteAsync(@"
                DELETE FROM products WHERE @Id = id
                "
                , new { Id = id });
            }
            finally
            {
                await _connection.CloseAsync();
            }

            return affectedRows > 0;
        }
        public async Task<IEnumerable<Product>> ReadAll()
        {
            IEnumerable<Product> products;
            try
            {
                await _connection.OpenAsync();

                products = await _connection.QueryAsync<Product>(@"
                SELECT id, length, height, width, price, discount, pathpicture, idsection FROM products
                "
                );
            }
            finally
            {
                await _connection.CloseAsync();
            }

            return products;
        }
        public async Task<Product?> ReadById(int id)
        {
            Product? product;
            try
            {
                await _connection.OpenAsync();

                product = await _connection.QueryFirstOrDefaultAsync<Product>(@"
                SELECT id, length, height, width, price, discount, pathpicture, idsection FROM products
                WHERE id = @Id
                "
                , new { Id = id });
            }
            finally
            {
                await _connection.CloseAsync();
            }

            return product;
        }
        public async Task<bool> Update(Product product)
        {
            int affectedRow;
            try
            {
                await _connection.OpenAsync();

                affectedRow = await _connection.ExecuteAsync(@"
                UPDATE products
                SET
                    length = @Length,
                    height = @Height,
                    width = @Width,
                    price = @Price,
                    discount = @Discount,
                    pathpicture = @PathPicture,
                    idsection = @IdSection
                WHERE id = @Id
                "
                , product);
            }
            finally
            {
                await _connection.CloseAsync();
            }

            return affectedRow > 0;
        }
    }
}
