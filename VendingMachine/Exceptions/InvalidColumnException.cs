using System;

namespace iQuest.VendingMachine.Exceptions
{
    [Serializable]
    internal class InvalidColumnException : Exception
    {
        private const string message = "The selected column is: {0} and it is invalid! Please try again!";

        public InvalidColumnException(int columnId) : base(string.Format(message, columnId)) { }
    }
}
