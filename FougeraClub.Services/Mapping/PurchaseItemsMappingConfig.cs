using FougeraClub.Core.Models;
using FougeraClub.Services.DTOs.PurchaseItemsDtos;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FougeraClub.Services.Mapping
{
    public class PurchaseItemsMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<PurchaseItems, PurchaseItemsDto>();
            config.NewConfig<PurchaseItemCreateDto, PurchaseItems>();
            config.NewConfig<PurchaseItemUpdateDto, PurchaseItems>();
        }
    }
}
