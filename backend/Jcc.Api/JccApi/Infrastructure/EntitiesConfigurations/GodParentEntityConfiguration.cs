using JccApi.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JccApi.Infrastructure.EntitiesConfigurations
{
    public class GodParentEntityConfiguration : IEntityTypeConfiguration<GodParent>
    {
        public void Configure(EntityTypeBuilder<GodParent> builder)
        {
            builder.ToTable("god_parent")
                .HasKey(gp => gp.Id);

            builder.Property(gp => gp.Id)
                .HasColumnName("id");

            builder.Property(gp => gp.Name)
                .HasColumnName("name")
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(gp => gp.ContactNumber)
                .HasColumnName("contact_number")
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(gp => gp.Address)
                .HasColumnName("address")
                .HasMaxLength(500)
                .IsRequired();
        }
    }
}
