using iQuest.VendingMachine;
using iQuest.VendingMachine.Authentication;
using iQuest.VendingMachine.UseCases;
using Moq;

namespace VendingMachineTests
{
    [TestClass]
    public class LogoutUseCaseTests
    {
        private Mock<IAuthenticationService> authenticationServiceMock = new Mock<IAuthenticationService>();
        private Mock<LogHelper> logHelperMock = new Mock<LogHelper>();

        [TestMethod]
        public void Constructor_WhenAuthenticationServiceIsNull_ThrowsArgumentNullException()
        {
            Assert.ThrowsException<System.ArgumentNullException>(() =>
            new LogoutUseCase(null, logHelperMock.Object));
        }

        [TestMethod]
        public void Execute()
        {
            LogoutUseCase logoutUseCase = new LogoutUseCase(authenticationServiceMock.Object, Mock.Of<LogHelper>());
            logoutUseCase.Execute();
            authenticationServiceMock.Verify(x => x.Logout(), Times.Once());
        }

        [TestCleanup]
        public void CleanUp()
        {
            authenticationServiceMock.Reset();
        }
    }
}
