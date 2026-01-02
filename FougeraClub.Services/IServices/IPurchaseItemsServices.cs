using FougeraClub.Services.DTOs.PurchaseItemsDtos;
using FougeraClub.Services.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FougeraClub.Services.IServices
{
    public interface IPurchaseItemsServices
    {
        public Task<CustomResult> AddItem(int OrderId, PurchaseItemCreateDto dto);
        public Task<CustomResult> UpdateItem( PurchaseItemUpdateDto dto);
        public Task<CustomResult> DeleteItem(int OrderId, int ItemId);
        public Task<CustomResult<List<PurchaseItemsDto>>> GetOrderItems(int OrderId);  
    }
}
