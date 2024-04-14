using iQuest.VendingMachine;
using iQuest.VendingMachine.Authentication;
using iQuest.VendingMachine.DataAccessLayer;
using iQuest.VendingMachine.FileFormat;
using iQuest.VendingMachine.Interfaces;
using iQuest.VendingMachine.PresentationLayer.Commands;
using iQuest.VendingMachine.ReportRepository;
using iQuest.VendingMachine.UseCases;
using Moq;

namespace VendingMachineTests.PresentationLayerTests
{
    [TestClass]
    public class SalesReportCommandTests
    {
        private Mock<IAuthenticationService> authenticationServiceMock = new Mock<IAuthenticationService>();
        private Mock<IUseCaseFactory> useCaseFactoryMock = new Mock<IUseCaseFactory>();
        private Mock<LogHelper> loggerMock = new Mock<LogHelper>();

        [TestMethod]
        public void Name_WhenInitializingTheUseCase_NameIsCorrect()
        {
            const string expectedName = "sales";
            SalesReportCommand salesReportCommand = new SalesReportCommand(authenticationServiceMock.Object, useCaseFactoryMock.Object);

            Assert.AreEqual(expectedName, salesReportCommand.Name);
        }

        [TestMethod]
        public void Description_WhenInitializingTheUseCase_DescriptionIsCorrect()
        {
            const string expectedDescription = "generate sales report";
            SalesReportCommand salesReportCommand = new SalesReportCommand(authenticationServiceMock.Object, useCaseFactoryMock.Object);

            Assert.AreEqual(expectedDescription, salesReportCommand.Description);
        }

        [TestMethod]
        public void CanExecute_WhenUserIsAuthenticated_ReturnsTrue()
        {
            authenticationServiceMock.Setup(x => x.IsUserAuthenticated).Returns(true);
            SalesReportCommand salesReportCommand = new SalesReportCommand(authenticationServiceMock.Object, useCaseFactoryMock.Object);

            Assert.IsTrue(salesReportCommand.CanExecute);
        }

        [TestMethod]
        public void CanExecute_WhenUserIsNotAuthenticated_ReturnsFalse()
        {
            authenticationServiceMock.Setup(x => x.IsUserAuthenticated).Returns(false);
            SalesReportCommand salesReportCommand = new SalesReportCommand(authenticationServiceMock.Object, useCaseFactoryMock.Object);

            Assert.IsFalse(salesReportCommand.CanExecute);
        }

        [TestMethod]
        public void Execute_Always_CallsCreate()
        {
            Mock<ISalesRepository> salesRepositoryMock = new Mock<ISalesRepository>();
            salesRepositoryMock.Setup(x => x.GetAll()).Returns(new List<Sales>());
            SalesReportRepository salesReportRepository = new SalesReportRepository(Mock.Of<IFileCreator<Sales>>());
            SalesReportUseCase salesReportUseCase = new SalesReportUseCase(salesReportRepository, salesRepositoryMock.Object, Mock.Of<LogHelper>());
            useCaseFactoryMock.Setup(x => x.Create<SalesReportUseCase>()).Returns(salesReportUseCase);
            SalesReportCommand salesReportCommand = new SalesReportCommand(authenticationServiceMock.Object, useCaseFactoryMock.Object);

            salesReportCommand.Execute();

            useCaseFactoryMock.Verify(x => x.Create<SalesReportUseCase>(), Times.Once);
        }

        [TestCleanup]
        public void CleanUp()
        {
            authenticationServiceMock.Reset();
            useCaseFactoryMock.Reset();
        }
    }
}
