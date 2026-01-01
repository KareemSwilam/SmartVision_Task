using FougeraClub.Services.DTOs.PurchaseItemsDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FougeraClub.Services.DTOs.PurchaseOrdersDtos
{
    public class PurchaseOrderAndSupplierAndItems
    {
        public int Id { get; set; }
        public string SupplierName { get; set; }
        public DateOnly Date { get; set; }
        public List<PurchaseItemsDto> items { get; set; }
    }
}
