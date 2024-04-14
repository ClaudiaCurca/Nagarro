using iQuest.VendingMachine;
using iQuest.VendingMachine.Exceptions;
using iQuest.VendingMachine.Interfaces;
using iQuest.VendingMachine.UseCases;
using Moq;

namespace VendingMachineTests
{
    [TestClass]
    public class SupplyUseCaseTests
    {
        private Mock<ISupplyView> supplyViewMock = new Mock<ISupplyView>();
        private Mock<IUnitOfWork> unitOfWorkMock = new Mock<IUnitOfWork>();
        private Mock<LogHelper> loggerMock = new Mock<LogHelper>();
        
        [TestMethod]
        public void Constructor_WhenSupplyViewIsNull_ThrowsArgumentNullException()
        {
            Assert.ThrowsException<System.ArgumentNullException>(() =>
            new SupplyUseCase(null, unitOfWorkMock.Object, loggerMock.Object));
        }

        [TestMethod]
        public void Constructor_WhenProductRepositoryIsNull_ThrowsArgumentNullException()
        {
            Assert.ThrowsException<System.ArgumentNullException>(() =>
            new SupplyUseCase(supplyViewMock.Object, null,loggerMock.Object));
        }

        [TestMethod]
        public void Execute_WhenColumnIdIsSmallerOrEqual0_ThrowsCancelException()
        {
            Product product = new Product() { ColumnId = -1, Name = "cola", Price = 5, Quantity = 5 };
            supplyViewMock.Setup(x => x.RequestProduct()).Returns(product);


            SupplyUseCase supplyUseCase = new SupplyUseCase(supplyViewMock.Object, unitOfWorkMock.Object, loggerMock.Object);

            Assert.ThrowsException<InvalidColumnException>(() => supplyUseCase.Execute());
        }

        [TestMethod]
        public void Execute_WhenColumnIdDoesntExists_AddNewProduct()
        {
            const int requestedColumn = 2;
            Product product = new Product() { ColumnId = requestedColumn, Name = "cola", Price = 5, Quantity = 5 };
            supplyViewMock.Setup(x => x.RequestProduct()).Returns(product);
            unitOfWorkMock.Setup(x=>x.Products.GetByColumn(requestedColumn)).Returns((Product)null);
            
            SupplyUseCase supplyUseCase = new SupplyUseCase(supplyViewMock.Object, unitOfWorkMock.Object, loggerMock.Object);

            supplyUseCase.Execute();

            unitOfWorkMock.Verify(x => x.Products.Add(product), Times.Once());
        }

        [TestMethod]
        public void Execute_WhenColumnIdExists_UpdateProduct()
        {
            const int requestedColumn = 2;
            Product product = new Product() { ColumnId = requestedColumn, Name = "cola", Price = 5, Quantity = 5 };
            supplyViewMock.Setup(x => x.RequestProduct()).Returns(product);
           
            unitOfWorkMock.Setup(x=>x.Products.GetByColumn(requestedColumn)).Returns(product);
            SupplyUseCase supplyUseCase = new SupplyUseCase(supplyViewMock.Object, unitOfWorkMock.Object, loggerMock.Object);

            supplyUseCase.Execute();

            unitOfWorkMock.Verify(x => x.Products.Update(product), Times.Once());
        }

        [TestCleanup]
        public void CleanUp()
        {
            supplyViewMock.Reset();
            unitOfWorkMock.Reset();
        }
    }
}
