using JccApi.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JccApi.Infrastructure.EntitiesConfigurations
{
    public class GiftTypeEntityConfiguration : IEntityTypeConfiguration<GiftType>
    {
        public void Configure(EntityTypeBuilder<GiftType> builder)
        {
            builder.ToTable("gift_type")
                .HasKey(gt => gt.Id);

            builder.Property(gt => gt.Id)
                .HasColumnName("id");

            builder.Property(gt => gt.Description)
                .HasColumnName("description")
                .HasMaxLength(20)
                .IsRequired();
        }
    }
}
