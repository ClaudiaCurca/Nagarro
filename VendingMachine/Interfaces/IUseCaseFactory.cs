namespace iQuest.VendingMachine.Interfaces
{
    internal interface IUseCaseFactory
    {
        T Create<T>() where T : IUseCase;
    }
}
