using Autofac;
using iQuest.VendingMachine.Interfaces;

namespace iQuest.VendingMachine
{
    internal class UseCaseFactory : IUseCaseFactory
    {
        public T Create<T>() where T : IUseCase
        {
            var container = ContainerConfig.GetContainer();
            using (var scope = container.BeginLifetimeScope())
            {
                return scope.Resolve<T>();
            }
        }
    }
}
