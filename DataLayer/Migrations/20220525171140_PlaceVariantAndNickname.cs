using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class PlaceVariantAndNickname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Nickname",
                table: "WaterSourcePlaces",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "WaterSourceVariantId",
                table: "WaterSourcePlaces",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_WaterSourcePlaces_WaterSourceVariantId",
                table: "WaterSourcePlaces",
                column: "WaterSourceVariantId");

            migrationBuilder.AddForeignKey(
                name: "FK_WaterSourcePlaces_WaterSourceVariants_WaterSourceVariantId",
                table: "WaterSourcePlaces",
                column: "WaterSourceVariantId",
                principalTable: "WaterSourceVariants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WaterSourcePlaces_WaterSourceVariants_WaterSourceVariantId",
                table: "WaterSourcePlaces");

            migrationBuilder.DropIndex(
                name: "IX_WaterSourcePlaces_WaterSourceVariantId",
                table: "WaterSourcePlaces");

            migrationBuilder.DropColumn(
                name: "Nickname",
                table: "WaterSourcePlaces");

            migrationBuilder.DropColumn(
                name: "WaterSourceVariantId",
                table: "WaterSourcePlaces");
        }
    }
}
