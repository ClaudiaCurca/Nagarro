using iQuest.VendingMachine.Authentication;
using iQuest.VendingMachine.Interfaces;
using iQuest.VendingMachine.UseCases;
using System;

namespace iQuest.VendingMachine.PresentationLayer.Commands
{
    internal class LoginCommand : IVendingMachineCommand
    {
        private readonly IAuthenticationService authenticationService;

        private readonly IUseCaseFactory useCaseFactory;
        public string Name => "login";
        public string Description => "Get access to administration section.";
        public bool CanExecute => !authenticationService.IsUserAuthenticated;

        public LoginCommand(IAuthenticationService authenticationService, IUseCaseFactory useCaseFactory)
        {
            this.authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
            this.useCaseFactory = useCaseFactory ?? throw new ArgumentNullException(nameof(useCaseFactory));
        }

        public void Execute()
        {
            LoginUseCase loginUseCase = useCaseFactory.Create<LoginUseCase>();
            loginUseCase.Execute();
        }
    }
}
