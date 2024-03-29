﻿using Contracts.Repositories;
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


        public async Task<Account> GetAccountById(Guid id)
        {
            var account = await _context.Set<Account>()
                             .FirstAsync(t => t.OwnerId == id.ToString());

            return account;
        }

        public async Task<Account> MakeAccount(Account account)
        {
            await _context.Accounts.AddAsync(account);
            await _context.SaveChangesAsync();

            return account;
        }

        public async Task<Account> UpdateAccount(Account account)
        {
            _context.Entry(account).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return account;
        }

        
    }
}
