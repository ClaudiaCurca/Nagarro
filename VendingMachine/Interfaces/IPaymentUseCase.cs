using iQuest.VendingMachine.Payment;

namespace iQuest.VendingMachine.Interfaces
{
    internal interface IPaymentUseCase
    {
        public string Name { get; }

        public string Description { get; }

        public bool CanExecute { get; }

        public PaymentMethod GetPaymentMethod();

        public void Execute(decimal price);
    }
}
