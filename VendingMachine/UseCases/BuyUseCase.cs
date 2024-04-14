using iQuest.VendingMachine.DataAccessLayer;
using iQuest.VendingMachine.Exceptions;
using iQuest.VendingMachine.Interfaces;
using System;

namespace iQuest.VendingMachine.UseCases
{
    internal class BuyUseCase : IUseCase
    {
        private readonly IBuyView buyView;
        private readonly IUnitOfWork unitOfWork;
        private readonly IPaymentUseCase paymentUseCase;
        private readonly ISalesRepository salesRepository;
        private readonly LogHelper logger;

        public BuyUseCase(IBuyView buyView, IUnitOfWork unitOfWork, IPaymentUseCase paymentUseCase, ISalesRepository salesRepository, LogHelper logger)
        {
            this.buyView = buyView ?? throw new ArgumentNullException(nameof(buyView));
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            this.paymentUseCase = paymentUseCase ?? throw new ArgumentNullException(nameof(paymentUseCase));
            this.salesRepository = salesRepository ?? throw new ArgumentNullException(nameof(salesRepository));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void Execute()
        {
            logger.Info("Buy UseCase");
            int selectedColumn = buyView.RequestProduct();
            Product product = unitOfWork.Products.GetByColumn(selectedColumn);

            if (product == null)
            {
                logger.Error("InvalidColumn");
                throw new InvalidColumnException(selectedColumn);
            }
            if (product.Quantity <= 0)
            {
                logger.Error("InsufficientStock");
                throw new InsufficientStockException(product.Name);
            }
            paymentUseCase.Execute(product.Price);
            buyView.DispenseProduct(product.Name);
            Product productCopy = (Product)product;
            productCopy.Quantity = product.Quantity - 1;
            unitOfWork.Products.Update(productCopy);
            unitOfWork.Save();
            salesRepository.Add(new Sales
            {
                Date = DateTime.Now,
                Name = product.Name,
                Price = product.Price,
                PaymentMethod = paymentUseCase.GetPaymentMethod().Name
            });
        }
    }
}
