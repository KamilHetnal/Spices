using Microsoft.EntityFrameworkCore.Migrations;

namespace spices.Data.Migrations
{
    public partial class addMenuItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_MenuItem_CategoryId",
                table: "MenuItem",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItem_Category_CategoryId",
                table: "MenuItem",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuItem_Category_CategoryId",
                table: "MenuItem");

            migrationBuilder.DropIndex(
                name: "IX_MenuItem_CategoryId",
                table: "MenuItem");
        }
    }
}
