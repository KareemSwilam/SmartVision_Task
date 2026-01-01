using FougeraClub.Services.DTOs.PurchaseOrdersDtos;
using FougeraClub.Services.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FougeraClub.Services.IServices
{
    public interface IPurchaseOrdersServices
    {
        public Task<CustomResult> AddOrders(PurchaseOrdersCreateDto createDto);
        public Task<CustomResult<List<PurchaseOrdersAndSupplier>>> GetOrderAndSupplier();
        public Task<CustomResult<List<PurchaseOrdersAndSupplier>>> GetOrderAndSupplierWithFilter(PurchaseOrdersAndSupplierFilterDto dto);
        public Task<CustomResult<PurchaseOrderAndSupplierAndItems>> GetOrderAndSupplierAndItems(int id);
        public Task<CustomResult> DeleteOrder(int id);
        public Task<CustomResult> UpdateOrder(int id, PurchaseOrdersUpdateDto dto);
        
    }
}
