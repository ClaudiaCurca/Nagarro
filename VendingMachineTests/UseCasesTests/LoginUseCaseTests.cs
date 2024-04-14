using iQuest.VendingMachine;
using iQuest.VendingMachine.Authentication;
using iQuest.VendingMachine.Interfaces;
using iQuest.VendingMachine.UseCases;
using Moq;

namespace VendingMachineTests
{
    [TestClass]
    public class LoginUseCaseTests
    {
        private Mock<IAuthenticationService> authenticationServiceMock = new Mock<IAuthenticationService>();
        private Mock<ILoginView> loginViewMock = new Mock<ILoginView>();
        private Mock<LogHelper> logHelperMock = new Mock<LogHelper>();

        [TestMethod]
        public void Constructor_WhenLoginViewIsNull_ThrowsArgumentNullException()
        {
            Assert.ThrowsException<System.ArgumentNullException>(() =>
            new LoginUseCase(authenticationServiceMock.Object, null, logHelperMock.Object));
        }

        [TestMethod]
        public void Constructor_WhenAuthenticationServiceIsNull_ThrownArgumentNullException()
        {
            Assert.ThrowsException<System.ArgumentNullException>(() =>
            new LoginUseCase(null, loginViewMock.Object, logHelperMock.Object));
        }

        [TestMethod]
        public void Execute_CallsLogin()
        {
            const string password = "110120";
            loginViewMock.Setup(s => s.AskForPassword()).Returns(password);
            LoginUseCase loginUseCase = new LoginUseCase(authenticationServiceMock.Object, loginViewMock.Object, logHelperMock.Object);

            loginUseCase.Execute();

            authenticationServiceMock.Verify(x => x.Login(password), Times.Once());
        }

        [TestMethod]
        public void Execute_CallsAskForPassword()
        {
            LoginUseCase loginUseCase = new LoginUseCase(authenticationServiceMock.Object, loginViewMock.Object, logHelperMock.Object);

            loginUseCase.Execute();

            loginViewMock.Verify(x => x.AskForPassword(), Times.Once());
        }

        [TestCleanup]
        public void CleanUp()
        {
            authenticationServiceMock.Reset();
            loginViewMock.Reset();
        }

    }
}
