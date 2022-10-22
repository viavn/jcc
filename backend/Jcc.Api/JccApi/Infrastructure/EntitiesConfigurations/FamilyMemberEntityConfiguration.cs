using JccApi.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JccApi.Infrastructure.EntitiesConfigurations
{
    public class FamilyMemberEntityConfiguration : IEntityTypeConfiguration<FamilyMember>
    {
        public void Configure(EntityTypeBuilder<FamilyMember> builder)
        {
            builder.ToTable("family_member")
                .HasKey(fm => fm.Id);

            builder.Property(fm => fm.Id)
                .HasColumnName("id");

            builder.Property(fm => fm.Name)
                .HasColumnName("name")
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(fm => fm.LegalPersonTypeId)
                .HasColumnName("legal_person_type_id")
                .IsRequired();

            builder.HasOne(fm => fm.LegalPersonType)
                .WithMany()
                .HasForeignKey(fm => fm.LegalPersonTypeId)
                .IsRequired();

            builder.Property(fm => fm.FamilyId)
                .HasColumnName("family_id")
                .IsRequired();

            builder.HasOne(fm => fm.Family)
                .WithMany(f => f.Members)
                .HasForeignKey(fm => fm.FamilyId)
                .IsRequired();
        }
    }
}
