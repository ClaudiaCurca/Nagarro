using iQuest.VendingMachine.FileFormat;
using System;
using System.Collections.Generic;

namespace iQuest.VendingMachine.ReportRepository
{
    internal class SalesReportRepository : IReportRepository<Sales>
    {
        private readonly string name = "SalesReport";
        private readonly IFileCreator<Sales> file;

        public SalesReportRepository(IFileCreator<Sales> file)
        {
            this.file = file ?? throw new ArgumentNullException(nameof(file));
        }

        public void CreateReport(List<Sales> list)
        {
            file.Write(list, name);
        }
    }
}
