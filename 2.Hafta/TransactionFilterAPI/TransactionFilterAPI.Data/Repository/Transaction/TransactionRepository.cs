using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TransactionFilterAPI.Data.DBContext;
using TransactionFilterAPI.Data.Domain;
using TransactionFilterAPI.Data.Repository.Base;

namespace TransactionFilterAPI.Data.Repository
{
    public class TransactionRepository : GenericRepository<Transaction>, ITransactionRepository
    {
        private readonly TransactionFilterAPIDBContext _context;

        public TransactionRepository(TransactionFilterAPIDBContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Transaction> GetByParameter(TransactionFilterParameters filter)
        {
            Expression<Func<Transaction, bool>> expression = transaction =>
                (filter.AccountNumber == null || transaction.AccountNumber == filter.AccountNumber) && // Eğer 0 olarak girilirse hesap numarasına göre filtrelemeye dahil edilmez, aksi halde hesap numarası ile eşleşen Transactionları filtreler.
                (filter.ReferenceNumber == null || transaction.ReferenceNumber == filter.ReferenceNumber) && // Girilen referans numarasına göre Transactionları filtreler.
                (filter.MinAmountCredit == null || transaction.CreditAmount >= filter.MinAmountCredit) &&
                (filter.MaxAmountCredit == null || transaction.CreditAmount <= filter.MaxAmountCredit) && // Kredi miktarı aralığına göre Transactionları filtreler.
                (filter.MinAmountDebit == null || transaction.DebitAmount >= filter.MinAmountDebit) &&
                (filter.MaxAmountDebit == null || transaction.DebitAmount <= filter.MaxAmountDebit) && // Borç miktarı aralığına göre Transactionları filtreler.
                (filter.Description == null || transaction.Description.Contains(filter.Description)) && // Girilen metni içeren Transactionları filtreler.
                (filter.BeginDate == null || transaction.TransactionDate >= filter.BeginDate) &&
                (filter.EndDate == null || transaction.TransactionDate <= filter.EndDate); // Transaction tarihini belirtilen tarih aralığına göre filtreler.


            return Where(expression);
        }
    }
}
