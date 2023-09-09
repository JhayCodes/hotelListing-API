using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelListing.Migrations
{
    public partial class AddedDefaultRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "79fb00de-1f50-4a9f-8be0-0fbc5d2a271e", "76d62340-afc9-4d71-836e-eb09c77842c0", "Administratore", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b388b904-4e6e-4d32-94b7-a5cfe4b64f06", "67a323e0-2e5c-440d-b0cc-983263c907e8", "User", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "79fb00de-1f50-4a9f-8be0-0fbc5d2a271e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b388b904-4e6e-4d32-94b7-a5cfe4b64f06");
        }
    }
}
