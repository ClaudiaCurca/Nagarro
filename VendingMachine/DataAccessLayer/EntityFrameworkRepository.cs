using iQuest.VendingMachine.Interfaces;
using LiteDB;
using System.Collections.Generic;
using System.Linq;

namespace iQuest.VendingMachine.DataAccessLayer
{
    internal class EntityFrameworkRepository : IProductRepository
    {
        private VendingDbContext db;

        public EntityFrameworkRepository(VendingDbContext db)
        {
            this.db = db;
        }
        public Product Add(Product product)
        {
            db.Products.Add(product);
            return product;
        }

        public bool Delete(Product product)
        {
            var itemToRemove = db.Products.SingleOrDefault(x => x.ColumnId == product.ColumnId); 

            if (itemToRemove != null)
            {
                db.Products.Remove(itemToRemove);
                return true;
            }
            return false;
        }

        public List<Product> GetAll()
        {
            if (db.Products.Count() == 0) { return new List<Product>(); }
            else
            {
                return db.Products.ToList();
            }
        }

        public Product GetByColumn(int columnid)
        {
            return db.Products.Where(x=>x.ColumnId == columnid).FirstOrDefault();
        }

        public Product Update(Product product)
        {
            var result = db.Products.SingleOrDefault(b => b.ColumnId == product.ColumnId);
            if (result != null)
            {
                result.Name =product.Name;
                result.Price =product.Price;
                result.Quantity=product.Quantity;
       
            }
            return result;
        }
    }
}
