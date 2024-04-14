namespace iQuest.VendingMachine.Interfaces
{
    internal interface ICardPaymentTerminal
    {
        public string AskForCardNumber();

        public void TransactionAccepted();

        public void TransactionRejected();
    }
}
