using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace DataLayer.Migrations
{
    public partial class SeedDataTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "WaterSourceVariants",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "Description", "Name" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000001"), new DateTime(2022, 2, 15, 20, 4, 43, 668, DateTimeKind.Utc).AddTicks(845), null, "Simple style but effective at quenching thirst", "Classic" });

            migrationBuilder.InsertData(
                table: "WaterSourcePictures",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "Uri", "WaterSourcePlaceId", "WaterSourceVariantId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000001"), new DateTime(2022, 2, 15, 20, 4, 43, 669, DateTimeKind.Utc).AddTicks(656), null, "Pictures/Default/Classic1.jpg", null, new Guid("00000000-0000-0000-0000-000000000001") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "WaterSourcePictures",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "WaterSourceVariants",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"));
        }
    }
}
