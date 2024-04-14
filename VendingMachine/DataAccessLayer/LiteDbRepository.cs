using iQuest.VendingMachine.Interfaces;
using LiteDB;
using System.Collections.Generic;
using System.Linq;

namespace iQuest.VendingMachine.DataAccessLayer
{
    internal class LiteDbRepository : IProductRepository
    {
        private readonly LiteDatabase db;
        ILiteCollection<Product> col;

        public LiteDbRepository(string path)
        {
            db = new LiteDatabase(path);
            col = db.GetCollection<Product>("products");
        }

        public List<Product> GetAll()
        {
            return col.Query().ToList();
        }

        public Product GetByColumn(int columnid)
        {
            Product product = col.Find(x => x.ColumnId == columnid).FirstOrDefault();
            return product;
        }

        public Product Add(Product product)
        {
            col.Insert(product);
            return product;
        }

        public Product Update(Product newProduct)
        {
            Product product = col.Find(x => x.ColumnId == newProduct.ColumnId).FirstOrDefault();
            if (product != null)
            {
                product.Price = newProduct.Price;
                product.Quantity = newProduct.Quantity;
                product.Name = newProduct.Name;

                col.Update(product);
            }
            return newProduct;
        }

        public bool Delete(Product product)
        {
            Product prod = col.Find(x => x.ColumnId == product.ColumnId).FirstOrDefault();
            if (prod != null)
            {
                BsonValue p = new BsonValue(prod.Id);
                return col.Delete(p);
            }
            return false;
        }
    }
}
