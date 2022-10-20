using JccApi.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JccApi.Infrastructure.EntitiesConfigurations
{
    public class ChildEntityConfiguration : IEntityTypeConfiguration<Child>
    {
        public void Configure(EntityTypeBuilder<Child> builder)
        {
            builder.ToTable("child")
                .HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .HasColumnName("id");

            builder.Property(c => c.Name)
                .HasColumnName("name")
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(c => c.Age)
                .HasColumnName("age")
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(c => c.ClotheSize)
                .HasColumnName("clothe_size")
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(c => c.ShoeSize)
                .HasColumnName("shoe_size")
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(c => c.GenreTypeId)
                .HasColumnName("genre_type_id")
                .IsRequired();

            builder.HasOne(c => c.GenreType)
                .WithMany(g => g.Children)
                .HasForeignKey(c => c.GenreTypeId)
                .IsRequired();

            builder.Property(c => c.FamilyId)
                .HasColumnName("family_id")
                .IsRequired();

            builder.HasOne(c => c.Family)
                .WithMany(f => f.Children)
                .HasForeignKey(c => c.FamilyId)
                .IsRequired();
        }
    }
}
