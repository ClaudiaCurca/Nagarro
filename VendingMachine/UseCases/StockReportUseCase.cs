using iQuest.VendingMachine.Interfaces;
using iQuest.VendingMachine.ReportRepository;
using System;
using System.Collections.Generic;

namespace iQuest.VendingMachine.UseCases
{
    internal class StockReportUseCase : IUseCase
    {
        private readonly StockReportRepository stockReportRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly LogHelper logger;

        public StockReportUseCase(StockReportRepository stockReportRepository, IUnitOfWork unitOfWork, LogHelper logger)
        {
            this.stockReportRepository = stockReportRepository ?? throw new ArgumentNullException(nameof(stockReportRepository));
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public void Execute()
        {
            List<Stock> stockList = new List<Stock>();
            foreach (Product product in unitOfWork.Products.GetAll())
            {
                stockList.Add(new Stock
                {
                    Name = product.Name,
                    Quantity = product.Quantity
                });
            }
            logger.Info("StockReport UseCase");
            stockReportRepository.CreateReport(stockList);
        }
    }
}
