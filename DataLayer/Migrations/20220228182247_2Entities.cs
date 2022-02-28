using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class _2Entities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "WaterSourcePictures",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2022, 2, 28, 18, 22, 47, 43, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "WaterSourceVariants",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2022, 2, 28, 18, 22, 47, 43, DateTimeKind.Utc));

            migrationBuilder.InsertData(
                table: "WaterSourceVariants",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000002"), new DateTime(2022, 2, 28, 18, 22, 47, 43, DateTimeKind.Utc), null, "Now with design for dogs", "Doggie" },
                    { new Guid("00000000-0000-0000-0000-000000000003"), new DateTime(2022, 2, 28, 18, 22, 47, 43, DateTimeKind.Utc), null, "Imagined in another time", "Old time" }
                });

            migrationBuilder.InsertData(
                table: "WaterSourcePictures",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "Uri", "WaterSourcePlaceId", "WaterSourceVariantId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000002"), new DateTime(2022, 2, 28, 18, 22, 47, 43, DateTimeKind.Utc), null, "Pictures/Default/Dog1.jpg", null, new Guid("00000000-0000-0000-0000-000000000002") });

            migrationBuilder.InsertData(
                table: "WaterSourcePictures",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "Uri", "WaterSourcePlaceId", "WaterSourceVariantId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000003"), new DateTime(2022, 2, 28, 18, 22, 47, 43, DateTimeKind.Utc), null, "Pictures/Default/OldTime1.jpg", null, new Guid("00000000-0000-0000-0000-000000000003") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "WaterSourcePictures",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "WaterSourcePictures",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "WaterSourceVariants",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "WaterSourceVariants",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"));

            migrationBuilder.UpdateData(
                table: "WaterSourcePictures",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2022, 2, 15, 20, 4, 43, 669, DateTimeKind.Utc).AddTicks(656));

            migrationBuilder.UpdateData(
                table: "WaterSourceVariants",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2022, 2, 15, 20, 4, 43, 668, DateTimeKind.Utc).AddTicks(845));
        }
    }
}
