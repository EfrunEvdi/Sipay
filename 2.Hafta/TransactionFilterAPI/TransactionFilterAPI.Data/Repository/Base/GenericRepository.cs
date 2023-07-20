using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TransactionFilterAPI.Base.BaseModel;
using TransactionFilterAPI.Data.DBContext;

namespace TransactionFilterAPI.Data.Repository.Base
{
    public class GenericRepository<Entity> : IGenericRepository<Entity> where Entity : BaseModel
    {
        private readonly TransactionFilterAPIDBContext _context;

        public GenericRepository(TransactionFilterAPIDBContext context)
        {
            _context = context;
        }

        public IEnumerable<Entity> Where(Expression<Func<Entity, bool>> expression)
        {
            return _context.Set<Entity>().Where(expression).ToList();
        }
    }
}
