using iQuest.VendingMachine.FileFormat;
using iQuest.VendingMachine.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace iQuest.VendingMachine.ReportRepository
{
    internal class VolumeReportRepository : IReportRepository<SalesVolume>
    {
        private readonly string name = "VolumeReport";
        private readonly IFileCreator<SalesVolume> file;

        public VolumeReportRepository(IFileCreator<SalesVolume> file)
        {
            this.file = file ?? throw new ArgumentNullException(nameof(file));
        }
        public void CreateReport(List<SalesVolume> list)
        {
            file.Write(list, name);
        }
    }
}
