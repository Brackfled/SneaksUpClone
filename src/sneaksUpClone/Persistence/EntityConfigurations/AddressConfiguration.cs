using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class AddressConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.ToTable("Addresses").HasKey(a => a.Id);

        builder.Property(a => a.Id).HasColumnName("Id").IsRequired();
        builder.Property(a => a.UserId).HasColumnName("UserId").IsRequired();
        builder.Property(a => a.AddressName).HasColumnName("AddressName").IsRequired();
        builder.Property(a => a.City).HasColumnName("City").IsRequired();
        builder.Property(a => a.Country).HasColumnName("Country").IsRequired();
        builder.Property(a => a.ZipCode).HasColumnName("ZipCode").IsRequired();
        builder.Property(a => a.ContactName).HasColumnName("ContactName").IsRequired();
        builder.Property(a => a.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(a => a.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(a => a.DeletedDate).HasColumnName("DeletedDate");

        builder.HasOne(a => a.User);

        builder.HasQueryFilter(a => !a.DeletedDate.HasValue);
    }
}