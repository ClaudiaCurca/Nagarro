using System.Collections.Generic;

namespace iQuest.VendingMachine.FileFormat
{
    internal interface IFileCreator<T>
    {
        public void Write(List<T> sale, string name);
    }
}
