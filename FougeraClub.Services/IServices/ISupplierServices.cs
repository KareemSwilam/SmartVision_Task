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
        public Task<CustomResult<SupplierDto>> GetSupplierByName(string name);
        public Task<CustomResult> DeleteSupplier(string name);


    }
}
