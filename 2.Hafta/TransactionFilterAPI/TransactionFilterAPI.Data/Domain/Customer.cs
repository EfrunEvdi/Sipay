using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionFilterAPI.Base.BaseModel;

namespace TransactionFilterAPI.Data.Domain
{
    public class Customer : BaseModel
    {
        public int CustomerNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }

        public virtual List<Account> Accounts { get; set; }
    }

    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            // Base Model
            builder.Property(x => x.InsertUser).IsRequired(true).HasMaxLength(50);
            builder.Property(x => x.InsertDate).IsRequired(true);

            builder.Property(x => x.CustomerNumber).IsRequired(true).ValueGeneratedNever();
            builder.HasIndex(x => x.CustomerNumber).IsUnique(true);
            builder.HasKey(x => x.CustomerNumber);

            builder.Property(x => x.FirstName).IsRequired(true).HasMaxLength(50);
            builder.Property(x => x.LastName).IsRequired(true).HasMaxLength(50);
            builder.Property(x => x.Address).IsRequired(true).HasMaxLength(50);
            builder.Property(x => x.IsActive).IsRequired(true).HasDefaultValue(true);

            builder.HasIndex(x => x.CustomerNumber).IsUnique(true);
            builder.HasMany(x => x.Accounts)
                   .WithOne(x => x.Customer)
                   .HasForeignKey(x => x.CustomerNumber)
                   .IsRequired();
        }
    }
}
