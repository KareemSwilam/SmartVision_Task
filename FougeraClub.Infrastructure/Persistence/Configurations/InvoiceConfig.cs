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
    public class InvoiceConfig : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.HasMany(i => i.PurchaseItems)
                   .WithOne(pi => pi.invoice);
            builder.Property(i => i.IsAsign).HasDefaultValue(false);
            builder.Property(i => i.VATRate).HasDefaultValue(0.15);
            builder.Property(i => i.VATAmount).HasComputedColumnSql("[VATRate] * [SubTotal]", stored: true);
            builder.Property(i => i.TotalAmount).HasComputedColumnSql("[SubTotal] + [VATRate] * [SubTotal]", stored: true);
        }
    }
}
