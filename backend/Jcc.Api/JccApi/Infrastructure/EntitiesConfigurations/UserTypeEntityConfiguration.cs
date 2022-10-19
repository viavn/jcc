using JccApi.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JccApi.Infrastructure.EntitiesConfigurations
{
    public class UserTypeEntityConfiguration : IEntityTypeConfiguration<UserType>
    {
        public void Configure(EntityTypeBuilder<UserType> builder)
        {
            builder.ToTable("user_type")
                .HasKey(ev => ev.Id);

            builder.Property(ev => ev.Id)
                .HasColumnName("id")
                .HasColumnType("integer");

            builder.Property(ev => ev.Description)
                .HasColumnName("description")
                .HasMaxLength(20)
                .IsRequired();
        }
    }
}
