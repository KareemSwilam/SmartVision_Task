using FougeraClub.Core.IRespository;
using FougeraClub.Core.Models;
using FougeraClub.Services.DTOs.PurchaseOrdersDtos;
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
    public class PurchaseOrdersServices : IPurchaseOrdersServices
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        public PurchaseOrdersServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unit = unitOfWork;  
            _mapper = mapper;
        }
        public async Task<CustomResult> AddOrders(PurchaseOrdersCreateDto createDto)
        {
            var supplierExist = await _unit.Supplier.Get(s => s.Id == createDto.SupplierId);
            if (supplierExist == null)
                return CustomResult.Failure(CustomError.NotFoundError("Supplier Not Exist"));
            var order = _mapper.Map<PurchaseOrders>(createDto);
            _unit.PurchaseOrders.Add(order);
            var complete = await _unit.Save();
            if (complete == 1)
                return CustomResult.Success();
            return CustomResult.Failure(CustomError.ServerError("Failer in Adding New Purchases order"));

        }

        public async Task<CustomResult> DeleteOrder(int id)
        {
            var orderExist = await _unit.PurchaseOrders.Get(po =>  po.Id == id);
            if (orderExist != null)
                _unit.PurchaseOrders.Delete(orderExist);
            CustomResult.Failure(CustomError.NotFoundError("The order you try to delete Not Exist"));
            var complete = await _unit.Save();
            if(complete == 1) return CustomResult.Success();
            return CustomResult.Failure(CustomError.ServerError("Failer in Delete this Order"));

        }

        public async Task<CustomResult<List<PurchaseOrdersAndSupplier>>> GetOrderAndSupplier()
        {
            var orderSupplier = await _unit.PurchaseOrders.GetAllOrderandSupplier();
            var orderSupplierDto = _mapper.Map<List<PurchaseOrdersAndSupplier>>(orderSupplier);
            return CustomResult<List<PurchaseOrdersAndSupplier>>.Success(orderSupplierDto);
        }

        public async Task<CustomResult<PurchaseOrderAndSupplierAndItems>> GetOrderAndSupplierAndItems(int id)
        {
            var orderExist = await _unit.PurchaseOrders.Get(p => p.Id == id);
            if (orderExist == null)
                return CustomResult<PurchaseOrderAndSupplierAndItems>.Failure(CustomError.NotFoundError("Order Not Found"));
            var orderSupplierItems = await _unit.PurchaseOrders.GetAllOrderandSupplierAndItems(id);
            var dto = _mapper.Map<PurchaseOrderAndSupplierAndItems>(orderSupplierItems);
            return CustomResult<PurchaseOrderAndSupplierAndItems>.Success(dto); 

        }

        public async Task<CustomResult<List<PurchaseOrdersAndSupplier>>> GetOrderAndSupplierWithFilter(PurchaseOrdersAndSupplierFilterDto dto)
        {
            var orderSupplier = await _unit.PurchaseOrders.GetAllOrderandSupplier(fromDate: dto.fromDate, toDate: dto.toDate, SupplierName: dto.supplierName  );
            var orderSupplierDto = _mapper.Map<List<PurchaseOrdersAndSupplier>>(orderSupplier);
            return CustomResult<List<PurchaseOrdersAndSupplier>>.Success(orderSupplierDto);
        }

        public async Task<CustomResult> UpdateOrder(int id, PurchaseOrdersUpdateDto dto)
        {
            var orderExist = await _unit.PurchaseOrders.Get(po => po.Id == id);
            if (orderExist == null)
                return CustomResult.Failure(CustomError.NotFoundError("Order you try to upate not found"));
            dto.Adapt(orderExist);
            _unit.PurchaseOrders.Update(orderExist);
            var complete = await _unit.Save();
            if (complete == 1)
                return CustomResult.Success();
            return CustomResult.Failure(CustomError.ServerError("Faild to Update item"));

        }
    }
}
