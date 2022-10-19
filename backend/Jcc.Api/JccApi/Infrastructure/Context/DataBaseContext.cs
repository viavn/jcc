using JccApi.Entities;
using JccApi.Infrastructure.EntitiesConfigurations;
using Microsoft.EntityFrameworkCore;

namespace JccApi.Infrastructure.Context
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions options) : base(options) { }

        public DbSet<GodParent_Old> GodParents { get; set; }
        public DbSet<Child_Old> Children { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserType> UserTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("uuid-ossp");
            new ChildEntityTypeConfiguration().Configure(modelBuilder.Entity<Child_Old>());
            new GodParentEntityTypeConfiguration().Configure(modelBuilder.Entity<GodParent_Old>());
            new UserEntityConfiguration().Configure(modelBuilder.Entity<User>());
            new UserTypeEntityConfiguration().Configure(modelBuilder.Entity<UserType>());
        }
    }
}
