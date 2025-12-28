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
    public class PurchaseOrdersConfig : IEntityTypeConfiguration<PurchaseOrders>
    {
        public void Configure(EntityTypeBuilder<PurchaseOrders> builder)
        {
            builder.Property(po => po.Date)
                   .HasColumnType("date")
                   .HasDefaultValueSql("CAST(GETDATE() AS date)");
            builder.HasIndex(po => po.SupplierId);
            builder.HasMany(po => po.items)
                   .WithOne(pi => pi.order)
                   .HasForeignKey(pi => pi.PurchaseOrdersId)
                   .IsRequired();

        }
    }
}
