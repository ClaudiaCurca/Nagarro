using iQuest.VendingMachine.Exceptions;
using iQuest.VendingMachine.Interfaces;
using iQuest.VendingMachine.Payment;
using System;
using System.Collections.Generic;
using System.Linq;

namespace iQuest.VendingMachine.UseCases
{
    internal class PaymentUseCase : IPaymentUseCase
    {
        private readonly IBuyView buyView;

        private readonly IEnumerable<IPaymentAlgorithm> paymentAlgorithms;

        private readonly IEnumerable<PaymentMethod> paymentMethods;

        private PaymentMethod UsedPaymentMethod;

        private readonly LogHelper logger;

        public string Name => "pay";

        public string Description => "pay product";

        public bool CanExecute => true;

        public PaymentUseCase(IBuyView buyView, IEnumerable<IPaymentAlgorithm> paymentAlgorithms, IEnumerable<PaymentMethod> paymentMethods, LogHelper logger)
        {
            this.buyView = buyView ?? throw new ArgumentNullException(nameof(buyView));
            this.paymentAlgorithms = paymentAlgorithms ?? throw new ArgumentNullException(nameof(paymentAlgorithms));
            this.paymentMethods = paymentMethods ?? throw new ArgumentNullException(nameof(paymentMethods));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void Execute(decimal price)
        {
            logger.Info("Payment UseCase");
            int? paymentCode = buyView.AskForPaymentMethod(paymentMethods.ToList());
            foreach (IPaymentAlgorithm paymentAlgorithm in paymentAlgorithms)
            {
                if (paymentCode == paymentAlgorithm.PaymentMethod)
                {
                    paymentAlgorithm.Run(price);
                    UsedPaymentMethod = paymentMethods.FirstOrDefault((method) => method.Id == paymentCode);
                    return;
                }
            }
            throw new CancelException();
        }
        public PaymentMethod GetPaymentMethod()
        {
            return UsedPaymentMethod;
        }

    }
}
