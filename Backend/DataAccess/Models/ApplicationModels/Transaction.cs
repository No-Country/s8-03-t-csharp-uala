using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.ApplicationModels
{
    public class Transaction
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Motive { get; set; }  // alquiler, prestamo, honorarios, pagoServicios , otro
        public string Reference { get; set; }

        
        public Guid SourceAccountId { get; set; }        
        public Guid DestinationAccountId { get; set; }        
    }
}
