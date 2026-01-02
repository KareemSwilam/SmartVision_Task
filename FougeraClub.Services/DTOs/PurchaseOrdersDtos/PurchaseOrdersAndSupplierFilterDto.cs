using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FougeraClub.Services.DTOs.PurchaseOrdersDtos
{
    public class PurchaseOrdersAndSupplierFilterDto
    {
        public DateOnly? fromDate {  get; set; } 
        public DateOnly? toDate { get; set; }
        public string? VaTNumber { get; set; }    
    }
}
