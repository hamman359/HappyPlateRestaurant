using HappyPlate.Domain.Entities;
using HappyPlate.Domain.ValueObjects;
using HappyPlate.Persistence.Constants;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HappyPlate.Persistence.Configurations;
internal sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable(TableNames.Product);

        builder.HasKey(x => x.Id);

        builder
            .Property(x => x.Price)
            .HasConversion(x => x.Amount, x => Price.Create(x).Value);
    }
}
