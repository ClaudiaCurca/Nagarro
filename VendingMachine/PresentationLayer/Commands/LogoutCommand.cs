using iQuest.VendingMachine.Authentication;
using iQuest.VendingMachine.Interfaces;
using iQuest.VendingMachine.UseCases;
using System;

namespace iQuest.VendingMachine.PresentationLayer.Commands
{
    internal class LogoutCommand : IVendingMachineCommand
    {
        private readonly IAuthenticationService authenticationService;
        private readonly IUseCaseFactory useCaseFactory;
        public string Name => "logout";
        public string Description => "Restrict access to administration section.";
        public bool CanExecute => authenticationService.IsUserAuthenticated;

        public LogoutCommand(IAuthenticationService authenticationService, IUseCaseFactory useCaseFactory)
        {
            this.authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
            this.useCaseFactory = useCaseFactory ?? throw new ArgumentNullException(nameof(useCaseFactory));
        }

        public void Execute()
        {
            LogoutUseCase logoutUseCase = useCaseFactory.Create<LogoutUseCase>();
            logoutUseCase.Execute();
        }
    }
}
