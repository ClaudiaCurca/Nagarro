using iQuest.VendingMachine.Authentication;
using iQuest.VendingMachine.Interfaces;
using iQuest.VendingMachine.UseCases;
using System;

namespace iQuest.VendingMachine.PresentationLayer.Commands
{
    internal class SupplyCommand : IVendingMachineCommand
    {
        private readonly IAuthenticationService authenticationService;
        private readonly IUseCaseFactory useCaseFactory;

        public string Name => "supply";
        public string Description => "supply new products and updating old ones";
        public bool CanExecute => authenticationService.IsUserAuthenticated;

        public SupplyCommand(IAuthenticationService authenticationService, IUseCaseFactory useCaseFactory)
        {
            this.authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
            this.useCaseFactory = useCaseFactory ?? throw new ArgumentNullException(nameof(useCaseFactory));
        }

        public void Execute()
        {
            SupplyUseCase supplyUseCase = useCaseFactory.Create<SupplyUseCase>();
            supplyUseCase.Execute();
        }
    }
}
