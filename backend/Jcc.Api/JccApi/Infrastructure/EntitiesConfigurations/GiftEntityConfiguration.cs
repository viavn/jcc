using JccApi.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JccApi.Infrastructure.EntitiesConfigurations
{
    public class GiftEntityConfiguration : IEntityTypeConfiguration<Gift>
    {
        public void Configure(EntityTypeBuilder<Gift> builder)
        {
            builder.ToTable("gift")
                .HasKey(g => new { g.ChildId, g.GodParentId });

            builder.Property(g => g.ChildId)
                .HasColumnName("child_id")
                .IsRequired();

            builder.Property(g => g.GodParentId)
                .HasColumnName("god_parent_id")
                .IsRequired();

            builder.Property(g => g.CreatedDate)
                .HasColumnName("created_date")
                .IsRequired();

            builder.Property(g => g.UpdatedDate)
                .HasColumnName("updated_date")
                .IsRequired();

            builder.Property(g => g.IsDelivered)
                .HasColumnName("is_delivered")
                .IsRequired();

            builder.Property(g => g.UserId)
                .HasColumnName("user_id")
                .IsRequired();

            builder.Property(g => g.TypeId)
                .HasColumnName("gif_type_id")
                .IsRequired();

            builder.HasOne(g => g.User)
                .WithMany(u => u.Gifts)
                .HasForeignKey(g => g.UserId)
                .IsRequired();

            builder.HasOne(g => g.Child)
                .WithMany(c => c.Gifts)
                .HasForeignKey(g => g.ChildId)
                .IsRequired();

            builder.HasOne(g => g.GodParent)
                .WithMany(gp => gp.Gifts)
                .HasForeignKey(g => g.GodParentId)
                .IsRequired();

            builder.HasOne(g => g.Type)
                .WithMany()
                .HasForeignKey(g => g.TypeId)
                .IsRequired();
        }
    }
}
