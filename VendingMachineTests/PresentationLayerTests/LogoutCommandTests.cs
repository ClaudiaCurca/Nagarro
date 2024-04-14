using iQuest.VendingMachine;
using iQuest.VendingMachine.Authentication;
using iQuest.VendingMachine.Interfaces;
using iQuest.VendingMachine.PresentationLayer.Commands;
using iQuest.VendingMachine.UseCases;
using Moq;

namespace VendingMachineTests
{
    [TestClass]
    public class LogoutCommandTests
    {
        private Mock<IAuthenticationService> authenticationServiceMock = new Mock<IAuthenticationService>();
        private Mock<IUseCaseFactory> useCaseFactoryMock = new Mock<IUseCaseFactory>();

        [TestMethod]
        public void Name_WhenInitializingTheUseCase_NameIsCorrect()
        {
            const string expectedName = "logout";
            LogoutCommand logoutCommand = new LogoutCommand(authenticationServiceMock.Object,useCaseFactoryMock.Object);

            Assert.AreEqual(expectedName, logoutCommand.Name);
        }

        [TestMethod]
        public void Description_WhenInitializingTheUseCase_DescriptionIsCorrect()
        {
            const string expectedDescription = "Restrict access to administration section.";
            LogoutCommand logoutCommand = new LogoutCommand(authenticationServiceMock.Object, useCaseFactoryMock.Object);

            Assert.AreEqual(expectedDescription, logoutCommand.Description);
        }

        [TestMethod]
        public void CanExecute_WhenUserIsAuthenticated_ReturnsTrue()
        {
            authenticationServiceMock.Setup(x => x.IsUserAuthenticated).Returns(true);
            LogoutCommand logoutCommand = new LogoutCommand(authenticationServiceMock.Object, useCaseFactoryMock.Object);

            Assert.IsTrue(logoutCommand.CanExecute);
        }

        [TestMethod]
        public void CanExecute_WhenUserIsNotAuthenticated_ReturnsFalse()
        {
            authenticationServiceMock.Setup(x => x.IsUserAuthenticated).Returns(false);
            LogoutCommand logoutCommand = new LogoutCommand(authenticationServiceMock.Object, useCaseFactoryMock.Object);

            Assert.IsFalse(logoutCommand.CanExecute);
        }

        [TestMethod]
        public void Execute_Always_CallsCreate()
        {
            LogoutUseCase logoutUseCase = new LogoutUseCase(authenticationServiceMock.Object, Mock.Of<LogHelper>());
            useCaseFactoryMock.Setup(x => x.Create<LogoutUseCase>()).Returns(logoutUseCase);
            LogoutCommand logoutCommand = new LogoutCommand(authenticationServiceMock.Object, useCaseFactoryMock.Object);

            logoutCommand.Execute();

            useCaseFactoryMock.Verify(x => x.Create<LogoutUseCase>(), Times.Once);
        }

        [TestCleanup]
        public void CleanUp()
        {
            authenticationServiceMock.Reset();
            useCaseFactoryMock.Reset();
        }
    }
}
