using iQuest.VendingMachine.UseCases;
using iQuest.VendingMachine;
using Moq;
using iQuest.VendingMachine.Authentication;
using iQuest.VendingMachine.Exceptions;
using iQuest.VendingMachine.Interfaces;
using iQuest.VendingMachine.Payment;
using iQuest.VendingMachine.DataAccessLayer;

namespace VendingMachineTests
{
    [TestClass]
    public class BuyUseCaseTests
    {
        private Mock<IAuthenticationService> authenticationServiceMock = new Mock<IAuthenticationService>();
        private Mock<IBuyView> buyViewMock = new Mock<IBuyView>();
        private Mock<IUnitOfWork> unitOfWorkMock = new Mock<IUnitOfWork>();
        private Mock<IPaymentUseCase> paymentUseCaseMock = new Mock<IPaymentUseCase>();
        private Mock<ISalesRepository> salesRepositoryMock = new Mock<ISalesRepository>();
        private Mock<LogHelper> loggerMock = new Mock<LogHelper>();

        [TestMethod]
        public void Constructor_WhenProductRepositoryIsNull_ThrowsArgumentNullException()
        {
            Assert.ThrowsException<System.ArgumentNullException>(() =>
            new BuyUseCase(buyViewMock.Object,
                null, paymentUseCaseMock.Object, salesRepositoryMock.Object,loggerMock.Object));
        }

        [TestMethod]
        public void Constructor_WhenBuyViewIsNull_ThrowsArgumentNullException()
        {
            Assert.ThrowsException<System.ArgumentNullException>(() =>
            new BuyUseCase(null,
            unitOfWorkMock.Object, paymentUseCaseMock.Object, salesRepositoryMock.Object,loggerMock.Object));
        }

        [TestMethod]
        public void Constructor_WhenPaymentUseCaseIsNull_ThrowsArgumentNullException()
        {
            Assert.ThrowsException<System.ArgumentNullException>(() =>
            new BuyUseCase(buyViewMock.Object,
            unitOfWorkMock.Object, null, salesRepositoryMock.Object,loggerMock.Object));
        }

        [TestMethod]
        public void Constructor_WhenSalesRepositoryIsNull_ThrowsArgumentNullException()
        {
            Assert.ThrowsException<System.ArgumentNullException>(() =>
            new BuyUseCase(buyViewMock.Object,
            unitOfWorkMock.Object, paymentUseCaseMock.Object, null, loggerMock.Object));
        }

        [TestMethod]
        public void Execute_Always_CallsUpdate()
        {
            const int selectedColumn = 1;
            buyViewMock.Setup(s => s.RequestProduct()).Returns(selectedColumn);
            paymentUseCaseMock.Setup(x => x.GetPaymentMethod()).Returns(new PaymentMethod(1, "cash"));
            unitOfWorkMock.Setup(p => p.Products.GetByColumn(selectedColumn)).Returns(
             new Product
             {
                 Name = "Chocolate",
                 Price = 9,
                 Quantity = 20,
                 ColumnId = 1
             });
            Product product = new Product()
            {
                Name = "Chocolate",
                Price = 9,
                Quantity = 19,
                ColumnId = 1
            };
            BuyUseCase buyUseCase = new BuyUseCase(buyViewMock.Object,
                unitOfWorkMock.Object, paymentUseCaseMock.Object, salesRepositoryMock.Object,loggerMock.Object);

            buyUseCase.Execute();

            unitOfWorkMock.Verify(x => x.Products.Update(product), Times.Once());

        }

        [TestMethod]
        public void Execute_Always_CallsGetByColumn()
        {
            const int selectedColumn = 1;
            Product product =
                new Product
                {
                    Name = "Chocolate",
                    Price = 9,
                    Quantity = 20,
                    ColumnId = 1
                };
            unitOfWorkMock.Setup(x => x.Products.GetByColumn(selectedColumn)).Returns(product);
            paymentUseCaseMock.Setup(x => x.GetPaymentMethod()).Returns(new PaymentMethod(1, "cash"));
            buyViewMock.Setup(x => x.RequestProduct()).Returns(selectedColumn);
            var buyUseCase = new BuyUseCase(buyViewMock.Object, unitOfWorkMock.Object,
                paymentUseCaseMock.Object, salesRepositoryMock.Object, loggerMock.Object);

            buyUseCase.Execute();

            unitOfWorkMock.Verify(x => x.Products.GetByColumn(product.ColumnId), Times.Once());
        }

        [TestMethod]
        public void Execute_Always_CallsDispenseProduct()
        {
            Product product =
                new Product
                {
                    Name = "Chocolate",
                    Price = 9,
                    Quantity = 20,
                    ColumnId = 1
                };
            buyViewMock.Setup(x => x.RequestProduct()).Returns(product.ColumnId);
            unitOfWorkMock.Setup(z => z.Products.GetByColumn(product.ColumnId)).Returns(product);
            paymentUseCaseMock.Setup(x => x.GetPaymentMethod()).Returns(new PaymentMethod(1, "cash"));
            var buyUseCase = new BuyUseCase(buyViewMock.Object, unitOfWorkMock.Object,
                paymentUseCaseMock.Object, salesRepositoryMock.Object, loggerMock.Object);

            buyUseCase.Execute();

            buyViewMock.Verify(x => x.DispenseProduct(product.Name), Times.Once());
        }

        [TestMethod]
        public void Execute_Always_CallsRequestProduct()
        {
            Product product =
               new Product
               {
                   Name = "Chocolate",
                   Price = 9,
                   Quantity = 20,
                   ColumnId = 1
               };
            buyViewMock.Setup(x => x.RequestProduct()).Returns(product.ColumnId);
            unitOfWorkMock.Setup(x => x.Products.GetByColumn(product.ColumnId)).Returns(product);
            paymentUseCaseMock.Setup(x => x.GetPaymentMethod()).Returns(new PaymentMethod(1, "cash"));

            var buyUseCase = new BuyUseCase(buyViewMock.Object, unitOfWorkMock.Object,
                paymentUseCaseMock.Object, salesRepositoryMock.Object, loggerMock.Object);

            buyUseCase.Execute();

            buyViewMock.Verify(x => x.RequestProduct(), Times.Once());
        }

        [TestMethod]
        public void Execute_WhenColumnIsInvalid_ThrowsInvalidColumnException()
        {
            const int invalidColumn = 100;
            buyViewMock.Setup(prod => prod.RequestProduct()).Returns(invalidColumn);
            BuyUseCase buyUseCase = new BuyUseCase(buyViewMock.Object,
                unitOfWorkMock.Object, paymentUseCaseMock.Object, salesRepositoryMock.Object, loggerMock.Object);

            Assert.ThrowsException<InvalidColumnException>(() => buyUseCase.Execute());
        }

        [TestMethod]
        public void Execute_WhenStockIsEmptyForAProduct_ThrowsInsufficientStockException()
        {
            const int columnRequested = 4;
            unitOfWorkMock.Setup(p => p.Products.GetByColumn(columnRequested)).Returns(
                new Product
                {
                    Name = "Chocolate",
                    Price = 9,
                    Quantity = 0,
                    ColumnId = 4
                });
            buyViewMock.Setup(s => s.RequestProduct()).Returns(columnRequested);
            BuyUseCase buyUseCase = new BuyUseCase(buyViewMock.Object,
                unitOfWorkMock.Object, paymentUseCaseMock.Object, salesRepositoryMock.Object, loggerMock.Object);

            Assert.ThrowsException<InsufficientStockException>(() => buyUseCase.Execute());
        }

        [TestCleanup]
        public void CleanUp()
        {
            buyViewMock.Reset();
            authenticationServiceMock.Reset();
            unitOfWorkMock.Reset();
            paymentUseCaseMock.Reset();
            salesRepositoryMock.Reset();
        }
    }
}