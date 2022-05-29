using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class OmittedConnections : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WaterSourceContributions_WaterSourcePlaces_RelatedContributionId",
                table: "WaterSourceContributions");

            migrationBuilder.AddForeignKey(
                name: "FK_WaterSourceContributions_WaterSourceContributions_RelatedContributionId",
                table: "WaterSourceContributions",
                column: "RelatedContributionId",
                principalTable: "WaterSourceContributions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WaterSourceContributions_WaterSourceContributions_RelatedContributionId",
                table: "WaterSourceContributions");

            migrationBuilder.AddForeignKey(
                name: "FK_WaterSourceContributions_WaterSourcePlaces_RelatedContributionId",
                table: "WaterSourceContributions",
                column: "RelatedContributionId",
                principalTable: "WaterSourcePlaces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
