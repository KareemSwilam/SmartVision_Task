using FougeraClub.Core.IRespository;
using FougeraClub.Core.Models;
using FougeraClub.Services.DTOs.InvoiceDtos;
using FougeraClub.Services.DTOs.PurchaseItemsDtos;
using FougeraClub.Services.IServices;
using FougeraClub.Services.Result;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace FougeraClub.Services.Services
{
    public class InvoiceServices: IInvoiceServices
    {
        private readonly IUnitOfWork _unit;
        public InvoiceServices(IUnitOfWork unit)
        {
            _unit = unit;
        }

        public async Task<CustomResult> AddInvoice(InvoiceCreateDto dto)
        {
            if (dto == null)
                return CustomResult.Failure(CustomError.ValidationError("Faild in adding Invoice"));
            var invoice = dto.Adapt<Invoice>();
            _unit.Invoice.Add(invoice);
            var complete = await _unit.Save();
            if (complete == 1)
                return CustomResult.Success();
            return CustomResult.Failure(CustomError.ServerError("Faild in adding Invoice"));

        }

        public async Task<CustomResult> AsignInvoice(int id)
        {
            var invoiceExist = await _unit.Invoice.Get(i => i.Id == id);
            if (invoiceExist == null)
                return CustomResult.Failure(CustomError.NotFoundError("Invoice you try to Sign not Exist"));
            invoiceExist.IsAsign = true;    
            _unit.Invoice.Update(invoiceExist);
            var complete = await _unit.Save();
            if (complete == 1)
                return CustomResult.Success();
            return CustomResult.Failure(CustomError.ServerError("Faild in adding Invoice"));

        }

        public async Task<CustomResult<InvoiceWithItemsWithSupplierNameDto>> GetAllDetailsInvoiceByOrderId(int orderid)
        {
            var invoiceExist = await _unit.Invoice.Get(i => i.PurchaseOrderId == orderid);
            if (invoiceExist == null)
                return CustomResult<InvoiceWithItemsWithSupplierNameDto>.Failure(CustomError.NotFoundError("This order do't Has Invoice"));
            var invoice = await _unit.Invoice.GetInvoiceWithItems(invoiceExist.PurchaseOrderId);
            var orderWithSupplier = await _unit.PurchaseOrders.GetOrderWithSupplier(invoiceExist.PurchaseOrderId);
            var dto = new InvoiceWithItemsWithSupplierNameDto
            {
                PurchaseOrderId = invoice.PurchaseOrderId,
                SubTotal = invoice.SubTotal,
                VATAmount = invoice.VATAmount,
                VATRate = invoice.VATRate,
                TotalAmount = invoice.TotalAmount,
                IsAsign = invoice.IsAsign,
                SupplierName = orderWithSupplier.supplier.Name,
                AmountInWord = AEDConverter.ConvertToWords(invoice.TotalAmount),
                items = invoice.PurchaseItems.Adapt<List<PurchaseItemsDto>>()
            };

            return CustomResult<InvoiceWithItemsWithSupplierNameDto>.Success(dto);
        }

        public async Task<CustomResult<InvoiceDto>> GetInvoiceByOrderId(int orderid)
        {
            var invoiceExist = await _unit.Invoice.Get(i => i.PurchaseOrderId == orderid);
            if (invoiceExist == null)
                return CustomResult<InvoiceDto>.Failure(CustomError.NotFoundError("This order do't Has Invoice"));
            var invoiceDto = invoiceExist.Adapt<InvoiceDto>();
            return CustomResult<InvoiceDto>.Success(invoiceDto);

        }

        public async Task<CustomResult> UpdateInvoice(int orderid, InvoiceUpdateDto dto)
        {
            var invoiceExist = await _unit.Invoice.Get(i => i.PurchaseOrderId == orderid);
            if (invoiceExist == null)
                return CustomResult.Failure(CustomError.NotFoundError("This order do't Has Invoice"));
            invoiceExist.SubTotal = dto.SubTotal;
            invoiceExist.VATRate = dto.VATRate;
            invoiceExist.IsAsign = dto.IsAsign;
            _unit.Invoice.Update(invoiceExist);
            var complete = await _unit.Save();
            if (complete == 1)
                return CustomResult.Success();
            return CustomResult.Failure(CustomError.ServerError("Faild in adding Invoice"));

        }
    }
}
