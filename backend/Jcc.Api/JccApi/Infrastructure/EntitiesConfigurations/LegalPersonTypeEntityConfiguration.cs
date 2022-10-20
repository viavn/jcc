using JccApi.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JccApi.Infrastructure.EntitiesConfigurations
{
    public class LegalPersonTypeEntityConfiguration : IEntityTypeConfiguration<LegalPersonType>
    {
        public void Configure(EntityTypeBuilder<LegalPersonType> builder)
        {
            builder.ToTable("legal_person_type")
                .HasKey(lp => lp.Id);

            builder.Property(lp => lp.Id)
                .HasColumnName("id");

            builder.Property(lp => lp.Description)
                .HasColumnName("description")
                .HasMaxLength(20)
                .IsRequired();
        }
    }
}
