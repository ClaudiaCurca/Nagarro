using iQuest.VendingMachine.Interfaces;
using iQuest.VendingMachine.UseCases;
using System;

namespace iQuest.VendingMachine.PresentationLayer.Commands
{
    internal class LookCommand : IVendingMachineCommand
    {
        private readonly IUseCaseFactory useCaseFactory;

        public string Name => "look";
        public string Description => "Looking at the products";
        public bool CanExecute => true;

        public LookCommand(IUseCaseFactory useCaseFactory)
        {
            this.useCaseFactory = useCaseFactory ?? throw new ArgumentNullException(nameof(useCaseFactory));
        }

        public void Execute()
        {
            LookUseCase lookUseCase = useCaseFactory.Create<LookUseCase>();
            lookUseCase.Execute();
        }
    }
}
