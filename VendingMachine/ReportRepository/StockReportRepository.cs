using iQuest.VendingMachine.FileFormat;
using iQuest.VendingMachine.Interfaces;
using System;
using System.Collections.Generic;

namespace iQuest.VendingMachine.ReportRepository
{
    internal class StockReportRepository:IReportRepository<Stock>
    {
        private readonly string name = "StockReport";
        private readonly IFileCreator<Stock> file;

        public StockReportRepository(IFileCreator<Stock> file)
        {
            this.file = file ?? throw new ArgumentNullException(nameof(file));
        }

        public void CreateReport(List<Stock> list)
        {
            file.Write(list, name);
        }
    }
}
