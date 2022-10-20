using JccApi.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JccApi.Infrastructure.EntitiesConfigurations
{
    public class GenreTypeEntityConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.ToTable("genre")
                .HasKey(g => g.Id);

            builder.Property(g => g.Id)
                .HasColumnName("id");

            builder.Property(g => g.Description)
                .HasColumnName("description")
                .HasMaxLength(6)
                .IsRequired();
        }
    }
}
