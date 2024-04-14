using System.Collections.Generic;

namespace iQuest.VendingMachine.Interfaces
{
    internal interface IProductRepository
    {
        public List<Product> GetAll();

        public Product GetByColumn(int columnid);

        public Product Add(Product product);

        public Product Update(Product product);

        public bool Delete(Product product);
    }
}