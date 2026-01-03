using FougeraClub.Core.IRespository;
using FougeraClub.Core.Models;
using FougeraClub.Services.DTOs.PurchaseItemsDtos;
using FougeraClub.Services.IServices;
using FougeraClub.Services.Result;
using Mapster;
using MapsterMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FougeraClub.Services.Services
{
    public class PurchaseItemsServices : IPurchaseItemsServices
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        public PurchaseItemsServices(IUnitOfWork unit, IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }
        public async Task<CustomResult> AddItem(int OrderId, PurchaseItemCreateDto dto)
        {
            var orderExist = await _unit.PurchaseOrders.Get(po => po.Id == OrderId);
            if (orderExist == null)
                return CustomResult.Failure(CustomError.NotFoundError("Order you to try to add item to it not Found"));
            var item = _mapper.Map<PurchaseItems>(dto);
            var invoice = await _unit.Invoice.Get(i => i.PurchaseOrderId == OrderId);
            item.PurchaseOrdersId = OrderId;
            item.InvoiceId = invoice.Id;
            _unit.PurchaseItems.Add(item);
            var complete = await _unit.Save();
            var SubTotal =  _unit.PurchaseItems.GetSubTotalPerOrder(OrderId);
            invoice.SubTotal = SubTotal;
            _unit.Invoice.Update(invoice);
            await _unit.Save();
            if (complete == 1)
                return CustomResult.Success();
            return CustomResult.Failure(CustomError.ServerError("Faild to add item to order"));
        }

        public async Task<CustomResult> DeleteItem(int OrderId, int ItemId)
        {
            var orderExist = await _unit.PurchaseOrders.Get(po => po.Id == OrderId);
            if (orderExist == null)
                return CustomResult.Failure(CustomError.NotFoundError("Order you to try to delete item from it not Found"));
            var itemExist = await _unit.PurchaseItems.Get(pi => pi.Id == ItemId);
            if(itemExist == null)   
                return CustomResult.Failure(CustomError.NotFoundError("item you try to delete not found"));
            _unit.PurchaseItems.Delete(itemExist);
            var complete = await _unit.Save();
            if (complete == 1)
                return CustomResult.Success();
            return CustomResult.Failure(CustomError.ServerError("Faild to Delete item from order"));

        }

        public async Task<CustomResult<List<PurchaseItemsDto>>> GetOrderItems(int OrderId)
        {
            var orderExist = await _unit.PurchaseOrders.Get(po => po.Id == OrderId);
            if (orderExist == null)
                CustomResult<List<PurchaseItemsDto>>.Failure(CustomError.NotFoundError("Order Not Found"));
            var items = await _unit.PurchaseItems.GetAll(pi => pi.PurchaseOrdersId == OrderId);
            var itemsdto = items.Adapt<List<PurchaseItemsDto>>();
            return CustomResult<List<PurchaseItemsDto>>.Success(itemsdto);
        }

        public async Task<CustomResult> UpdateItem( PurchaseItemUpdateDto dto)
        {
            var itemExist = await _unit.PurchaseItems.Get(pi => pi.Id == dto.Id);
            if (itemExist == null)
                return CustomResult.Failure(CustomError.NotFoundError("item you try to delete not found"));
            dto.Adapt(itemExist);   
            _unit.PurchaseItems.Update(itemExist);
            var complete = await _unit.Save();
            var invoice = await _unit.Invoice.Get(i => i.Id == itemExist.InvoiceId);
            var SubTotal = _unit.PurchaseItems.GetSubTotalPerOrder(itemExist.PurchaseOrdersId);
            invoice.SubTotal = SubTotal;
            _unit.Invoice.Update(invoice);
            await _unit.Save();
            if (complete == 1)
                return CustomResult.Success();
            return CustomResult.Failure(CustomError.ServerError("Faild to Update item"));

        }
    }
}
