using iQuest.VendingMachine.Exceptions;
using iQuest.VendingMachine.Interfaces;
using System;

namespace iQuest.VendingMachine.Payment
{
    internal class CardPayment : IPaymentAlgorithm
    {
        private ICardPaymentTerminal cardPaymentTerminal;
        private int paymentMethod;

        public string Name => "card";

        public int PaymentMethod
        {
            get
            {
                return paymentMethod;
            }
            set 
            { 
                paymentMethod = value; 
            }
        }

        public CardPayment(ICardPaymentTerminal cardPaymentTerminal, int PaymentMethod)
        {
            this.cardPaymentTerminal = cardPaymentTerminal ?? throw new ArgumentNullException(nameof(cardPaymentTerminal));
            this.PaymentMethod = PaymentMethod;
        }

        public void Run(decimal price)
        {
            string cardNumber = cardPaymentTerminal.AskForCardNumber();
            if (IsCardNumberValid(cardNumber) == true)
            {
                cardPaymentTerminal.TransactionAccepted();
            }
            else
            {
                cardPaymentTerminal.TransactionRejected();
                throw new CancelException();
            }
        }
        private bool IsCardNumberValid(string cardNumber)
        {
            int nSum = 0;
            bool isSecond = false;
            for (int i = cardNumber.Length - 1; i >= 0; i--)
            {

                int d = cardNumber[i] - '0';

                if (isSecond == true)
                {
                    d = d * 2;
                }
                nSum += d / 10;
                nSum += d % 10;

                isSecond = !isSecond;
            }
            return (nSum % 10 == 0);
        }
    }
}
