using HappyPlate.Domain.Entities;
using HappyPlate.Domain.ValueObjects;
using HappyPlate.Persistence.Constants;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HappyPlate.Persistence.Configurations;

internal sealed class CustomerConfiguration
    : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable(TableNames.Customer);

        builder.HasKey(x => x.Id);

        builder
            .Property(x => x.FirstName)
            .HasConversion(x => x.Value, x => FirstName.Create(x).Value);

        builder
            .Property(x => x.LastName)
            .HasConversion(x => x.Value, x => LastName.Create(x).Value);

        builder
            .Property(x => x.Email)
            .HasConversion(x => x.Value, x => Email.Create(x).Value);

        builder
            .Property(x => x.PhoneNumber)
            .HasConversion(
                x => x.Number,
                x => PhoneNumber.Create(
                    x.Substring(1, 3),
                    x.Substring(6, 3),
                    x.Substring(10, 4),
                    x.Length > 15 ? x.Substring(15) : null).Value);

        builder
            .HasMany(x => x.Addresses)
            .WithOne()
            .HasForeignKey(x => x.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
