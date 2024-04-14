using iQuest.VendingMachine.Exceptions;
using iQuest.VendingMachine.Interfaces;
using System;

namespace iQuest.VendingMachine.PaymentMethods
{
    internal class CashPayment : IPaymentAlgorithm
    {
        ICashPaymentTerminal cashPaymentTerminal;
        private int paymentMethod;

        public string Name => "cash";
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

        public CashPayment(ICashPaymentTerminal cashPaymentTerminal, int PaymentMethod)
        {
            this.cashPaymentTerminal = cashPaymentTerminal ?? throw new ArgumentNullException(nameof(cashPaymentTerminal));
            this.PaymentMethod = PaymentMethod;
        }

        public void Run(decimal price)
        {
            decimal money = 0;
            decimal moneyReceived = 0;

            while (money < price)
            {
                try
                {
                    moneyReceived = cashPaymentTerminal.AskForMoney();
                    if (moneyReceived <= 0)
                    {
                        cashPaymentTerminal.GiveBackChange(money);
                        throw new CancelException();
                    }
                    else
                    {
                        money += moneyReceived;
                        cashPaymentTerminal.DisplayTotal(money);
                        if (money >= price)
                        {
                            cashPaymentTerminal.GiveBackChange(money - price);
                        }
                    }
                }
                catch (CancelException)
                {
                    cashPaymentTerminal.GiveBackChange(money);
                    throw new CancelException();
                }
                catch (Exception e) when (e is ArgumentNullException || e is FormatException)
                {
                    throw new CancelException();
                }
            }
        }
    }
}
