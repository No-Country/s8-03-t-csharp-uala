using DataAccess.Models.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DataAccess.Models.ApplicationModels
{
    public class Address
    {
        public int Id { get; set; }
        public string Province { get; set; }    
        public string City { get; set; }
        public string Road { get; set; }
        public string ZipCode { get; set; }
    }
}
