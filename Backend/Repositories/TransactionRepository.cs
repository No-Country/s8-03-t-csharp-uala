using Common.DTO.TransactionDTOs;
using Contracts.Repositories;
using DataAccess;
using DataAccess.Models.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly UalaContext _context;
        
        public TransactionRepository (UalaContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Transaction>> GetAllTransactionsByAccountId(Guid accountId)
        {
            var transactions = await _context.Transactions
                .Where(t => t.SourceAccountId == accountId || t.DestinationAccountId == accountId)
                .ToListAsync();

            return transactions;
        }


        public async Task<Transaction> GetTransactionById(Guid id)
        {
            var transaction = await _context.Set<Transaction>()
                             .SingleOrDefaultAsync(t => t.Id == id);

            return transaction;
        }

        public async Task<Transaction> MakeTransaction (Transaction transaction)
        {

            await _context.Transactions.AddAsync(transaction);

            return transaction;
           
        }
      
    }
}
