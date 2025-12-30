using FougeraClub.Core.Models;
using FougeraClub.Services.DTOs.SupplierDtos;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FougeraClub.Services.Mapping
{
    public class SupplierMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<SupplierCreateDto, Supplier>().TwoWays();
            config.NewConfig<Supplier, SupplierDto>();
        }
    }
}
