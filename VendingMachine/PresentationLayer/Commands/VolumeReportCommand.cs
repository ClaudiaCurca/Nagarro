using iQuest.VendingMachine.Authentication;
using iQuest.VendingMachine.Interfaces;
using iQuest.VendingMachine.UseCases;
using System;

namespace iQuest.VendingMachine.PresentationLayer.Commands
{
    internal class VolumeReportCommand : IVendingMachineCommand
    {
        private readonly IAuthenticationService authenticationService;
        private readonly IUseCaseFactory useCaseFactory;

        public string Name => "volume";
        public string Description => "generate volume report";
        public bool CanExecute => authenticationService.IsUserAuthenticated;

        public VolumeReportCommand(IAuthenticationService authenticationService, IUseCaseFactory useCaseFactory)
        {
            this.authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
            this.useCaseFactory = useCaseFactory ?? throw new ArgumentNullException(nameof(useCaseFactory));
        }
        
        public void Execute()
        {
            VolumeReportUseCase volumeReportUseCase = useCaseFactory.Create<VolumeReportUseCase>();
            volumeReportUseCase.Execute();
        }
    }
}
