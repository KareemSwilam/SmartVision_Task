using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FougeraClub.Core.Models
{
    public  class PurchaseItems
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public double PricePerUnit { get; set; }
        public double TotalInLine { get; set; }
        public string Description { get; set; }
        public int  PurchaseOrdersId { get; set; } 
        public int InvoiceId { get; set; }
        public PurchaseOrders order { get; set; }  
        public Invoice invoice { get; set; }    
    }
}
