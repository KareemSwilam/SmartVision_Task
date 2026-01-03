using FougeraClub.Core.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FougeraClub.Core.Models
{
    public class Supplier : ISoftDeletable
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Email { get; set; }   
        public string PhoneNumber { get; set; }
        public string VATNumber { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get ; set; }
        
        public ICollection<PurchaseOrders> purchases { get; set; } = new List<PurchaseOrders>();
        
    }
}
