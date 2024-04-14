using iQuest.VendingMachine.DataAccessLayer;
using iQuest.VendingMachine.Interfaces;
using iQuest.VendingMachine.ReportRepository;
using System;

namespace iQuest.VendingMachine.UseCases
{
    internal class SalesReportUseCase : IUseCase
    {
        SalesReportRepository salesReportRepository;
        private readonly ISalesRepository salesRepository;
        private readonly LogHelper logger;

        public SalesReportUseCase(SalesReportRepository salesReportRepository,ISalesRepository salesRepository, LogHelper logger)
        {
            this.salesReportRepository = salesReportRepository ?? throw new ArgumentNullException(nameof(salesReportRepository));
            this.salesRepository = salesRepository ?? throw new ArgumentNullException(nameof(salesRepository));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public void Execute()
        {
            logger.Info("SalesReport UseCase");
            salesReportRepository.CreateReport(salesRepository.GetAll());
        }
    }
}
