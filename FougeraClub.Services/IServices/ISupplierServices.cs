using FougeraClub.Services.DTOs.SupplierDtos;
using FougeraClub.Services.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FougeraClub.Services.IServices
{
    public interface ISupplierServices
    {
        public Task<CustomResult> AddSupplier(SupplierCreateDto dto);
        public Task<CustomResult<List<SupplierDto>>> GetSuppliers();
        public Task<CustomResult<List<SupplierDto>>> GetSupplierByNameOrPhone(string filter);
        public Task<CustomResult> DeleteSupplier(string VATNumber);
        public Task<CustomResult> UpdateSupplier(string VATNumber, SupplierUpdateDto dto);

    }
}
