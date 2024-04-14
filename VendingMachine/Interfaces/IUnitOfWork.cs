using System;

namespace iQuest.VendingMachine.Interfaces
{
    internal interface IUnitOfWork:IDisposable
    {
        IProductRepository Products { get; }
        int Save();
    }
}
