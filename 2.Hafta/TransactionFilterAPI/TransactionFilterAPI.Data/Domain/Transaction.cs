using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionFilterAPI.Base.BaseModel;

namespace TransactionFilterAPI.Data.Domain
{
    public class Transaction : BaseModel
    {
        public decimal CreditAmount { get; set; }
        public decimal DebitAmount { get; set; }
        public string Description { get; set; }
        public DateTime TransactionDate { get; set; }
        public string ReferenceNumber { get; set; }

        public int AccountNumber { get; set; }
        public virtual Account Account { get; set; }
    }

    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            // Base Model
            builder.Property(x => x.ID).IsRequired(true).UseIdentityColumn();
            builder.Property(x => x.InsertUser).IsRequired(true).HasMaxLength(50);
            builder.Property(x => x.InsertDate).IsRequired(true);

            builder.Property(x => x.CreditAmount).IsRequired(true).HasPrecision(15, 4).HasDefaultValue(0);
            builder.Property(x => x.DebitAmount).IsRequired(true).HasPrecision(15, 4).HasMaxLength(0);
            builder.Property(x => x.Description).IsRequired(true).HasMaxLength(250);
            builder.Property(x => x.TransactionDate).IsRequired(true);

            builder.Property(x => x.ReferenceNumber).IsRequired(true).HasMaxLength(50);
            builder.Property(x => x.ReferenceNumber).IsRequired(true);
            builder.HasIndex(x => x.ReferenceNumber);

            builder.Property(x => x.AccountNumber).IsRequired(true);
            builder.HasIndex(x => x.AccountNumber);
        }
    }
}