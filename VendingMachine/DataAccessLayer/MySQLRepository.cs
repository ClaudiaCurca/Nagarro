using iQuest.VendingMachine.Interfaces;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace iQuest.VendingMachine.DataAccessLayer
{
    internal class MySQLRepository : IProductRepository
    {
        private readonly MySqlConnection db;
        private readonly MySqlDataAdapter adapter = new MySqlDataAdapter();

        public MySQLRepository(string connectionString)
        {
            db = new MySqlConnection(connectionString);
        }

        public bool Delete(Product product)
        {
            using (db)
            {
                db.Open();
                MySqlCommand cmd = new MySqlCommand("delete from product where idProduct=@id", db);
                cmd.Parameters.AddWithValue("@id", product.ColumnId);

                adapter.InsertCommand = cmd;
                int numberRowsDeleted = adapter.InsertCommand.ExecuteNonQuery();
                return numberRowsDeleted > 0;
            }
        }

        public List<Product> GetAll()
        {
            using (db)
            {
                db.Open();
                List<Product> products = new List<Product>();
                MySqlCommand cmd = new MySqlCommand("SELECT idProduct, Name, Price, Quantity FROM product", db);
                MySqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    products.Add(new Product
                    {
                        ColumnId = Convert.ToInt32(sdr["idProduct"]),
                        Name = sdr["Name"].ToString(),
                        Price = Convert.ToDecimal(sdr["Price"]),
                        Quantity = Convert.ToInt32(sdr["Quantity"])
                    });
                }
                return products;
            }
        }

        public Product GetByColumn(int columnid)
        {
            using (db)
            {
                Product product = new Product();

                MySqlCommand cmd = new MySqlCommand("select idProduct, Name, Price, Quantity from product where idProduct=@id", db);
                cmd.Parameters.AddWithValue("@id", columnid);
                MySqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    product.ColumnId = Convert.ToInt32(sdr["idProduct"]);
                    product.Name = sdr["Name"].ToString();
                    product.Price = Convert.ToDecimal(sdr["Price"]);
                    product.Quantity = Convert.ToInt32(sdr["Quantity"]);

                    return product;
                }
                return null;
            }
        }

        public Product Add(Product product)
        {
            using (db)
            {
                MySqlCommand cmd = new MySqlCommand("insert into product (idProduct, Name, Price, Quantity) values (@columnId, @name, @price, @quantity)", db);
                cmd.Parameters.AddWithValue("@columnId", product.ColumnId);
                cmd.Parameters.AddWithValue("@name", product.Name);
                cmd.Parameters.AddWithValue("@price", product.Price);
                cmd.Parameters.AddWithValue("@quantity", product.Quantity);

                adapter.InsertCommand = cmd;
                adapter.InsertCommand.ExecuteNonQuery();
                return product;
            }
        }

        public Product Update(Product product)
        {
            using (db)
            {
                MySqlCommand cmd = new MySqlCommand("update product set Name=@name, Price=@price, Quantity=@quantity where idProduct=@id", db);

                cmd.Parameters.AddWithValue("@id", product.ColumnId);
                cmd.Parameters.AddWithValue("@name", product.Name);
                cmd.Parameters.AddWithValue("@price", product.Price);
                cmd.Parameters.AddWithValue("@quantity", product.Quantity);

                adapter.InsertCommand = cmd;
                adapter.InsertCommand.ExecuteNonQuery();
                return product;
            }
        }
    }
}
