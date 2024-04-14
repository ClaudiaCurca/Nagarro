using iQuest.VendingMachine.DataAccessLayer;
using iQuest.VendingMachine.Interfaces;

namespace iQuest.VendingMachine
{
    internal class UnitOfWork : IUnitOfWork
    {
        VendingDbContext db;

        public UnitOfWork(VendingDbContext db)
        {
            this.db = db;
            Products = new EntityFrameworkRepository(db);
        }

        public IProductRepository Products { get; private set; }

        public void Dispose()
        {
            db.Dispose();
        }

        public int Save()
        {
            return db.SaveChanges();
        }
    }
}
