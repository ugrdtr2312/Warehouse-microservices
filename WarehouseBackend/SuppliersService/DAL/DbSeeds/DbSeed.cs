using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.DbSeeds
{
    public static class DbSeed
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Supplier>().HasData(
                new Supplier{ Id=1, CompanyName = "Sidorov company", FirstName = "Nikita", Surname = "Sidorov", PhoneNumber = "050-111-11-11"},
                new Supplier{ Id=2, CompanyName = "Stepaniuk warehouse", FirstName = "Ira", Surname = "Stepaniuk", PhoneNumber = "050-222-22-22"},
                new Supplier{ Id=3, CompanyName = "Fedorenko storage", FirstName = "Danial", Surname = "Fedorenko", PhoneNumber = "050-333-33-33"},
                new Supplier{ Id=4, CompanyName = "Dolid delivery", FirstName = "Vova", Surname = "Dolid", PhoneNumber = "050-444-44-44"}
            );
        }
    }
}