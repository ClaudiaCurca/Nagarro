using iQuest.VendingMachine.Interfaces;
using System;

namespace iQuest.VendingMachine.PresentationLayer
{
    internal class CashPaymentTerminal : DisplayBase, ICashPaymentTerminal
    {
        public decimal AskForMoney()
        {
            DisplayLine("Enter the money ", ConsoleColor.White);

            return Convert(Console.ReadLine());
        }

        public void GiveBackChange(decimal change)
        {
            DisplayLine($"The change is: {change}", ConsoleColor.Magenta);
        }

        public void DisplayTotal(decimal money)
        {
            DisplayLine("Total:" + money, ConsoleColor.White);
        }

        private decimal Convert(string value)
        {
            return decimal.Parse(value);
        }
    }
}
