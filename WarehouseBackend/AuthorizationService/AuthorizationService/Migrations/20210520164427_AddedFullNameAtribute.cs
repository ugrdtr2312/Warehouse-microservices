using Microsoft.EntityFrameworkCore.Migrations;

namespace AuthorizationService.Migrations
{
    public partial class AddedFullNameAtribute : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                values: new object[,]
                {
                    { "dddbebf2-556e-40d9-9a6f-94ceb47ea596", "ebc34087-46a3-4256-8223-c8bb08f9189a", "Admin", "ADMIN" },
                    { "10b95d3c-a82d-4242-b1fd-d8d8f3e2e399", "3c658dd5-4e32-435f-9f91-cd3a38c13e06", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "56566b78-7672-46e1-a240-9abc06879241", 0, "32b26428-0be6-473d-a22c-b107693cf376", "ApplicationUser", "user@gmail.com", false, "Mr. User", false, null, "USER@GMAIL.COM", "USER", "AQAAAAEAACcQAAAAEMfQ9GEePG2rlKR2y9lF1SVRLSaJ1JvhBqSjs895DFUrYcNJHnE6r7xy3I0lDpv7qQ==", null, false, "1591481c-b469-486b-8d9b-db15da6d5ed2", false, "user" },
                    { "09e235cd-c11e-42e9-934e-4cba41aeaa76", 0, "0ab5774c-8126-4c9a-9086-09f3dfcc328c", "ApplicationUser", "admin@gmail.com", false, "Mr. Admin", false, null, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAEAACcQAAAAEA1IGznaUL38/tmo1Fvd4A8kEeGaJSS+0ryibwbQLRAWyv/Quo79DMYvEvZMO8p2jQ==", null, false, "62ead46e-92bf-478d-9142-aa5bd36b9613", false, "admin" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "dddbebf2-556e-40d9-9a6f-94ceb47ea596", "09e235cd-c11e-42e9-934e-4cba41aeaa76" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "10b95d3c-a82d-4242-b1fd-d8d8f3e2e399", "56566b78-7672-46e1-a240-9abc06879241" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "dddbebf2-556e-40d9-9a6f-94ceb47ea596", "09e235cd-c11e-42e9-934e-4cba41aeaa76" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "10b95d3c-a82d-4242-b1fd-d8d8f3e2e399", "56566b78-7672-46e1-a240-9abc06879241" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "10b95d3c-a82d-4242-b1fd-d8d8f3e2e399");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dddbebf2-556e-40d9-9a6f-94ceb47ea596");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "09e235cd-c11e-42e9-934e-4cba41aeaa76");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "56566b78-7672-46e1-a240-9abc06879241");

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
    }
}
