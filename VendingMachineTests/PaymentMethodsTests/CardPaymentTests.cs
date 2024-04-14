using iQuest.VendingMachine.Exceptions;
using iQuest.VendingMachine.Interfaces;
using iQuest.VendingMachine.Payment;
using Moq;

namespace VendingMachineTests
{
    [TestClass]
    public class CardPaymentTests
    {
        private Mock<ICardPaymentTerminal> cardPaymentTerminalMock = new Mock<ICardPaymentTerminal>();

        [TestMethod]
        public void Run_CallsAskForCardNumber_ReturnsValidCardNumber()
        {
            const decimal price = 10;
            const string cardNumber = "79927398713";
            const int cardPamentMethod = 2;

            cardPaymentTerminalMock.Setup(v => v.AskForCardNumber()).Returns(cardNumber);
            var cardPayment = new CardPayment(cardPaymentTerminalMock.Object, cardPamentMethod);

            cardPayment.Run(price);

            cardPaymentTerminalMock.Verify(x => x.AskForCardNumber(), Times.Once());
        }

        [TestMethod]
        public void Run_CallsAskForCardNumber_ThrowsCancelException()
        {
            const decimal price = 10;
            const string invalidCardNumber = "12311";
            const int cardPamentMethod = 2;

            cardPaymentTerminalMock.Setup(v => v.AskForCardNumber()).Returns(invalidCardNumber);
            var cardPayment = new CardPayment(cardPaymentTerminalMock.Object, cardPamentMethod);

            Assert.ThrowsException<CancelException>(() => cardPayment.Run(price));
        }

        [TestCleanup]
        public void CleanUp()
        {
            cardPaymentTerminalMock.Reset();
        }
    }
}
