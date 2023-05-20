using Contracts.Repositories;
using DataAccess;
using DataAccess.Models.ApplicationModels;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UalaContext _context;
        
        public AccountRepository(UalaContext context)
        {
            _context = context;
        }


        //public async Task<IEnumerable<Account>> GetAllTransactionsByAccountId(Guid accountId)
        //{
        //    var transactions = await _context.Transactions
        //        .Where(t => t.SourceAccountId == accountId || t.DestinationAccountId == accountId)
        //        .ToListAsync();

        //    return transactions;
        //}


        public async Task<Account> GetAccountById(Guid id)
        {
            var account = await _context.Set<Account>()
                             .SingleOrDefaultAsync(t => t.Id == id);

            return account;
        }

        public async Task<Account> MakeAccount(Account account)
        {
            await _context.Accounts.AddAsync(account);
            await _context.SaveChangesAsync();

            return account;
        }

        public  Task UpdateAccount(Account account)
        {
            _context.Accounts.Update(account);
            return Task.CompletedTask;
        }
    }
}
