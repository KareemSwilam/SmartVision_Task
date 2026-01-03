using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FougeraClub.Services.DTOs.InvoiceDtos
{
    public class InvoiceDto
    {
        public int Id { get; set; }
        public int PurchaseOrderId { get; set; }
        public double SubTotal { get; set; }
        public double VATAmount { get; set; }
        public double VATRate { get; set; }
        public double TotalAmount { get; set; }
        public bool IsAsign { get; set; }
    }
}
