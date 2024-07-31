using HappyPlate.Domain.Entities;
using HappyPlate.Domain.ValueObjects;
using HappyPlate.Persistence.Constants;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HappyPlate.Persistence.Configurations;

internal sealed class AddressConfiguration
    : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.ToTable(TableNames.Address);

        builder.HasKey(x => x.Id);

        builder
            .Property(x => x.State)
            .HasConversion(x => x.Name, x => State.FromName(x)!);

        builder
            .Property(x => x.ZipCode)
            .HasConversion(x => x.Value, x => ZipCode.Create(x).Value);

        builder
            .Property(x => x.Type)
            .HasConversion(x => x.Name, x => AddressType.FromName(x)!);
    }
}
