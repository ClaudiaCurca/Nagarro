using iQuest.VendingMachine.Interfaces;
using System;

namespace iQuest.VendingMachine.PresentationLayer.Views
{
    internal class SupplyView : DisplayBase, ISupplyView
    {
        public void SupplyInformation()
        {
            DisplayLine("To update the products, select an already existing column", ConsoleColor.Red);
            DisplayLine("If one of the fields below is invalid, the order will be cancelled!", ConsoleColor.Red);
        }
        private string RequestColumnId()
        {
            DisplayLine("Add ColumnId", ConsoleColor.White);

            return Console.ReadLine();
        }
        private string RequestPrice()
        {
            DisplayLine("Add price", ConsoleColor.White);

            return Console.ReadLine();
        }
        private string RequestName()
        {
            DisplayLine("Add product name", ConsoleColor.White);

            return Console.ReadLine();
        }
        private string RequestQuantity()
        {
            DisplayLine("Add quantity", ConsoleColor.White);

            return Console.ReadLine();
        }
        public Product RequestProduct()
        {
            Product newProduct = new Product()
            {
                ColumnId = int.Parse(RequestColumnId()),
                Name = RequestName(),
                Price = decimal.Parse(RequestPrice()),
                Quantity = int.Parse(RequestQuantity())
            };
            return newProduct;
        }
    }
}
