using System.Runtime.CompilerServices;

namespace iQuest.VendingMachine
{
    public class LogHelper
    {
        public void Error(string message, [CallerFilePath] string fileName = "")
        {
            GetLogger(fileName).Error(message);
        }

        public void Info(string message, [CallerFilePath] string fileName = "")
        {
            GetLogger(fileName).Info(message);
        }

        private static log4net.ILog GetLogger(string fileName)
        {
            return log4net.LogManager.GetLogger(fileName);
        }
    }

}
