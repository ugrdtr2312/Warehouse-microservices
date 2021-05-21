using Microsoft.EntityFrameworkCore.Migrations;

namespace AuthorizationService.Migrations
{
    public partial class UsersRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "07f2660c-7827-44e0-b392-d9ac0bfc1301");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e27274b2-c539-4d1b-95b7-657ad90e23cf");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "9c8cde1f-6728-41d4-ad53-52078e55a56e", "3dda49e2-5c4e-4e45-a0ea-af216e17253b", "Admin", "ADMIN" },
                    { "5635b31d-abe8-4686-8e10-3cfe48c1962e", "a2bb2ca3-677f-4976-9ad2-2e2c71b50762", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "ed8ed912-c3df-4352-a96f-8dc8f99cb25f", 0, "f25cdf31-f569-4af1-896e-f512d5624eed", "ApplicationUser", "user@gmail.com", false, null, false, null, "USER@GMAIL.COM", "USER", "AQAAAAEAACcQAAAAEBUWu+Cg4usKbeKRToIhJVNxLXi726wZFzeXVmnUTl+lnR9eMC9Kkcf9Z81jpOVNCg==", null, false, "f7c59e10-d1d3-42d9-8e6c-40f8824126ee", false, "user" },
                    { "9ca9a450-373d-4b11-9229-74aed89bda23", 0, "0ae19341-637a-4ea9-890d-b23f496b843c", "ApplicationUser", "admin@gmail.com", false, null, false, null, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAEAACcQAAAAEL3Ti2DLnAM7vSj/pJ6wAUq6IxpNsSTyLyuE61P3FSjH4Bb35yEdf5kWlnKyTvP8Ew==", null, false, "03f83181-9ee0-4a13-adc5-6c2704a352c5", false, "admin" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "9c8cde1f-6728-41d4-ad53-52078e55a56e", "9ca9a450-373d-4b11-9229-74aed89bda23" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "5635b31d-abe8-4686-8e10-3cfe48c1962e", "ed8ed912-c3df-4352-a96f-8dc8f99cb25f" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "9c8cde1f-6728-41d4-ad53-52078e55a56e", "9ca9a450-373d-4b11-9229-74aed89bda23" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "5635b31d-abe8-4686-8e10-3cfe48c1962e", "ed8ed912-c3df-4352-a96f-8dc8f99cb25f" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5635b31d-abe8-4686-8e10-3cfe48c1962e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9c8cde1f-6728-41d4-ad53-52078e55a56e");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9ca9a450-373d-4b11-9229-74aed89bda23");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ed8ed912-c3df-4352-a96f-8dc8f99cb25f");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e27274b2-c539-4d1b-95b7-657ad90e23cf", "878f563f-7efe-45a7-8a12-d3bbab9d4447", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "07f2660c-7827-44e0-b392-d9ac0bfc1301", "688b13d7-d9f6-49e3-a89e-5625ff28b87d", "User", "USER" });
        }
    }
}
