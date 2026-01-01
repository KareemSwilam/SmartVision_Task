using FougeraClub.Core.IRespository;
using FougeraClub.Core.Models;
using FougeraClub.Services.DTOs.SupplierDtos;
using FougeraClub.Services.IServices;
using FougeraClub.Services.Result;
using Mapster;
using MapsterMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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

        public async Task<CustomResult> DeleteSupplier(string VATNumber)
        {
            var supplierExist = await _unit.Supplier.Get(S => S.VATNumber == VATNumber);
            if (supplierExist == null)
                return CustomResult.Failure(CustomError.NotFoundError("Supplier you try to delete not exist"));
            _unit.Supplier.Delete(supplierExist);
            var complete = await _unit.Save();
            if (complete == 1)
                return CustomResult.Success();
            return CustomResult.Failure(CustomError.ServerError("Faild to Delete Supplier"));
        }

        

        public async Task<CustomResult<List<SupplierDto>>> GetSupplierByNameOrPhone(string filter)
        {
            var supplierExist = await _unit.Supplier.GetAll(s => s.Name.Contains(filter));
            if (supplierExist != null)
            {
                var supplierDto = supplierExist.Adapt<List<SupplierDto>>();
                return CustomResult<List<SupplierDto>>.Success(supplierDto);
            }
            supplierExist = await _unit.Supplier.GetAll(s => s.PhoneNumber.Contains(filter));
            if (supplierExist != null)
            {
                var supplierDto = supplierExist.Adapt<List<SupplierDto>>();
                return CustomResult<List<SupplierDto>>.Success(supplierDto);
            }
            return CustomResult<List<SupplierDto>>.Success(new List<SupplierDto>());
        }

        public async Task<CustomResult<List<SupplierDto>>> GetSuppliers()
        {
            var suppliers = await _unit.Supplier.GetAll();
            var suppliersDto = _mapper.Map<List<SupplierDto>>(suppliers);
            return CustomResult<List<SupplierDto>>.Success(suppliersDto);
            
        }

        public async Task<CustomResult> UpdateSupplier(string VATNumber, SupplierUpdateDto dto)
        {
            var SupplierExist = await _unit.Supplier.Get(s => s.VATNumber ==  VATNumber);
            if (SupplierExist == null)
                CustomResult.Failure(CustomError.NotFoundError("Supplier you try to update not exist"));
            dto.Adapt(SupplierExist);
            _unit.Supplier.Update(SupplierExist!);
            var complete = await _unit.Save();
            if (complete == 1)
                return CustomResult.Success();
            return CustomResult.Failure(CustomError.ServerError("Faild to Update Supplier"));

        }
    }
}
