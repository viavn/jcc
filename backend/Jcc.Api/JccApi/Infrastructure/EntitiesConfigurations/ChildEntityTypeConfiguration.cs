using JccApi.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JccApi.Infrastructure.EntitiesConfigurations
{
    public class ChildEntityTypeConfiguration : IEntityTypeConfiguration<Child>
    {
        public void Configure(EntityTypeBuilder<Child> builder)
        {
            builder.ToTable("children")
                .HasKey(ev => ev.Id);

            builder.Property(ev => ev.Id)
                .HasColumnName("id");

            builder.Property(ev => ev.Name)
                .HasColumnName("name")
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(ev => ev.Age)
                .HasColumnName("age")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(ev => ev.ClothesSize)
                .HasColumnName("clothes_size")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(ev => ev.ShoesSize)
                .HasColumnName("shoes_size")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(ev => ev.LegalResponsible)
                .HasColumnName("legal_responsible")
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(ev => ev.FamilyAcronym)
                .HasColumnName("family_acronym")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(ev => ev.FamilyPhone)
                .HasColumnName("family_phone")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(ev => ev.FamilyAddress)
                .HasColumnName("family_address")
                .HasMaxLength(500)
                .IsRequired();
        }
    }
}
