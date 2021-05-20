using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AuthorizationService.Models
{
    public static class SeedRoles
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            var passwordHasher = new PasswordHasher<ApplicationUser>();
            
            //add roles
            var roles = new List<IdentityRole>()
            {
                new() { Name = "Admin", NormalizedName = "ADMIN" },
                new() { Name = "User", NormalizedName = "USER" }
            };

            modelBuilder.Entity<IdentityRole>().HasData(roles);
            
            
            //add users
            var users = new List<ApplicationUser>()
            {
                new()
                {
                    UserName = "user",
                    NormalizedUserName = "USER",
                    Email = "user@gmail.com",
                    NormalizedEmail = "USER@GMAIL.COM",
                    FullName = "Mr. User"
                },

                new()
                {
                    UserName = "admin",
                    NormalizedUserName = "ADMIN",
                    Email = "admin@gmail.com",
                    NormalizedEmail = "ADMIN@GMAIL.COM",
                    FullName = "Mr. Admin"
                },
            };
            
            modelBuilder.Entity<ApplicationUser>().HasData(users);
            
            
            //add users to roles
            var userRoles = new List<IdentityUserRole<string>>();

            users[0].PasswordHash = passwordHasher.HashPassword(users[0], "user");
            users[1].PasswordHash = passwordHasher.HashPassword(users[1], "admin");

            userRoles.Add(new IdentityUserRole<string> { UserId = users[0].Id, RoleId = 
                roles.First(q => q.Name == "User").Id });

            userRoles.Add(new IdentityUserRole<string> { UserId = users[1].Id, RoleId = 
                roles.First(q => q.Name == "Admin").Id });
            
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(userRoles);
        }
    }
}