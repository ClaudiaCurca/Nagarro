using iQuest.VendingMachine.Interfaces;
using System;

namespace iQuest.VendingMachine.PresentationLayer.Views
{
    internal class VolumeReportView: DisplayBase, IVolumeReportView
    {
        public void DisplayInfo()
        {
            DisplayLine("The date format is yyyy/MM/dd", ConsoleColor.Cyan);
        }
        public DateTime RequestStartTime()
        {
            Display("Enter a Start Time: ", ConsoleColor.Cyan);
            DateTime userDateTime;
            DateTime.TryParse(Console.ReadLine(), out userDateTime);
            return userDateTime;
        }
        public DateTime RequestEndTime()
        {
            Display("Enter a End Time: ", ConsoleColor.Cyan);
            DateTime userDateTime;
            DateTime.TryParse(Console.ReadLine(), out userDateTime);
            return userDateTime;
        }
    }
}
