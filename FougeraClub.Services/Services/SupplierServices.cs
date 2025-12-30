using FougeraClub.Core.IRespository;
using FougeraClub.Core.Models;
using FougeraClub.Services.DTOs.SupplierDtos;
using FougeraClub.Services.IServices;
using FougeraClub.Services.Result;
using MapsterMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FougeraClub.Services.Services
{
    public class SupplierServices : ISupplierServices
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        public SupplierServices(IUnitOfWork unit, IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;

        }
        public async Task<CustomResult> AddSupplier(SupplierCreateDto dto)
        {
            var SupplierExist = await _unit.Supplier.Get(s => s.VATNumber ==  dto.VATNumber);
            if (SupplierExist != null)
                return CustomResult.Failure(CustomError.ValidationError("Thier is a Supplier with same VATNumber"));
            var supplier = _mapper.Map<Supplier>(dto);
            _unit.Supplier.Add(supplier);
            var complete = await  _unit.Save();
            if (complete == 1)
                return CustomResult.Success();
            return CustomResult.Failure(CustomError.ServerError("Faild to Add a new Supplier"));
        }

        public Task<CustomResult> DeleteSupplier(string name)
        {
            throw new NotImplementedException();
        }

        public Task<CustomResult<SupplierDto>> GetSupplierByName(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<CustomResult<List<SupplierDto>>> GetSuppliers()
        {
            var suppliers = await _unit.Supplier.GetAll();
            var suppliersDto = _mapper.Map<List<SupplierDto>>(suppliers);
            return CustomResult<List<SupplierDto>>.Success(suppliersDto);
            
        }
    }
}
