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
                .HasKey(user => user.Id);

            builder.Property(user => user.Id)
                .HasColumnName("id");

            builder.Property(user => user.Name)
                .HasColumnName("name")
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(user => user.Login)
                .HasColumnName("login")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(user => user.Password)
                .HasColumnName("password")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(user => user.IsDeleted)
                .HasColumnName("is_deleted")
                .HasColumnType("boolean")
                .IsRequired();

            builder.Property(user => user.UserTypeId)
                .HasColumnName("user_type_id")
                .IsRequired();

            builder.HasOne(user => user.UserType)
                .WithMany(userType => userType.Users)
                .HasForeignKey(user => user.UserTypeId)
                .IsRequired();

            builder.HasMany(user => user.Gifts)
                .WithOne(gift => gift.User)
                .HasForeignKey(gift => gift.UserId)
                .IsRequired();
        }
    }

    public class LegalPersonTypeEntityConfiguration : IEntityTypeConfiguration<LegalPersonType>
    {
        public void Configure(EntityTypeBuilder<LegalPersonType> builder)
        {
            builder.ToTable("legal_person_type")
                .HasKey(legalPersonType => legalPersonType.Id);

            builder.Property(legalPersonType => legalPersonType.Id)
                .HasColumnName("id")
                .HasColumnType("integer");

            builder.Property(legalPersonType => legalPersonType.Description)
                .HasColumnName("description")
                .HasMaxLength(20)
                .IsRequired();
        }
    }

    public class GiftTypeEntityConfiguration : IEntityTypeConfiguration<GiftType>
    {
        public void Configure(EntityTypeBuilder<GiftType> builder)
        {
            builder.ToTable("gift_type")
                .HasKey(giftType => giftType.Id);

            builder.Property(giftType => giftType.Id)
                .HasColumnName("id")
                .HasColumnType("integer");

            builder.Property(giftType => giftType.Description)
                .HasColumnName("description")
                .HasMaxLength(20)
                .IsRequired();
        }
    }

    public class GenreTypeEntityConfiguration : IEntityTypeConfiguration<GenreType>
    {
        public void Configure(EntityTypeBuilder<GenreType> builder)
        {
            builder.ToTable("genre")
                .HasKey(giftType => giftType.Id);

            builder.Property(giftType => giftType.Id)
                .HasColumnName("id")
                .HasColumnType("integer");

            builder.Property(giftType => giftType.Description)
                .HasColumnName("description")
                .HasMaxLength(6)
                .IsRequired();
        }
    }

    public class FamilyMemberEntityConfiguration : IEntityTypeConfiguration<FamilyMember>
    {
        public void Configure(EntityTypeBuilder<FamilyMember> builder)
        {
            builder.ToTable("family_member")
                .HasKey(familyMember => familyMember.Id);

            builder.Property(familyMember => familyMember.Id)
                .HasColumnName("id");

            builder.Property(familyMember => familyMember.Name)
                .HasColumnName("name")
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(familyMember => familyMember.LegalPersonTypeId)
                .HasColumnName("legal_person_type_id")
                .IsRequired();

            builder.HasOne(familyMember => familyMember.LegalPersonType)
                .WithMany(legalPerson => legalPerson.Members)
                .HasForeignKey(familyMember => familyMember.LegalPersonTypeId)
                .IsRequired();

            builder.Property(familyMember => familyMember.Family)
                .HasColumnName("family_id")
                .IsRequired();

            builder.HasOne(familyMember => familyMember.Family)
                .WithMany(legalPerson => legalPerson.Members)
                .HasForeignKey(familyMember => familyMember.FamilyId)
                .IsRequired();
        }
    }

    public class FamilyEntityConfiguration : IEntityTypeConfiguration<Family>
    {
        public void Configure(EntityTypeBuilder<Family> builder)
        {
            builder.ToTable("family")
                .HasKey(family => family.Id);

            builder.Property(family => family.Id)
                .HasColumnName("id");

            builder.Property(family => family.Code)
                .HasColumnName("code")
                .HasMaxLength(10)
                .IsRequired();

            builder.Property(family => family.ContactNumber)
                .HasColumnName("contact_number")
                .HasMaxLength(11)
                .IsRequired();

            builder.Property(family => family.Address)
                .HasColumnName("address")
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(family => family.Comment)
                .HasColumnName("comment");
        }
    }

    public class ChildEntityConfiguration : IEntityTypeConfiguration<Child>
    {
        public void Configure(EntityTypeBuilder<Child> builder)
        {
            builder.ToTable("child")
                .HasKey(child => child.Id);

            builder.Property(child => child.Id)
                .HasColumnName("id");

            builder.Property(child => child.Name)
                .HasColumnName("name")
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(child => child.Age)
                .HasColumnName("age")
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(child => child.ClotheSize)
                .HasColumnName("clothe_size")
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(child => child.ShoeSize)
                .HasColumnName("shoe_size")
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(child => child.GenreTypeId)
                .HasColumnName("genre_type_id")
                .IsRequired();

            builder.HasOne(child => child.GenreType)
                .WithMany(genre => genre.Children)
                .HasForeignKey(child => child.GenreTypeId)
                .IsRequired();

            builder.Property(child => child.FamilyId)
                .HasColumnName("family_id")
                .IsRequired();

            builder.HasOne(child => child.Family)
                .WithMany(family => family.Children)
                .HasForeignKey(child => child.FamilyId)
                .IsRequired();
        }
    }

    public class GodParentEntityConfiguration : IEntityTypeConfiguration<GodParent>
    {
        public void Configure(EntityTypeBuilder<GodParent> builder)
        {
            builder.ToTable("god_parent")
                .HasKey(child => child.Id);

            builder.Property(child => child.Id)
                .HasColumnName("id");

            builder.Property(child => child.Name)
                .HasColumnName("name")
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(child => child.ContactNumber)
                .HasColumnName("contact_number")
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(child => child.Address)
                .HasColumnName("address")
                .HasMaxLength(500)
                .IsRequired();
        }
    }

    public class GiftEntityConfiguration : IEntityTypeConfiguration<Gift>
    {
        public void Configure(EntityTypeBuilder<Gift> builder)
        {
            builder.ToTable("gift")
                .HasKey(gift => new { gift.ChildId, gift.GodParentId });

            builder.Property(gift => gift.ChildId)
                .HasColumnName("child_id")
                .IsRequired();

            builder.Property(gift => gift.GodParentId)
                .HasColumnName("god_parent_id")
                .IsRequired();

            builder.Property(gift => gift.CreatedDate)
                .HasColumnName("created_date")
                .IsRequired();

            builder.Property(gift => gift.UpdatedDate)
                .HasColumnName("updated_date")
                .IsRequired();

            builder.Property(gift => gift.IsDelivered)
                .HasColumnName("is_delivered")
                .IsRequired();

            builder.Property(gift => gift.UserId)
                .HasColumnName("user_id")
                .IsRequired();

            builder.HasOne(gift => gift.User)
                .WithMany(user => user.Gifts)
                .HasForeignKey(gift => gift.UserId)
                .IsRequired();

            builder.HasOne(gift => gift.Child)
                .WithMany(child => child.Gifts)
                .HasForeignKey(gift => gift.ChildId)
                .IsRequired();

            builder.HasOne(gift => gift.GodParent)
                .WithMany(godParent => godParent.Gifts)
                .HasForeignKey(gift => gift.GodParentId)
                .IsRequired();
        }
    }
}
