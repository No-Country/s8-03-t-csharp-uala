using DataAccess.Models.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace DataAccess.Models.ApplicationModels
{
    public class Account
    {
        public Guid Id { get; set; }
        public long AccountNumber { get; set; }
        public string Url_ProfilePicture { get; set; }
        public decimal Balance { get; set; }
        public decimal InvestedBalance { get; set; }
        public long CVU { get; set; }
        public string Alias { get; set; }
        public string OwnerId { get; set; }


        [ForeignKey("OwnerId")]
        public ApplicationUser Owner { get; set; }
    }
}
