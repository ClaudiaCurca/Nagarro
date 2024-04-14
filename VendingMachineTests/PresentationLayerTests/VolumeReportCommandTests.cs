using iQuest.VendingMachine.Authentication;
using iQuest.VendingMachine.Interfaces;
using iQuest.VendingMachine.PresentationLayer.Commands;
using iQuest.VendingMachine.UseCases;
using Moq;
using iQuest.VendingMachine.ReportRepository;
using iQuest.VendingMachine.FileFormat;
using iQuest.VendingMachine;
using iQuest.VendingMachine.DataAccessLayer;

namespace VendingMachineTests.PresentationLayerTests
{
    [TestClass]
    public class VolumeReportCommandTests
    {
        private Mock<IAuthenticationService> authenticationServiceMock = new Mock<IAuthenticationService>();
        private Mock<IUseCaseFactory> useCaseFactoryMock = new Mock<IUseCaseFactory>();

        [TestMethod]
        public void Name_WhenInitializingTheUseCase_NameIsCorrect()
        {
            const string expectedName = "volume";
            VolumeReportCommand volumeReportCommand = new VolumeReportCommand(authenticationServiceMock.Object, useCaseFactoryMock.Object);

            Assert.AreEqual(expectedName, volumeReportCommand.Name);
        }

        [TestMethod]
        public void Description_WhenInitializingTheUseCase_DescriptionIsCorrect()
        {
            const string expectedDescription = "generate volume report";
            VolumeReportCommand volumeReportCommand = new VolumeReportCommand(authenticationServiceMock.Object, useCaseFactoryMock.Object);

            Assert.AreEqual(expectedDescription, volumeReportCommand.Description);
        }

        [TestMethod]
        public void CanExecute_WhenUserIsAuthenticated_ReturnsTrue()
        {
            authenticationServiceMock.Setup(x => x.IsUserAuthenticated).Returns(true);
            VolumeReportCommand volumeReportCommand = new VolumeReportCommand(authenticationServiceMock.Object, useCaseFactoryMock.Object);

            Assert.IsTrue(volumeReportCommand.CanExecute);
        }

        [TestMethod]
        public void CanExecute_WhenUserIsNotAuthenticated_ReturnsFalse()
        {
            authenticationServiceMock.Setup(x => x.IsUserAuthenticated).Returns(false);
            VolumeReportCommand volumeReportCommand = new VolumeReportCommand(authenticationServiceMock.Object, useCaseFactoryMock.Object);

            Assert.IsFalse(volumeReportCommand.CanExecute);
        }

        [TestMethod]
        public void Execute_Always_CallsCreate()
        {
            Mock<IVolumeReportView> volumeReportViewMock = new Mock<IVolumeReportView>();
            var startDateString = "2023/04/01 8:30:52 AM";
            DateTime startDate = DateTime.Parse(startDateString,
                                      System.Globalization.CultureInfo.InvariantCulture);
            var endDateString = "2023/04/06 8:30:52 AM";
            DateTime endDate = DateTime.Parse(endDateString,
                                      System.Globalization.CultureInfo.InvariantCulture);
            volumeReportViewMock.Setup(x => x.RequestStartTime()).Returns(startDate);
            volumeReportViewMock.Setup(x => x.RequestEndTime()).Returns(endDate);

            Mock<ISalesRepository> salesRepositoryMock = new Mock<ISalesRepository>();
            salesRepositoryMock.Setup(x => x.GetByDate(startDate, endDate)).Returns(new List<Sales>());

            VolumeReportRepository reportRepository = new VolumeReportRepository(Mock.Of<IFileCreator<SalesVolume>>());

            VolumeReportUseCase volumeReportUseCase = new VolumeReportUseCase(reportRepository, volumeReportViewMock.Object, salesRepositoryMock.Object, Mock.Of<LogHelper>());
            useCaseFactoryMock.Setup(x => x.Create<VolumeReportUseCase>()).Returns(volumeReportUseCase);
            VolumeReportCommand volumeReportCommand = new VolumeReportCommand(authenticationServiceMock.Object, useCaseFactoryMock.Object);

            volumeReportCommand.Execute();

            useCaseFactoryMock.Verify(x => x.Create<VolumeReportUseCase>(), Times.Once);
        }

        [TestCleanup]
        public void CleanUp()
        {
            authenticationServiceMock.Reset();
            useCaseFactoryMock.Reset();
        }
    }
}
