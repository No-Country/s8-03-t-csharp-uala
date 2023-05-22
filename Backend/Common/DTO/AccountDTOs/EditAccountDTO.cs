using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO.AccountDTOs
{
    public class EditAccountDTO
    {
        public long AccountNumber { get; set; }
        public string Url_ProfilePicture { get; set; }
        public decimal Balance { get; set; }
        public decimal InvestedBalance { get; set; }
        public long CVU { get; set; }
        public string Alias { get; set; }
        public string OwnerId { get; set; }
    }
}
