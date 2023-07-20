using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionFilterAPI.Data.Domain;

namespace TransactionFilterAPI.Data.DBContext
{
    public class TransactionFilterAPIDBContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = EFRUN\\SQLEXPRESS; Database = TransactionFilterAPIDB; Integrated Security = true;");
        }

        //public TransactionFilterAPIDBContext(DbContextOptions<TransactionFilterAPIDBContext> options) : base(options)
        //{

        //}

        // DbSet
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            modelBuilder.ApplyConfiguration(new AccountConfiguration());
            modelBuilder.ApplyConfiguration(new TransactionConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
