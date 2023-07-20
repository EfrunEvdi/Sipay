using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TransactionFilterAPI.Data.Domain;
using TransactionFilterAPI.Data.Repository.Base;

namespace TransactionFilterAPI.Data.Repository
{
    public interface ITransactionRepository : IGenericRepository<Transaction>
    {
        IEnumerable<Transaction> GetByParameter(TransactionFilterParameters filter);
    }
}
