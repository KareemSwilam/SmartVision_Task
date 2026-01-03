using FougeraClub.Core.Models;
using FougeraClub.Services.DTOs.InvoiceDtos;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FougeraClub.Services.Mapping
{
    public class InvoiceMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<InvoiceCreateDto, Invoice>();
            config.NewConfig<Invoice, InvoiceDto>();
        }
    }
}
