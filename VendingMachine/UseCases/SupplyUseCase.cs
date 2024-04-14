using iQuest.VendingMachine.Exceptions;
using iQuest.VendingMachine.Interfaces;
using System;

namespace iQuest.VendingMachine.UseCases
{
    internal class SupplyUseCase : IUseCase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ISupplyView supplyView;
        private readonly LogHelper logger;

        public SupplyUseCase(ISupplyView supplyView, IUnitOfWork unitOfWork, LogHelper logger)
        {
            this.supplyView = supplyView ?? throw new ArgumentNullException(nameof(supplyView));
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void Execute()
        {
            Product newProduct = supplyView.RequestProduct();
            if (newProduct.ColumnId <= 0)
            {
                logger.Error("InvalidColumn");
                throw new InvalidColumnException(newProduct.ColumnId);
            }
            logger.Info("Supply UseCase");
            if (unitOfWork.Products.GetByColumn(newProduct.ColumnId) == null)
            {
                unitOfWork.Products.Add(newProduct);
                unitOfWork.Save();
            }
            else
            {
                unitOfWork.Products.Update(newProduct);
                unitOfWork.Save();
            }
        }
    }
}
