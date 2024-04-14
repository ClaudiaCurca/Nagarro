using iQuest.VendingMachine.Authentication;
using iQuest.VendingMachine.Interfaces;
using iQuest.VendingMachine.UseCases;
using System;

namespace iQuest.VendingMachine.PresentationLayer.Commands
{
    internal class SalesReportCommand : IVendingMachineCommand
    {
        private readonly IAuthenticationService authenticationService;
        private readonly IUseCaseFactory useCaseFactory;

        public string Name => "sales";
        public string Description => "generate sales report";
        public bool CanExecute => authenticationService.IsUserAuthenticated;

        public SalesReportCommand(IAuthenticationService authenticationService,IUseCaseFactory useCaseFactory)
        {
            this.authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
            this.useCaseFactory = useCaseFactory ?? throw new ArgumentNullException(nameof(useCaseFactory));
        }

        public void Execute()
        {
            SalesReportUseCase salesReportUseCase = useCaseFactory.Create<SalesReportUseCase>();
            salesReportUseCase.Execute();
        }
    }
}
