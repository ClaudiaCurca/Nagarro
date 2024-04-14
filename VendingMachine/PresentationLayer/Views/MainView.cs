using System.Collections.Generic;
using iQuest.VendingMachine.Interfaces;

namespace iQuest.VendingMachine.PresentationLayer.Views
{
    internal class MainView : DisplayBase, IMainView
    {
        public void DisplayApplicationHeader()
        {
            ApplicationHeaderControl applicationHeaderControl = new ApplicationHeaderControl();
            applicationHeaderControl.Display();
        }

        public IVendingMachineCommand ChooseCommand(IEnumerable<IVendingMachineCommand> commands)
        {
            CommandSelectorControl commandSelectorControl = new CommandSelectorControl
            {
                Commands = commands
            };
            return commandSelectorControl.Display();
        }
    }
}