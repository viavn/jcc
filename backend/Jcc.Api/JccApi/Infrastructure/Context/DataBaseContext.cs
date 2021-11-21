using JccApi.Entities;
using JccApi.Infrastructure.EntitiesConfigurations;
using Microsoft.EntityFrameworkCore;

namespace JccApi.Infrastructure.Context
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions options) : base(options) { }

        public DbSet<GodParent> GodParents { get; set; }
        public DbSet<Child> Children { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("uuid-ossp");
            new ChildEntityTypeConfiguration().Configure(modelBuilder.Entity<Child>());
            new GodParentEntityTypeConfiguration().Configure(modelBuilder.Entity<GodParent>());
            new UserEntityTypeConfiguration().Configure(modelBuilder.Entity<User>());
        }
    }
}
