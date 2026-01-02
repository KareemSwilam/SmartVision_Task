using FougeraClub.Core.Models;
using FougeraClub.Services.DTOs.PurchaseOrdersDtos;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FougeraClub.Services.Mapping
{
    public class PurchaseOrdersMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<PurchaseOrdersCreateDto, PurchaseOrders>();
            config.NewConfig<PurchaseOrders, PurchaseOrdersAndSupplier>()
                .Map(dest => dest.Name, src => src.supplier.Name);
            config.NewConfig<PurchaseOrders, PurchaseOrderAndSupplierAndItems>()
                  .Map(dest => dest.SupplierName, src=>  src.supplier.Name);  
            config.NewConfig<PurchaseOrdersUpdateDto, PurchaseOrders>();
               
        }
    }
}
