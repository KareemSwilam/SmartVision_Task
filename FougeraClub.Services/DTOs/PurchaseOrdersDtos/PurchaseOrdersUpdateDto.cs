using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FougeraClub.Services.DTOs.PurchaseOrdersDtos
{
    public class PurchaseOrdersUpdateDto
    {
        public DateOnly Date { get; set; }
        public bool ApplyVAT { get; set; }
        public int SupplierId { get; set; }
    }
}
