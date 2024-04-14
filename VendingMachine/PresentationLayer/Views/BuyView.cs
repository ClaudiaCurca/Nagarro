using iQuest.VendingMachine.Interfaces;
using iQuest.VendingMachine.Payment;
using System;
using System.Collections.Generic;

namespace iQuest.VendingMachine.PresentationLayer.Views
{
    internal class BuyView : DisplayBase, IBuyView
    {
        public int RequestProduct()
        {
            DisplayLine("Select the product!", ConsoleColor.White);
            return int.Parse(Console.ReadLine());
        }

        public void DispenseProduct(string productName)
        {
            DisplayLine($"You have selected the product: {productName} ", ConsoleColor.White);
            DisplayLine("You can take the product! Thank you!", ConsoleColor.White);
        }

        public int? AskForPaymentMethod(List<PaymentMethod> paymentMethods)
        {
            DisplayLine($"Select your payment method:", ConsoleColor.White);

            foreach (PaymentMethod paymentMethod in paymentMethods)
            {
                Display($"Code: {paymentMethod.Id} " +
                        $"Payment method: {paymentMethod.Name} "
                        , ConsoleColor.Green);
                Console.WriteLine();
            }
            return int.Parse(Console.ReadLine());
        }
    }
}

