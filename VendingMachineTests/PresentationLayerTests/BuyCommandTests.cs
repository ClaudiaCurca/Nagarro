using iQuest.VendingMachine;
using iQuest.VendingMachine.Authentication;
using iQuest.VendingMachine.DataAccessLayer;
using iQuest.VendingMachine.Interfaces;
using iQuest.VendingMachine.Payment;
using iQuest.VendingMachine.PresentationLayer.Commands;
using iQuest.VendingMachine.UseCases;
using Moq;

namespace VendingMachineTests
{
    [TestClass]
    public class BuyCommandTests
    {
        private Mock<IAuthenticationService> authenticationServiceMock = new Mock<IAuthenticationService>();
        private Mock<IUseCaseFactory> useCaseFactoryMock = new Mock<IUseCaseFactory>();

        [TestMethod]
        public void Name_WhenInitializingTheUseCase_NameIsCorrect()
        {
            const string expectedName = "buy";

            BuyCommand buyCommand = new BuyCommand(authenticationServiceMock.Object, useCaseFactoryMock.Object);

            Assert.AreEqual(expectedName, buyCommand.Name);
        }

        [TestMethod]
        public void Description_WhenInitializingTheUseCase_DescriptionIsCorrect()
        {
            const string expectedName = "Buy product.";
            BuyCommand buyCommand = new BuyCommand(authenticationServiceMock.Object, useCaseFactoryMock.Object);

            Assert.AreEqual(expectedName, buyCommand.Description);
        }

        [TestMethod]
        public void CanExecute_WhenUserIsNotAuthenticated_ReturnsTrue()
        {
            authenticationServiceMock.Setup(x => x.IsUserAuthenticated).Returns(false);
            BuyCommand buyCommand = new BuyCommand(authenticationServiceMock.Object, useCaseFactoryMock.Object);

            Assert.IsTrue(buyCommand.CanExecute);
        }

        [TestMethod]
        public void CanExecute_WhenUserIsAuthenticated_ReturnsFalse()
        {
            authenticationServiceMock.Setup(x => x.IsUserAuthenticated).Returns(true);
            BuyCommand buyCommand = new BuyCommand(authenticationServiceMock.Object, useCaseFactoryMock.Object);

            Assert.AreEqual(false, buyCommand.CanExecute);
        }

        [TestMethod]
        public void Execute_Always_CallsCreate()
        {
            Product product = new Product
            {
                Quantity = 10
            };
            Mock<IUnitOfWork> productRepositoryMock = new Mock<IUnitOfWork>();
            Mock<IPaymentUseCase> paymentUseCaseMock = new Mock<IPaymentUseCase>();
            paymentUseCaseMock.Setup(x => x.GetPaymentMethod()).Returns(new PaymentMethod(1, "cash"));
            productRepositoryMock.Setup(x => x.Products.GetByColumn(It.IsAny<int>())).Returns(product);
            BuyUseCase buyUseCase = new BuyUseCase(Mock.Of<IBuyView>(), productRepositoryMock.Object, paymentUseCaseMock.Object, Mock.Of<ISalesRepository>(),Mock.Of<LogHelper>());
            useCaseFactoryMock.Setup(x => x.Create<BuyUseCase>()).Returns(buyUseCase);
            BuyCommand buyCommand = new BuyCommand(authenticationServiceMock.Object, useCaseFactoryMock.Object);

            buyCommand.Execute();

            useCaseFactoryMock.Verify(x => x.Create<BuyUseCase>(), Times.Once);
        }

        [TestCleanup]
        public void CleanUp()
        {
            authenticationServiceMock.Reset();
            useCaseFactoryMock.Reset();
        }
    }
}
