using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Transactions;

namespace Common.DTO.AccountDTOs
{
    public  class AccountDTO
    {
        //[JsonIgnore]
        public Guid Id { get; set; }
        public long AccountNumber { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
        public string Url_ProfilePicture { get; set; }
        public decimal Balance { get; set; }
        public decimal InvestedBalance { get; set; }
        public long CVU { get; set; }
        public string Alias { get; set; }
        public Guid OwnerId { get; set; }

        //[ForeignKey("OwnerId")]
        //public ApplicationUser Owner { get; set; }
    }
}
