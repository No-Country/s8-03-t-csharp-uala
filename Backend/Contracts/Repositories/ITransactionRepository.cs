using Common.DTO;
using DataAccess.Models.ApplicationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Repositories
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<Transaction>> GetAllTransactionsByAccountId(Guid accountId);

        Task<Transaction> GetTransactionById(Guid id);

        Task<Transaction> MakeTransaction(Transaction transaction);
    }
}
