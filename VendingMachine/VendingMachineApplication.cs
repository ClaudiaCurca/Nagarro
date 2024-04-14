using System;
using System.Collections.Generic;
using iQuest.VendingMachine.Authentication;
using iQuest.VendingMachine.Exceptions;
using iQuest.VendingMachine.Interfaces;

namespace iQuest.VendingMachine
{
    internal class VendingMachineApplication
    {
        private readonly IEnumerable<IVendingMachineCommand> commands;
        private readonly IMainView mainView;
        private readonly LogHelper logger;

        public VendingMachineApplication(IEnumerable<IVendingMachineCommand> commands, IMainView mainView, LogHelper logger)
        {
            this.commands = commands ?? throw new ArgumentNullException(nameof(commands));
            this.mainView = mainView ?? throw new ArgumentNullException(nameof(mainView));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));   
        }

        public void Run()
        {
            logger.Info("The application has been opened");
            mainView.DisplayApplicationHeader();

            while (true)
            {
                List<IVendingMachineCommand> availableCommands = GetExecutableCommands();

                IVendingMachineCommand command = mainView.ChooseCommand(availableCommands);
                try
                {
                    logger.Info("The user has chosen the " + command.Name + " command");
                    command.Execute();
                }
                catch (Exception e) when (e is FormatException || e is InvalidColumnException ||
                    e is InsufficientStockException || e is CancelException ||
                    e is InvalidPasswordException)
                {
                    Console.WriteLine(e.Message);
                    logger.Error(e.Message);
                }

            }
        }

        private List<IVendingMachineCommand> GetExecutableCommands()
        {
            List<IVendingMachineCommand> executableCommands = new List<IVendingMachineCommand>();

            foreach (IVendingMachineCommand command in commands)
            {
                if (command.CanExecute)
                    executableCommands.Add(command);
            }

            return executableCommands;
        }
    }
}