using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletPlusApi.Core.Data;


namespace WalletPlusApi.Infrastructure.Persistence
{
    public class WalletPlusApiContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<MoneyWallet> MoneyWallets { get; set; }
        public DbSet<PointWallet> PointWallets { get; set; }
        public DbSet<Transaction> WalletTransactions { get; set; }


        public WalletPlusApiContext(DbContextOptions options)
            : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //custom logic
            modelBuilder.Entity<Customer>().ToTable("Customers");
            modelBuilder.Entity<MoneyWallet>().ToTable("MoneyWallets");
            modelBuilder.Entity<PointWallet>().ToTable("PointWallets");
            modelBuilder.Entity<Transaction>().ToTable("WalletTransactions");


        }
    }
}
