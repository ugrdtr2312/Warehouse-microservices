using DAL.Configurations;
using DAL.DbSeeds;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Contexts
{
    public class WarehouseContext : DbContext
    {
        public DbSet<Brand> Brands { get; set; }
        
        public WarehouseContext(DbContextOptions<WarehouseContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new BrandConfiguration());
            
            DbSeed.Seed(modelBuilder);
        }
    }
}