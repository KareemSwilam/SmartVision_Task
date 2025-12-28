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
        public string Description { get; set; }
        public int  PurchaseOrdersId { get; set; } 
        public PurchaseOrders order { get; set; }  
    }
}
