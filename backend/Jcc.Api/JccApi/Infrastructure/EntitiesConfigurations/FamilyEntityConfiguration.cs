using JccApi.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JccApi.Infrastructure.EntitiesConfigurations
{
    public class FamilyEntityConfiguration : IEntityTypeConfiguration<Family>
    {
        public void Configure(EntityTypeBuilder<Family> builder)
        {
            builder.ToTable("family")
                .HasKey(f => f.Id);

            builder.Property(f => f.Id)
                .HasColumnName("id");

            builder.Property(f => f.Code)
                .HasColumnName("code")
                .HasMaxLength(10)
                .IsRequired();

            builder.Property(f => f.ContactNumber)
                .HasColumnName("contact_number")
                .HasMaxLength(11)
                .IsRequired();

            builder.Property(f => f.Address)
                .HasColumnName("address")
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(f => f.Comment)
                .HasColumnName("comment");
        }
    }
}
