using iQuest.VendingMachine.Interfaces;
using System.Collections.Generic;

namespace iQuest.VendingMachine.DataAccessLayer
{
    internal class InMemoryRepository : IProductRepository
    {
        private readonly ICollection<Product> Products = new List<Product>
        {
            new Product
            {
                Name = "Chocolate",
                Price = 9,
                Quantity = 20,
                ColumnId = 1
            },
            new Product
            {
                Name = "Chips",
                Price = 5,
                Quantity = 7,
                ColumnId = 2
            },
            new Product
            {
                Name = "Still Water",
                Price = 2,
                Quantity = 10,
                ColumnId = 3
            },
            new Product
            {
                Name = "Tea",
                Price = 2,
                Quantity = 0,
                ColumnId = 4
            },
            new Product
            {
                Name = "Pepsi",
                Price = 2,
                Quantity = 1,
                ColumnId = 5
            }
        };

        public List<Product> GetAll()
        {
            List<Product> list = new List<Product>();

            foreach (Product product in Products)
            {
                list.Add(product);
            }
            return list;
        }

        public Product GetByColumn(int columnid)
        {
            foreach (Product product in Products)
            {
                if (product.ColumnId == columnid)
                {
                    return product;
                }
            }
            return null;
        }

        public Product Add(Product newProduct)
        {
            Products.Add(newProduct);

            return newProduct;
        }

        public Product Update(Product newProduct)
        {
            foreach(Product product in Products)
            {
                if (product.ColumnId == newProduct.ColumnId)
                {
                    product.Name = newProduct.Name;
                    product.Price = newProduct.Price;
                    product.Quantity = newProduct.Quantity;
                    return newProduct;
                } 
            }
            return null;
        }

        public bool Delete(Product product)
        {
            foreach(Product products in Products)
            {
                if (products.ColumnId == product.ColumnId)
                {
                    return Products.Remove(products);
                }
            }
            return false;
        }
    }
}
