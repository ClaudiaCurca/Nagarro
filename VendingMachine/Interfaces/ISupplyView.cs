namespace iQuest.VendingMachine.Interfaces
{
    internal interface ISupplyView
    {
        public void SupplyInformation();

        public Product RequestProduct();
    }
}
