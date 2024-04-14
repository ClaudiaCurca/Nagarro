using System;

namespace iQuest.VendingMachine.Interfaces
{
    internal interface IVolumeReportView
    {
        public void DisplayInfo();

        public DateTime RequestStartTime();

        public DateTime RequestEndTime();
    }
}
