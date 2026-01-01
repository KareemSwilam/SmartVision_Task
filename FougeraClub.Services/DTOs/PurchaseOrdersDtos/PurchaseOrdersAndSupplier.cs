using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FougeraClub.Services.DTOs.PurchaseOrdersDtos
{
    public class PurchaseOrdersAndSupplier
    {
        public int Id { get; set; }
        public string SupplierName { get; set; }
        public DateOnly Date { get; set; }
    }
}
