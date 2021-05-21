using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.DbSeeds
{
    public static class DbSeed
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category {Id = 1, CategoryName = "Juice", Description = "Description how to store juice..."},
                new Category {Id = 2, CategoryName = "Water", Description = "Description how to store water..."},
                new Category {Id = 3, CategoryName = "Soda", Description = "Description how to store soda..."},
                new Category {Id = 4, CategoryName = "Beer", Description = "Description how to store beer..."},
                new Category {Id = 5, CategoryName = "Wine", Description = "Description how to store wine..."},
                new Category {Id = 6, CategoryName = "Whiskey", Description = "Description how to store whiskey..."}
            );
        }
    }
}