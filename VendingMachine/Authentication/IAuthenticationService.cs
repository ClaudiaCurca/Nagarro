namespace iQuest.VendingMachine.Authentication
{
    internal interface IAuthenticationService
    {
        bool IsUserAuthenticated { get; }
        public void Login(string password);
        public void Logout();
    }
}
