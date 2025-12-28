using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FougeraClub.Core.Models
{
    public class PurchaseOrders
    {
        public int Id { get; set; }
        public DateOnly Date {  get; set; }
        public bool ApplyVAT { get; set; }
        public int SupplierId { get; set; }
        public Supplier supplier {  get; set; }
        public ICollection<PurchaseItems> items { get; set; }  = new HashSet<PurchaseItems>(); 
    }
}
