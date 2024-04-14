using iQuest.VendingMachine;
using iQuest.VendingMachine.Authentication;
using iQuest.VendingMachine.Interfaces;
using iQuest.VendingMachine.PresentationLayer.Commands;
using iQuest.VendingMachine.UseCases;
using Moq;

namespace VendingMachineTests
{
    [TestClass]
    public class SupplyCommandTests
    {
        private Mock<IAuthenticationService> authenticationServiceMock = new Mock<IAuthenticationService>();
        private Mock<IUseCaseFactory> useCaseFactoryMock = new Mock<IUseCaseFactory>();

        [TestMethod]
        public void Name_WhenInitializingTheUseCase_NameIsCorrect()
        {
            const string expectedName = "supply";
            SupplyCommand supplyCommand = new SupplyCommand(authenticationServiceMock.Object, useCaseFactoryMock.Object);

            Assert.AreEqual(expectedName, supplyCommand.Name);
        }

        [TestMethod]
        public void Description_WhenInitializingTheUseCase_DescriptionIsCorrect()
        {
            const string expectedDescription = "supply new products and updating old ones";
            SupplyCommand supplyCommand = new SupplyCommand(authenticationServiceMock.Object, useCaseFactoryMock.Object);

            Assert.AreEqual(expectedDescription, supplyCommand.Description);
        }

        [TestMethod]
        public void CanExecute_WhenUserIsAuthenticated_ReturnsTrue()
        {
            authenticationServiceMock.Setup(x => x.IsUserAuthenticated).Returns(true);
            SupplyCommand supplyCommand = new SupplyCommand(authenticationServiceMock.Object, useCaseFactoryMock.Object);

            Assert.IsTrue(supplyCommand.CanExecute);
        }

        [TestMethod]
        public void CanExecute_WhenUserIsNotAuthenticated_ReturnsFalse()
        {
            authenticationServiceMock.Setup(x => x.IsUserAuthenticated).Returns(false);
            SupplyCommand supplyCommand = new SupplyCommand(authenticationServiceMock.Object, useCaseFactoryMock.Object);

            Assert.IsFalse(supplyCommand.CanExecute);
        }

        [TestMethod]
        public void Execute_Always_CallsCreate()
        {
            Product product = new Product
            {
                Name = "Chocolate",
                Price = 9,
                Quantity = 0,
                ColumnId = 1
            };
            Mock<ISupplyView> supplyViewMock = new Mock<ISupplyView>();
            supplyViewMock.Setup(x => x.RequestProduct()).Returns(product);

            SupplyUseCase supplyUseCase = new SupplyUseCase(supplyViewMock.Object, Mock.Of<IUnitOfWork>(),Mock.Of<LogHelper>());
            useCaseFactoryMock.Setup(x => x.Create<SupplyUseCase>()).Returns(supplyUseCase);
            SupplyCommand supplyCommand = new SupplyCommand(authenticationServiceMock.Object, useCaseFactoryMock.Object);

            supplyCommand.Execute();

            useCaseFactoryMock.Verify(x => x.Create<SupplyUseCase>(), Times.Once);
        }

        [TestCleanup]
        public void CleanUp()
        {
            authenticationServiceMock.Reset();
            useCaseFactoryMock.Reset();
        }
    }
}
