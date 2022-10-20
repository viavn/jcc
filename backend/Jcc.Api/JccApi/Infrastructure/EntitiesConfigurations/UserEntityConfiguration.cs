using JccApi.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JccApi.Infrastructure.EntitiesConfigurations
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("user")
                .HasKey(u => u.Id);

            builder.Property(u => u.Id)
                .HasColumnName("id");

            builder.Property(u => u.Name)
                .HasColumnName("name")
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(u => u.Login)
                .HasColumnName("login")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(u => u.Password)
                .HasColumnName("password")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(u => u.IsDeleted)
                .HasColumnName("is_deleted")
                .HasColumnType("boolean")
                .IsRequired();

            builder.Property(u => u.UserTypeId)
                .HasColumnName("user_type_id")
                .IsRequired();

            builder.HasOne(u => u.UserType)
                .WithMany(ut => ut.Users)
                .HasForeignKey(u => u.UserTypeId)
                .IsRequired();

            builder.HasMany(u => u.Gifts)
                .WithOne(g => g.User)
                .HasForeignKey(g => g.UserId)
                .IsRequired();
        }
    }
}
