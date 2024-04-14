using System;
using System.Collections.Generic;
using System.Linq;
using iQuest.VendingMachine.Interfaces;

namespace iQuest.VendingMachine.PresentationLayer.Views
{
    internal class ShelfView : DisplayBase, IShelfView
    {
        public void DisplayProducts(IEnumerable<Product> products)
        {
            if (!products.Any())
            {
                Display("There are no products available!", ConsoleColor.Red);
                return;
            }
            DisplayLine("The Shelf contains the following items:", ConsoleColor.White);
            foreach (Product product in products)
            {
                Console.WriteLine();
                Display($"Product code: {product.ColumnId} " +
                        $"Product name: {product.Name} " +
                        $"Price: {product.Price} " +
                        $"Quantity: {product.Quantity} ", ConsoleColor.Green);

            }
        }
    }
}
