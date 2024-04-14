using iQuest.VendingMachine.Authentication;
using iQuest.VendingMachine.Interfaces;
using iQuest.VendingMachine.UseCases;
using System;

namespace iQuest.VendingMachine.PresentationLayer.Commands
{
    internal class BuyCommand : IVendingMachineCommand
    {
        private readonly IAuthenticationService authenticationService;
        private readonly IUseCaseFactory useCaseFactory;
        public string Name => "buy";
        public string Description => "Buy product.";
        public bool CanExecute => !authenticationService.IsUserAuthenticated;

        public BuyCommand(IAuthenticationService authenticationService, IUseCaseFactory useCaseFactory)
        {
            this.authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
            this.useCaseFactory = useCaseFactory ?? throw new ArgumentNullException(nameof(useCaseFactory));
        }

        public void Execute()
        {
            IUseCase useCase = useCaseFactory.Create<BuyUseCase>();
            useCase.Execute();
        }
    }
}
