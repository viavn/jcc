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
                .HasKey(ut => ut.Id);

            builder.Property(ut => ut.Id)
                .HasColumnName("id");

            builder.Property(ut => ut.Description)
                .HasColumnName("description")
                .HasMaxLength(20)
                .IsRequired();
        }
    }
}
