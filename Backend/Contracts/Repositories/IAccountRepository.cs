using DataAccess.Models.ApplicationModels;

namespace Contracts.Repositories
{
    public interface IAccountRepository
    {
        //Task<IEnumerable<Transaction>> GetAccountById(Guid accountId);

        Task<Account> GetAccountById(Guid id);
        Task<Account> UpdateAccount(Account account); 
        Task<Account> MakeAccount(Account account);
    }
}
