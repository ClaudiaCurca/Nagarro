using iQuest.VendingMachine.Authentication;
using iQuest.VendingMachine.Interfaces;
using iQuest.VendingMachine.UseCases;
using System;
using System.Collections.Generic;
using System.Text;

namespace iQuest.VendingMachine.PresentationLayer.Commands
{
    internal class StockReportCommand : IVendingMachineCommand
    {
        private readonly IAuthenticationService authenticationService;
        private readonly IUseCaseFactory useCaseFactory;

        public string Name => "stock";
        public string Description => "generate stock report";
        public bool CanExecute => authenticationService.IsUserAuthenticated;

        public StockReportCommand(IAuthenticationService authenticationService, IUseCaseFactory useCaseFactory)
        {
            this.authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
            this.useCaseFactory = useCaseFactory ?? throw new ArgumentNullException(nameof(useCaseFactory));
        }

        public void Execute()
        {
            StockReportUseCase stockReportUseCase = useCaseFactory.Create<StockReportUseCase>();
            stockReportUseCase.Execute();
        }
    }
}
