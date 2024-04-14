using System;
using iQuest.VendingMachine.Authentication;
using iQuest.VendingMachine.Interfaces;

namespace iQuest.VendingMachine.UseCases
{
    internal class LogoutUseCase : IUseCase
    {
        private readonly IAuthenticationService authenticationService;
        private readonly LogHelper logger;

        public LogoutUseCase(IAuthenticationService authenticationService, LogHelper logger)
        {
            this.authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void Execute()
        {
            logger.Info("Logout UseCase");
            authenticationService.Logout();
        }
    }
}