using PT3_01.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace PT3_01.Service
{
    public class SqliteService : IDatabase
    {
        private SqliteConnection connection = new SqliteConnection("Data Source = data.db");

        public async void Delete(Guid id)
        {
            await connection.OpenAsync();

            var cmd = connection.CreateCommand();
            cmd.CommandText = 
                "DELETE FROM product WHERE id=@id";
            cmd.Parameters.AddWithValue("@id", id);

            await cmd.ExecuteNonQueryAsync();
            await connection.CloseAsync();
        }

        public async Task<List<Product>> GetAll()
        {
            var products = new List<Product>();
            await connection.OpenAsync();

            var cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT id, name, desc, image, price, colour FROM product";

            using(var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    var p = new Product
                    {
                        Id = reader.GetGuid(0),
                        Name = reader.GetString(1),
                        Desc = reader.GetString(2),
                        Image = reader.GetString(3),
                        Price = reader.GetDouble(4),
                        Colour = reader.GetString(5)
                    };

                    products.Add(p);
                }

            }
            await connection.CloseAsync();
            return products;
        }

        public async Task<Product> GetOne(Guid id)
        {
            var p = new Product
            {
                Id = Guid.Empty,
            };

            await connection.OpenAsync();

            var cmd = connection.CreateCommand();
            cmd.CommandText = @"SELECT id, name, desc, image, price, colour FROM product WHERE id=@id";
            cmd.Parameters.AddWithValue("@id", id);


            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    p = new Product
                    {
                        Id = reader.GetGuid(0),
                        Name = reader.GetString(1),
                        Desc = reader.GetString(2),
                        Image = reader.GetString(3),
                        Price = reader.GetDouble(4),
                        Colour = reader.GetString(5)
                    };
                }
            }
            await connection.CloseAsync();
            return p;
        }

        public void Initialise()
        {
            connection.Open();

            var cmd = connection.CreateCommand();
            cmd.CommandText = @"
                CREATE TABLE IF NOT EXISTS product
                (id text, name text, image text, price REAL, desc text, colour text, primary key(id))";
            cmd.ExecuteNonQuery();
            connection.Close();
        }

        public async void Insert(Product p)
        {
            await connection.OpenAsync();
            var cmd = connection.CreateCommand();
            cmd.CommandText = @"INSERT INTO product(id, name, image, price, desc, colour)
                            VALUES (@id, @name, @image, @price, @desc, @colour)";
            cmd.Parameters.AddWithValue("@id", p.Id);
            cmd.Parameters.AddWithValue("@name", p.Name);
            cmd.Parameters.AddWithValue("@image", p.Image);
            cmd.Parameters.AddWithValue("@price", p.Price);
            cmd.Parameters.AddWithValue("@desc", p.Desc);
            cmd.Parameters.AddWithValue("@colour", p.Colour);

            await cmd.ExecuteNonQueryAsync();
            await connection.CloseAsync();
        }

        public async void Update(Product p)
        {
            await connection.OpenAsync();
            var cmd = connection.CreateCommand();
            cmd.CommandText = @"UPDATE product SET name = @name, image = @image, price = @price, desc = @desc, colour = @colour 
                            WHERE id=@id";
            cmd.Parameters.AddWithValue("@id", p.Id);
            cmd.Parameters.AddWithValue("@name", p.Name);
            cmd.Parameters.AddWithValue("@image", p.Image);
            cmd.Parameters.AddWithValue("@price", p.Price);
            cmd.Parameters.AddWithValue("@desc", p.Desc);
            cmd.Parameters.AddWithValue("@colour", p.Colour);

            await cmd.ExecuteNonQueryAsync();
            await connection.CloseAsync();
        }

    }
}
