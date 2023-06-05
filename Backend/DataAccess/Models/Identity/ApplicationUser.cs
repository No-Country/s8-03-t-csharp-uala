using DataAccess.Models.ApplicationModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public ICollection<Account> Accounts { get; set; }
        public Address Address { get; set; }

    }
}
