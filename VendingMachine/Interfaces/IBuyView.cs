using iQuest.VendingMachine.Payment;
using System.Collections.Generic;

namespace iQuest.VendingMachine.Interfaces
{
    internal interface IBuyView
    {
        public int RequestProduct();
        public void DispenseProduct(string productName);
        public int? AskForPaymentMethod(List<PaymentMethod> paymentMethods);
    }
}
