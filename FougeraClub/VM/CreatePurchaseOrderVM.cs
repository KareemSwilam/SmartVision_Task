using FougeraClub.Services.DTOs.PurchaseItemsDtos;
using FougeraClub.Services.DTOs.PurchaseOrdersDtos;

namespace FougeraClub.VM
{
    public class CreatePurchaseOrderVM
    {
        public PurchaseOrdersCreateDto Order { get; set; }
        public List<PurchaseItemCreateDto> Items { get; set; } = new List<PurchaseItemCreateDto>();
    }
}
