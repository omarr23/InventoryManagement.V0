using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InventoryManagement.DAL.Migrations
{
    /// <inheritdoc />
    public partial class seeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2a232611-66ef-46d8-bedb-d25f03d34e55", "c785fce2-fa51-48ea-8aa9-cf5cd7d19d93", "ADMIN", "ADMIN" },
                    { "9af5afac-cc72-4216-be76-c2b550595a7f", "312adb3a-6281-469c-852e-597d97576d6e", "MANAGER", "MANAGER" },
                    { "e4cfd596-334a-4f00-811c-7b2a188306e5", "390e51b5-64b2-483f-940c-b3d360840d3b", "USER", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2a232611-66ef-46d8-bedb-d25f03d34e55");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9af5afac-cc72-4216-be76-c2b550595a7f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e4cfd596-334a-4f00-811c-7b2a188306e5");
        }
    }
}
