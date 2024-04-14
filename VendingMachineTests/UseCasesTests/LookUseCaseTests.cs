using iQuest.VendingMachine;
using iQuest.VendingMachine.Authentication;
using iQuest.VendingMachine.Interfaces;
using iQuest.VendingMachine.UseCases;
using Moq;

namespace VendingMachineTests
{
    [TestClass]
    public class LookUseCaseTests
    {
        private Mock<IShelfView> shelfViewMock = new Mock<IShelfView>();
        private Mock<IUnitOfWork> unitOfWorkMock = new Mock<IUnitOfWork>();
        private Mock<IAuthenticationService> authenticationServiceMock = new Mock<IAuthenticationService>();
        private Mock<LogHelper> loggerMock = new Mock<LogHelper>();

        [TestMethod]
        public void Constructor_WhenAuthenticationServiceIsNull_ThrowsArgumentNullException()
        {
            Assert.ThrowsException<System.ArgumentNullException>(() =>
            new LookUseCase(shelfViewMock.Object, unitOfWorkMock.Object, null, loggerMock.Object));
        }

        [TestMethod]
        public void Constructor_WhenProductRepositoryIsNull_ThrowsArgumentNullException()
        {
            Assert.ThrowsException<System.ArgumentNullException>(() =>
            new LookUseCase(shelfViewMock.Object, null, authenticationServiceMock.Object, loggerMock.Object));
        }

        [TestMethod]
        public void Constructor_WhenShelfViewIsNull_ThrowsArgumentNullException()
        {
            Assert.ThrowsException<System.ArgumentNullException>(() =>
            new LookUseCase(null, unitOfWorkMock.Object, authenticationServiceMock.Object, Mock.Of<LogHelper>()));
        }

        [TestMethod]
        public void Execute_CallsDisplayProduct()
        {
            List<Product> listOfProducts = 
                new List<Product> 
                {
                    new Product
                    {
                        Name = "Chocolate",
                        Price = 9,
                        Quantity = 0,
                        ColumnId = 1
                    },
                    new Product
                    {
                        Name = "Tea",
                        Price = 9,
                        Quantity = 10,
                        ColumnId = 2
                    }
                };
            unitOfWorkMock.Setup(x => x.Products.GetAll()).Returns(listOfProducts);
            var lookUseCase = new LookUseCase(shelfViewMock.Object, unitOfWorkMock.Object, authenticationServiceMock.Object, loggerMock.Object);

            lookUseCase.Execute();
            
            shelfViewMock.Verify(shelfViewMock => shelfViewMock.DisplayProducts(It.Is<List<Product>>
                (z => z.All(s => s.Quantity > 0))),Times.Once);
        }

        [TestMethod]
        public void Execute_CallsGetAll()
        {
            List<Product> listOfProducts =
                new List<Product>
                {
                new Product
                {
                    Name = "Chocolate",
                    Price = 9,
                    Quantity = 0,
                    ColumnId = 1
                },
                new Product
                {
                    Name = "Tea",
                    Price = 9,
                    Quantity = 10,
                    ColumnId = 2
                }
                };
            unitOfWorkMock.Setup(x => x.Products.GetAll()).Returns(listOfProducts);
            var lookUseCase = new LookUseCase(shelfViewMock.Object, unitOfWorkMock.Object, authenticationServiceMock.Object, loggerMock.Object);

            lookUseCase.Execute();

            unitOfWorkMock.Verify(x => x.Products.GetAll(),Times.Once());
        }

        [TestCleanup]
        public void CleanUp()
        {
            shelfViewMock.Reset();
            authenticationServiceMock.Reset();
            unitOfWorkMock.Reset();
        }
    }
}
