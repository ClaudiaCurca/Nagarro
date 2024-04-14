using Microsoft.EntityFrameworkCore;

namespace iQuest.VendingMachine
{
    internal class VendingDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public DbSet<Sales> Sales { get; set; }

        public DbSet<Stock> Stocks { get; set; }

        public DbSet<SalesVolume> SalesVolume { get; set; }

        public VendingDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasKey(r => r.Id);
            modelBuilder.Entity<Product>().Property(r => r.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Sales>().HasKey(r => r.Id);
            modelBuilder.Entity<Sales>().Property(r => r.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<SalesVolume>().HasKey(r => r.Id);
            modelBuilder.Entity<SalesVolume>().Property(r => r.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Stock>().HasKey(r => r.Id);
            modelBuilder.Entity<Stock>().Property(r => r.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Product>().ToTable("Products");
            modelBuilder.Entity<Sales>().ToTable("Sales");
            modelBuilder.Entity<SalesVolume>().ToTable("SalesVolume");
            modelBuilder.Entity<Stock>().ToTable("Stocks");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=VendingDB.db;");
        }
    }
}
