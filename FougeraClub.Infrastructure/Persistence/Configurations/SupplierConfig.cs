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
    public class SupplierConfig : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.Property(s => s.Name).IsRequired();
            builder.Property(s => s.VATNumber).IsRequired();
            builder.Property(s => s.PhoneNumber).IsRequired();
            builder.Property(s => s.IsDeleted).HasDefaultValue(false);
            builder.HasIndex(s => s.Name);
            builder.HasMany(s => s.purchases)
                   .WithOne(p => p.supplier)
                   .HasForeignKey(p => p.SupplierId)
                   .IsRequired();
            builder.HasQueryFilter(s => s.IsDeleted == false);
        }
    }
}
