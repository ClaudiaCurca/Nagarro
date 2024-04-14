namespace iQuest.VendingMachine.Interfaces
{
    internal interface IPaymentAlgorithm
    {
        public string Name { get; }

        public int PaymentMethod { get; set; }

        public void Run(decimal price);
    }
}
