using iQuest.VendingMachine;
using iQuest.VendingMachine.Authentication;
using iQuest.VendingMachine.Exceptions;
using iQuest.VendingMachine.Interfaces;
using iQuest.VendingMachine.Payment;
using iQuest.VendingMachine.PaymentMethods;
using iQuest.VendingMachine.PresentationLayer;
using iQuest.VendingMachine.UseCases;
using Moq;

namespace VendingMachineTests
{
    [TestClass]
    public class PaymentUseCaseTests
    {
        private Mock<IBuyView> buyViewMock = new Mock<IBuyView>();
        private Mock<IAuthenticationService> authenticationServiceMock = new Mock<IAuthenticationService>();

        private IEnumerable<PaymentMethod> paymentMethods = new List<PaymentMethod>
        {
            new PaymentMethod(1,"cash"),
            new PaymentMethod(2,"card")
        };

        private IEnumerable<IPaymentAlgorithm> paymentAlgorithms = new List<IPaymentAlgorithm>
        {
            new CashPayment(new CashPaymentTerminal(),1),
            new CardPayment(new CardPaymentTerminal(),2)
        };

        [TestMethod]
        public void Name_WhenInitializingTheUseCase_NameIsCorrect()
        {
            const string expectedName = "pay";

            PaymentUseCase paymentUseCase = new PaymentUseCase(buyViewMock.Object, paymentAlgorithms, paymentMethods,Mock.Of<LogHelper>());

            Assert.AreEqual(expectedName, paymentUseCase.Name);
        }

        [TestMethod]
        public void Description_WhenInitializingTheUseCase_DescriptionIsCorrect()
        {
            const string expectedDescription = "pay product";

            PaymentUseCase paymentUseCase = new PaymentUseCase(buyViewMock.Object, paymentAlgorithms, paymentMethods, Mock.Of<LogHelper>());

            Assert.AreEqual(expectedDescription, paymentUseCase.Description);
        }

        [TestMethod]
        public void CanExecute_WhenUserIsAuthenticated_ReturnsTrue()
        {
            authenticationServiceMock.Setup(x => x.IsUserAuthenticated).Returns(true);
            PaymentUseCase paymentUseCase = new PaymentUseCase(buyViewMock.Object, paymentAlgorithms, paymentMethods, Mock.Of<LogHelper>());

            Assert.AreEqual(true, paymentUseCase.CanExecute);
        }

        [TestMethod]
        public void Constructor_WhenBuyViewIsNull_ThrowsArgumentNullException()
        {
            Assert.ThrowsException<System.ArgumentNullException>(() =>
            new PaymentUseCase(null, paymentAlgorithms, paymentMethods, Mock.Of<LogHelper>()));
        }

        [TestMethod]
        public void Constructor_WhenPaymentAlgorithmIsNull_ThrowsArgumentNullException()
        {
            Assert.ThrowsException<System.ArgumentNullException>(() =>
            new PaymentUseCase(buyViewMock.Object, null, paymentMethods, Mock.Of<LogHelper>()));
        }

        [TestMethod]
        public void Constructor_WhenPaymentMethodsIsNull_ThrowsArgumentNullException()
        {
            Assert.ThrowsException<System.ArgumentNullException>(() =>
            new PaymentUseCase(buyViewMock.Object, paymentAlgorithms, null, Mock.Of<LogHelper>()));
        }

        [TestMethod]
        public void Execute_CallsAskForPaymentMethod_ReturnsValidPaymentCode()
        {
            const decimal price = 10;
            const int validPaymentCode = 1;
            List<PaymentMethod> paymentMethodsList = paymentMethods.ToList();
            Mock<ICashPaymentTerminal> cashpaymentTerminal = new Mock<ICashPaymentTerminal>();
            cashpaymentTerminal.Setup(x => x.AskForMoney()).Returns(price);
            Mock<CashPayment> cashpayment = new Mock<CashPayment>(cashpaymentTerminal.Object,1);
            

            IEnumerable<IPaymentAlgorithm> paymentAlgorithms = new List<IPaymentAlgorithm>
            {
                cashpayment.Object
            };

        buyViewMock.Setup(x => x.AskForPaymentMethod(paymentMethodsList)).Returns(validPaymentCode);
            var paymentUseCase = new PaymentUseCase(buyViewMock.Object, paymentAlgorithms, paymentMethodsList, Mock.Of<LogHelper>());

            paymentUseCase.Execute(price);

            buyViewMock.Verify(x => x.AskForPaymentMethod(paymentMethodsList), Times.Once());
        }

        [TestMethod]
        public void Execute_WhenPaymentCodeIsInvalid_ThrowsCancelException()
        {
            const int invalidPaymentCode = 100;
            const int price = 10;

            buyViewMock.Setup(x => x.AskForPaymentMethod(paymentMethods.ToList())).Returns(invalidPaymentCode);
            var paymentUseCase = new PaymentUseCase(buyViewMock.Object, paymentAlgorithms, paymentMethods, Mock.Of<LogHelper>());

            Assert.ThrowsException<CancelException>(() => paymentUseCase.Execute(price));
        }

        [TestCleanup]
        public void CleanUp()
        {
            authenticationServiceMock.Reset();
            buyViewMock.Reset();
        }
    }
}
