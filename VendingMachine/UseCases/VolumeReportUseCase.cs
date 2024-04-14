using iQuest.VendingMachine.DataAccessLayer;
using iQuest.VendingMachine.Interfaces;
using iQuest.VendingMachine.ReportRepository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace iQuest.VendingMachine.UseCases
{
    internal class VolumeReportUseCase : IUseCase
    {
        private readonly VolumeReportRepository volumeReportRepository;
        private readonly IVolumeReportView volumeReportView;
        private readonly ISalesRepository salesRepository;
        private readonly LogHelper logger;

        public VolumeReportUseCase(VolumeReportRepository volumeReportRepository, IVolumeReportView volumeReportView, ISalesRepository salesRepository, LogHelper logger)
        {
            this.volumeReportRepository = volumeReportRepository ?? throw new ArgumentNullException(nameof(volumeReportRepository));
            this.volumeReportView = volumeReportView ?? throw new ArgumentNullException(nameof(volumeReportView));
            this.salesRepository = salesRepository ?? throw new ArgumentNullException(nameof(salesRepository));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void Execute()
        {
            List<SalesVolume> list = new List<SalesVolume>();
            volumeReportView.DisplayInfo();
            DateTime startDateTime = volumeReportView.RequestStartTime();
            DateTime endDateTime = volumeReportView.RequestEndTime();

            foreach (Sales sale in salesRepository.GetByDate(startDateTime, endDateTime))
            {
                SalesVolume salesVolume = list.FirstOrDefault(x => x.Name == sale.Name);
                if (salesVolume == null)
                {
                    list.Add(new SalesVolume
                    {
                        Name = sale.Name,
                        Quantity = 1
                    });
                }
                else
                {
                    salesVolume.Quantity++;
                }

            }
            logger.Info("VolumeReport UseCase");
            volumeReportRepository.CreateReport(list);
        }
    }
}
