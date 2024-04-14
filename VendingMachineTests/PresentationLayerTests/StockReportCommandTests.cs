using iQuest.VendingMachine;
using iQuest.VendingMachine.Authentication;
using iQuest.VendingMachine.FileFormat;
using iQuest.VendingMachine.Interfaces;
using iQuest.VendingMachine.PresentationLayer.Commands;
using iQuest.VendingMachine.ReportRepository;
using iQuest.VendingMachine.UseCases;
using Moq;

namespace VendingMachineTests.PresentationLayerTests
{
    [TestClass]
    public class StockReportCommandTests
    {
        private Mock<IAuthenticationService> authenticationServiceMock = new Mock<IAuthenticationService>();
        private Mock<IUseCaseFactory> useCaseFactoryMock = new Mock<IUseCaseFactory>();

        [TestMethod]
        public void Name_WhenInitializingTheUseCase_NameIsCorrect()
        {
            const string expectedName = "stock";
            StockReportCommand stockReportCommand = new StockReportCommand(authenticationServiceMock.Object, useCaseFactoryMock.Object);

            Assert.AreEqual(expectedName, stockReportCommand.Name);
        }

        [TestMethod]
        public void Description_WhenInitializingTheUseCase_DescriptionIsCorrect()
        {
            const string expectedDescription = "generate stock report";
            StockReportCommand stockReportCommand = new StockReportCommand(authenticationServiceMock.Object, useCaseFactoryMock.Object);

            Assert.AreEqual(expectedDescription, stockReportCommand.Description);
        }

        [TestMethod]
        public void CanExecute_WhenUserIsAuthenticated_ReturnsTrue()
        {
            authenticationServiceMock.Setup(x => x.IsUserAuthenticated).Returns(true);
            StockReportCommand stockReportCommand = new StockReportCommand(authenticationServiceMock.Object, useCaseFactoryMock.Object);

            Assert.IsTrue(stockReportCommand.CanExecute);
        }

        [TestMethod]
        public void CanExecute_WhenUserIsNotAuthenticated_ReturnsFalse()
        {
            authenticationServiceMock.Setup(x => x.IsUserAuthenticated).Returns(false);
            StockReportCommand stockReportCommand = new StockReportCommand(authenticationServiceMock.Object, useCaseFactoryMock.Object);

            Assert.IsFalse(stockReportCommand.CanExecute);
        }

        [TestMethod]
        public void Execute_Always_CallsCreate()
        {
            Mock<IUnitOfWork> unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(x=>x.Products.GetAll()).Returns(new List<Product>());
            StockReportRepository stockReportRepository = new StockReportRepository(Mock.Of<IFileCreator<Stock>>());
            StockReportUseCase stockReportUseCase = new StockReportUseCase(stockReportRepository,unitOfWorkMock.Object,Mock.Of<LogHelper>());
            useCaseFactoryMock.Setup(x => x.Create<StockReportUseCase>()).Returns(stockReportUseCase);
            StockReportCommand stockReportCommand = new StockReportCommand(authenticationServiceMock.Object, useCaseFactoryMock.Object);

            stockReportCommand.Execute();

            useCaseFactoryMock.Verify(x => x.Create<StockReportUseCase>(), Times.Once);
        }

        [TestCleanup]
        public void CleanUp()
        {
            authenticationServiceMock.Reset();
            useCaseFactoryMock.Reset();
        }
    }
}
