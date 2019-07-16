using Microsoft.EntityFrameworkCore.Migrations;

namespace spices.Data.Migrations
{
    public partial class addSubCategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubCategory_Category_CategotyId",
                table: "SubCategory");

            migrationBuilder.DropIndex(
                name: "IX_SubCategory_CategotyId",
                table: "SubCategory");

            migrationBuilder.DropColumn(
                name: "CategotyId",
                table: "SubCategory");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategory_CategoryId",
                table: "SubCategory",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubCategory_Category_CategoryId",
                table: "SubCategory",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubCategory_Category_CategoryId",
                table: "SubCategory");

            migrationBuilder.DropIndex(
                name: "IX_SubCategory_CategoryId",
                table: "SubCategory");

            migrationBuilder.AddColumn<int>(
                name: "CategotyId",
                table: "SubCategory",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubCategory_CategotyId",
                table: "SubCategory",
                column: "CategotyId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubCategory_Category_CategotyId",
                table: "SubCategory",
                column: "CategotyId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
