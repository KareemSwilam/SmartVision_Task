using FougeraClub.Services.DTOs.PurchaseItemsDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FougeraClub.Services.DTOs.InvoiceDtos
{
    public class InvoiceWithItemsWithSupplierNameDto
    {
        public int PurchaseOrderId { get; set; }
        public double SubTotal { get; set; }
        public double VATAmount { get; set; }
        public double VATRate { get; set; }
        public double TotalAmount { get; set; }
        public bool IsAsign { get; set; }
        public string AmountInWord { get; set; }
        public string SupplierName { get; set; }    
        public List<PurchaseItemsDto> items { get; set; }
    }
}
