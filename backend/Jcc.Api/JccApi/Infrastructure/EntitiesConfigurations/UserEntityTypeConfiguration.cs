using JccApi.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JccApi.Infrastructure.EntitiesConfigurations
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users")
                .HasKey(ev => ev.Id);

            builder.Property(ev => ev.Id)
                .HasColumnName("id");

            builder.Property(ev => ev.Name)
                .HasColumnName("name")
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(ev => ev.Login)
                .HasColumnName("login")
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(ev => ev.Password)
                .HasColumnName("password")
                .HasMaxLength(500)
                .IsRequired();
        }
    }
}
