using Microsoft.EntityFrameworkCore.Migrations;

namespace FleaMarket.Migrations
{
    public partial class IsCoverWasAddedToImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Images_CoverId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_CoverId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "CoverId",
                table: "Items");

            migrationBuilder.AddColumn<bool>(
                name: "IsCover",
                table: "Images",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCover",
                table: "Images");

            migrationBuilder.AddColumn<int>(
                name: "CoverId",
                table: "Items",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Items_CoverId",
                table: "Items",
                column: "CoverId",
                unique: true,
                filter: "[CoverId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Images_CoverId",
                table: "Items",
                column: "CoverId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
