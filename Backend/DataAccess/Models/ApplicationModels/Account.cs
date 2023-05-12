using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace DataAccess.Models.ApplicationModels
{
    public class Account
    {
        public Guid Id { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
    }
}
