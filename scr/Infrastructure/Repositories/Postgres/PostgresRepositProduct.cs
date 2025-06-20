﻿using Dapper;
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
        public async Task<int?> Create(Product product)
        {
            int? productId = null;
            try
            {
                await _connection.OpenAsync();

                productId = await _connection.QuerySingleAsync<int>(@"
                INSERT INTO products (length, height, width, price, discount, id_picture, id_section)
                VALUES (@Length, @Height, @Width, @Price, @Discount, @IdPicture, @IdSection)
                RETURNING id"
                , product);
            }
            finally
            {
                await _connection.CloseAsync();
            }

            return productId;
        }
        public async Task<bool> Delete(int id)
        {
            int affectedRows;
            try
            {
                await _connection.OpenAsync();

                affectedRows = await _connection.ExecuteAsync(@"
                DELETE FROM products WHERE id = @Id
                "
                , new { Id = id });
            }
            finally
            {
                await _connection.CloseAsync();
            }

            return affectedRows > 0;
        }

        public async Task<IEnumerable<Product>> GetBySection(int idSection)
        {
            IEnumerable<Product> products;
            try
            {
                await _connection.OpenAsync();

                products = await _connection.QueryAsync<Product>(@"
                SELECT id, length, height, width, price, discount, id_picture as IdPicture, id_section as IdSection FROM products
                WHERE id_section = @IdSection
                "
                , new { IdSection = idSection });
            }
            finally
            {
                await _connection.CloseAsync();
            }

            return products;
        }

        public async Task<IEnumerable<Product>> ReadAll()
        {
            IEnumerable<Product> products;
            try
            {
                await _connection.OpenAsync();

                products = await _connection.QueryAsync<Product>(@"
                SELECT id, length, height, width, price, discount, id_picture as IdPicture, id_section as IdSection FROM products
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
                SELECT id, length, height, width, price, discount, id_picture as IdPicture, id_section as IdSection FROM products
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
                    id_picture = @IdPicture,
                    id_section = @IdSection
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
