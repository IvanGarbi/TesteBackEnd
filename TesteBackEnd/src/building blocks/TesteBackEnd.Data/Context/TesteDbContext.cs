using Microsoft.EntityFrameworkCore;
using TesteBackEnd.Core.Models;

namespace TesteBackEnd.Data.Context
{
    public class TesteDbContext : DbContext
    {
        public TesteDbContext(DbContextOptions<TesteDbContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public DbSet<Cliente> Clientes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                         .SelectMany(e => e.GetProperties()
                             .Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("VARCHAR(50)");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TesteDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
