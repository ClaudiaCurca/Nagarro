namespace iQuest.VendingMachine.Interfaces
{
    internal interface ICashPaymentTerminal
    {
        public decimal AskForMoney();

        public void GiveBackChange(decimal change);

        public void DisplayTotal(decimal money);
    }
}
