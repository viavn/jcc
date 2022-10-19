using JccApi.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JccApi.Infrastructure.EntitiesConfigurations
{
    public class GodParentEntityTypeConfiguration : IEntityTypeConfiguration<GodParent_Old>
    {
        public void Configure(EntityTypeBuilder<GodParent_Old> builder)
        {
            builder.ToTable("god_parents")
                .HasKey(ev => ev.Id);

            builder.Property(ev => ev.Id)
                .HasColumnName("id");

            builder.Property(ev => ev.Name)
                .HasColumnName("name")
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(ev => ev.Phone)
                .HasColumnName("phone")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(ev => ev.IsClothesSelected)
                .HasColumnName("is_clothes_selected")
                .IsRequired();

            builder.Property(ev => ev.IsShoesSelected)
                .HasColumnName("is_shoes_selected")
                .IsRequired();

            builder.Property(ev => ev.IsGiftSelected)
                .HasColumnName("is_gift_selected")
                .IsRequired();

            builder.Property(ev => ev.CreatedDate)
                .HasColumnName("created_date")
                .IsRequired();

            builder.HasOne(p => p.Child)
                .WithMany(b => b.GodParents)
                .HasForeignKey(p => p.ChildId)
                .IsRequired();

            builder.HasOne(p => p.User)
                .WithMany()
                .HasForeignKey(p => p.UserId)
                .IsRequired();
        }
    }
}
