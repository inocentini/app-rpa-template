using Microsoft.EntityFrameworkCore;
using RPA.Core.Entities;

namespace RPA.Infrastructure.Data
{
    public class RPADbContext : DbContext
    {
        public RPADbContext(DbContextOptions<RPADbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurações globais
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                // Configuração para entidades que herdam de BaseEntity
                if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
                {
                    modelBuilder.Entity(entityType.ClrType)
                        .Property(nameof(BaseEntity.CreatedAt))
                        .HasDefaultValueSql("GETDATE()");

                    modelBuilder.Entity(entityType.ClrType)
                        .Property(nameof(BaseEntity.IsActive))
                        .HasDefaultValue(true);
                }
            }
        }
    }
} 