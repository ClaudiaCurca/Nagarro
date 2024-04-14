using iQuest.VendingMachine.Interfaces;
using System;

namespace iQuest.VendingMachine.PresentationLayer
{
    internal class CardPaymentTerminal : DisplayBase, ICardPaymentTerminal
    {
        public string AskForCardNumber()
        {
            DisplayLine("Introduce the card number: ", ConsoleColor.Magenta);
            return Console.ReadLine();
        }

        public void TransactionAccepted()
        {
            DisplayLine("Transaction accepted", ConsoleColor.Green);
        }

        public void TransactionRejected()
        {
            DisplayLine("Declined transaction ", ConsoleColor.Red);
        }
    }
}
