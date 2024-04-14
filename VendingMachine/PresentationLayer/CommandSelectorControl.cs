using System;
using System.Collections.Generic;
using iQuest.VendingMachine.Interfaces;

namespace iQuest.VendingMachine.PresentationLayer
{
    internal class CommandSelectorControl : DisplayBase
    {
        public IEnumerable<IVendingMachineCommand> Commands { get; set; }

        public IVendingMachineCommand Display()
        {
            DisplayCommands();
            return SelectCommand();
        }

        private void DisplayCommands()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Available commands:");
            Console.WriteLine();

            foreach (IVendingMachineCommand command in Commands)
                DisplayCommand(command);
        }

        private static void DisplayCommand(IVendingMachineCommand command)
        {
            ConsoleColor oldColor = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(command.Name);

            Console.ForegroundColor = oldColor;

            Console.WriteLine(" - " + command.Description);
        }

        private IVendingMachineCommand SelectCommand()
        {
            while (true)
            {
                string rawValue = ReadCommandName();
                IVendingMachineCommand selectedCommand = FindCommand(rawValue);

                if (selectedCommand != null)
                    return selectedCommand;

                DisplayLine("Invalid command. Please try again.", ConsoleColor.Red);
            }
        }

        private IVendingMachineCommand FindCommand(string rawValue)
        {
            IVendingMachineCommand selectedUseCase = null;

            foreach (IVendingMachineCommand x in Commands)
            {
                if (x.Name == rawValue)
                {
                    selectedUseCase = x;
                    break;
                }
            }

            return selectedUseCase;
        }

        private string ReadCommandName()
        {
            Console.WriteLine();
            Display("Choose command: ", ConsoleColor.Cyan);
            string rawValue = Console.ReadLine();
            Console.WriteLine();

            return rawValue;
        }
    }
}