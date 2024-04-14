using System.Collections.Generic;

namespace iQuest.VendingMachine.Interfaces
{
    internal interface IShelfView
    {
        public void DisplayProducts(IEnumerable<Product> products);
    }
}
