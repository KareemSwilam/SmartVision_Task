using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FougeraClub.Services.DTOs.PurchaseOrdersDtos
{
    public class PurchaseOrdersCreateDto
    {
        public DateOnly Date { get; set; }
        public bool ApplyVAT { get; set; }
        public string VATNumber { get; set; }
    }
}
