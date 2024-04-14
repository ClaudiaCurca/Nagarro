namespace iQuest.VendingMachine.Interfaces
{
    internal interface IVendingMachineCommand
    {
        public string Name { get; }

        public string Description { get; }

        public bool CanExecute { get; }

        public void Execute();
    }
}
