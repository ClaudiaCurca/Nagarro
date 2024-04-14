using System;

namespace iQuest.VendingMachine.Exceptions
{
    [Serializable]
    internal class CancelException : Exception
    {
        public CancelException() : base(String.Format("Your action was cancelled")) { }
    }
}
