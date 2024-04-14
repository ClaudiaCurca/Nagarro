using System;
using iQuest.VendingMachine.Authentication;
using iQuest.VendingMachine.Interfaces;

namespace iQuest.VendingMachine.UseCases
{
    internal class LoginUseCase : IUseCase
    {
        private readonly IAuthenticationService authenticationService;
        private readonly ILoginView loginView;
        private readonly LogHelper logger;

        public LoginUseCase(IAuthenticationService authenticationService, ILoginView loginView, LogHelper logger)
        {
            this.authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
            this.loginView = loginView ?? throw new ArgumentNullException(nameof(loginView));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void Execute()
        {
            logger.Info("Login UseCase");
            string password = loginView.AskForPassword();
            authenticationService.Login(password);
        }
    }
}