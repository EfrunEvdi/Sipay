using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransactionFilterAPI.Base.BaseModel
{
    public class BaseModel
    {
        public int ID { get; set; }
        public DateTime InsertDate { get; set; }
        public string InsertUser { get; set; }
    }
}
