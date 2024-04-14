using System;

namespace iQuest.VendingMachine.Exceptions
{
    [Serializable]
    internal class DataBaseConnectionException : Exception
    {
        public DataBaseConnectionException() : base(String.Format("Can not open connection ! ")) { }
    }
}
