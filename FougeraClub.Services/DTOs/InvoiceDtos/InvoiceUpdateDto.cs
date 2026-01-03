using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FougeraClub.Services.DTOs.InvoiceDtos
{
    public class InvoiceUpdateDto
    {
        public double SubTotal { get; set; }
        public double VATRate { get; set; } 
        public bool IsAsign { get; set; } = false;
    }
}
