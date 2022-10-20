using JccApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace JccApi.Infrastructure.Context
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions options) : base(options) { }

        public DbSet<Child> Children { get; set; }
        public DbSet<Family> Families { get; set; }
        public DbSet<FamilyMember> FamilyMembers { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Gift> Gifts { get; set; }
        public DbSet<GiftType> GiftTypes { get; set; }
        public DbSet<GodParent> GodParents { get; set; }
        public DbSet<LegalPersonType> LegalPersonTypes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserType> UserTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("uuid-ossp");
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(User).Assembly);
        }
    }
}
