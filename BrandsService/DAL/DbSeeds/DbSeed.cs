using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.DbSeeds
{
    public static class DbSeed
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Brand>().HasData(
                new Brand{ Id = 1, BrandName = "Sandora"},
                new Brand{ Id = 2, BrandName = "OKZDP"},
                new Brand{ Id = 3, BrandName = "PepsiCo"},
                new Brand{ Id = 4, BrandName = "Coca-Cola Company"},
                new Brand{ Id = 5, BrandName = "Guinness"},
                new Brand{ Id = 6, BrandName = "Hoegaarden"},
                new Brand{ Id = 7, BrandName = "Morshynska"},
                new Brand{ Id = 8, BrandName = "Jameson"},
                new Brand{ Id = 9, BrandName = "Jack Daniel's"},
                new Brand{ Id = 10, BrandName = "French rose"}
            );
        }
    }
}