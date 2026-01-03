using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FougeraClub.Services.DTOs.InvoiceDtos
{
    public class InvoiceCreateDto
    {
        public int PurchaseOrderId { get; set; }
        public double VATRate { get; set; } = 0;
    }
}
