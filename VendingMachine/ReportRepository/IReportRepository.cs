using System.Collections.Generic;

namespace iQuest.VendingMachine.ReportRepository
{
    internal interface IReportRepository<T>
    {
        void CreateReport(List<T> list);
    }
}