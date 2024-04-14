using iQuest.VendingMachine;
using iQuest.VendingMachine.Authentication;
using iQuest.VendingMachine.Interfaces;
using iQuest.VendingMachine.PresentationLayer.Commands;
using iQuest.VendingMachine.UseCases;
using Moq;

namespace VendingMachineTests
{
    [TestClass]
    public class LoginCommandTests
    {
        private Mock<IAuthenticationService> authenticationServiceMock = new Mock<IAuthenticationService>();
        private Mock<IUseCaseFactory> useCaseFactoryMock = new Mock<IUseCaseFactory>();

        [TestMethod]
        public void Name_WhenInitializingTheUseCase_NameIsCorrect()
        {
            const string expectedName = "login";
            LoginCommand loginCommand = new LoginCommand(authenticationServiceMock.Object,
                useCaseFactoryMock.Object);

            Assert.AreEqual(expectedName, loginCommand.Name);
        }

        [TestMethod]
        public void Description_WhenInitializingTheUseCase_DescriptionIsCorrect()
        {
            const string expectedDescription = "Get access to administration section.";
            LoginCommand loginCommand = new LoginCommand(authenticationServiceMock.Object,
                useCaseFactoryMock.Object);

            Assert.AreEqual(expectedDescription, loginCommand.Description);
        }

        [TestMethod]
        public void CanExecute_WhenUserIsNotAuthenticated_ReturnsTrue()
        {
            authenticationServiceMock.Setup(x => x.IsUserAuthenticated).Returns(false);
            LoginCommand loginCommand = new LoginCommand(authenticationServiceMock.Object,
                useCaseFactoryMock.Object);

            Assert.IsTrue(loginCommand.CanExecute);
        }

        [TestMethod]
        public void CanExecute_WhenUserIsAuthenticated_ReturnsFalse()
        {
            authenticationServiceMock.Setup(x => x.IsUserAuthenticated).Returns(true);
            LoginCommand loginCommand = new LoginCommand(authenticationServiceMock.Object,
                useCaseFactoryMock.Object);

            Assert.IsFalse(loginCommand.CanExecute);
        }

        [TestMethod]
        public void Execute_Always_CallsCreate()
        {
            LoginUseCase loginUseCase = new LoginUseCase(authenticationServiceMock.Object, Mock.Of<ILoginView>(), Mock.Of<LogHelper>());
            useCaseFactoryMock.Setup(x => x.Create<LoginUseCase>()).Returns(loginUseCase);
            LoginCommand loginCommand = new LoginCommand(authenticationServiceMock.Object, useCaseFactoryMock.Object);

            loginCommand.Execute();

            useCaseFactoryMock.Verify(x => x.Create<LoginUseCase>(), Times.Once);
        }

        [TestCleanup]
        public void CleanUp()
        {
            authenticationServiceMock.Reset();
            useCaseFactoryMock.Reset();
        }
    }
}
