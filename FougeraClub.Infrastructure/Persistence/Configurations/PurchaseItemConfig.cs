using FougeraClub.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FougeraClub.Infrastructure.Persistence.Configurations
{
    public class PurchaseItemConfig : IEntityTypeConfiguration<PurchaseItems>
    {
        public void Configure(EntityTypeBuilder<PurchaseItems> builder)
        {
            builder.Property(pi => pi.Amount).IsRequired();
            builder.HasIndex(pi => pi.PurchaseOrdersId);
            builder.Property(pi => pi.Description).IsRequired();
            
        }
    }
}
