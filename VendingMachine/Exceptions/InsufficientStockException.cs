using System;

namespace iQuest.VendingMachine.Exceptions
{
    [Serializable]
    internal class InsufficientStockException : Exception
    {
        private const string message = "Sorry, quantity unavailable for {0}. Please try again later! ";

        public InsufficientStockException(string productName) : base(String.Format(message, productName)) { }
    }
}
