using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DataAccess.Models.Identity;
using DataAccess.Models.ApplicationModels;

namespace DataAccess
{
    public class UalaContext : IdentityDbContext<IdentityUser>
    {
        public UalaContext(DbContextOptions<UalaContext> options) : base(options)
        {
        }
        public DbSet<ApplicationUser> Clients { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}
