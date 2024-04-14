using iQuest.VendingMachine.Exceptions;
using iQuest.VendingMachine.Interfaces;
using iQuest.VendingMachine.PaymentMethods;
using Moq;

namespace VendingMachineTests
{
    [TestClass]
    public class CashPaymentTests
    {
        private readonly Mock<ICashPaymentTerminal> cashPaymentTerminalMock = new Mock<ICashPaymentTerminal>();

        [TestMethod]
        public void Run_CallsAskForMoney_ReturnsMoney()
        {
            const decimal price = 10;
            const int cashPaymentMethod = 1;

            cashPaymentTerminalMock.Setup(v => v.AskForMoney()).Returns(20);
            var cashPayment = new CashPayment(cashPaymentTerminalMock.Object, cashPaymentMethod);

            cashPayment.Run(price);

            cashPaymentTerminalMock.Verify(x => x.AskForMoney(), Times.Once());
        }

        [TestMethod]
        public void Run_ThrowsCancelException()
        {
            const decimal price = 10;
            const decimal invalidMoney = -1;
            const int cashPaymentMethod = 1;

            cashPaymentTerminalMock.Setup(v => v.AskForMoney()).Returns(invalidMoney);
            var cashPayment = new CashPayment(cashPaymentTerminalMock.Object, cashPaymentMethod);

            Assert.ThrowsException<CancelException>(() => cashPayment.Run(price));
        }

        [TestMethod]
        public void Run_CallsGiveBackChange_ReturnsChange()
        {
            const decimal price = 10;
            const decimal money = 20;
            const decimal change = money - price;
            const int cashPaymentMethod = 1;

            cashPaymentTerminalMock.Setup(v => v.AskForMoney()).Returns(money);
            var cashPayment = new CashPayment(cashPaymentTerminalMock.Object, cashPaymentMethod);

            cashPayment.Run(price);

            cashPaymentTerminalMock.Verify(x => x.GiveBackChange(change), Times.Once());
        }

        [TestCleanup]
        public void CleanUp()
        {
            cashPaymentTerminalMock.Reset();
        }
    }
}
