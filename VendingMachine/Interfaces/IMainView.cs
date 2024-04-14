using System.Collections.Generic;

namespace iQuest.VendingMachine.Interfaces
{
    internal interface IMainView
    {
        IVendingMachineCommand ChooseCommand(IEnumerable<IVendingMachineCommand> commands);
        void DisplayApplicationHeader();
    }
}