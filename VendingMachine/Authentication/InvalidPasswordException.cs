using System;

namespace iQuest.VendingMachine.Authentication
{
    internal class InvalidPasswordException : Exception
    {
        private const string DefaultMessage = "Invalid password";

        public InvalidPasswordException()
            : base(String.Format(DefaultMessage)) { }
    }
}