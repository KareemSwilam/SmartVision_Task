using FougeraClub.Services.DTOs.PurchaseItemsDtos;
using FougeraClub.Services.DTOs.PurchaseOrdersDtos;

namespace FougeraClub.VM
{
    public class UpdatePurchaseOrderVm
    {
        public PurchaseOrdersUpdateDto Order { get; set; }
        public List<PurchaseItemUpdateDto> Items { get; set; } = new List<PurchaseItemUpdateDto>();
    }
}
