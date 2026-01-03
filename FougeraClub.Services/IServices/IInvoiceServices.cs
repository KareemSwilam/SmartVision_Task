using FougeraClub.Services.DTOs.InvoiceDtos;
using FougeraClub.Services.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FougeraClub.Services.IServices
{
    public interface IInvoiceServices
    {
        public Task<CustomResult> AddInvoice(InvoiceCreateDto dto);
        public Task<CustomResult> AsignInvoice(int id);
        public Task<CustomResult<InvoiceDto>> GetInvoiceByOrderId(int orderid);
        public Task<CustomResult<InvoiceWithItemsWithSupplierNameDto>> GetAllDetailsInvoiceByOrderId(int orderid);
        public Task<CustomResult> UpdateInvoice(int orderid, InvoiceUpdateDto dto);
    }
}
