using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class tfsv7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_User_UserId",
                table: "Product");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Product",
                newName: "ProviderUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Product_UserId",
                table: "Product",
                newName: "IX_Product_ProviderUserId");

            migrationBuilder.AddColumn<int>(
                name: "DistributorUserId",
                table: "Product",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Product_DistributorUserId",
                table: "Product",
                column: "DistributorUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_User_DistributorUserId",
                table: "Product",
                column: "DistributorUserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_User_ProviderUserId",
                table: "Product",
                column: "ProviderUserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_User_DistributorUserId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_User_ProviderUserId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_DistributorUserId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "DistributorUserId",
                table: "Product");

            migrationBuilder.RenameColumn(
                name: "ProviderUserId",
                table: "Product",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Product_ProviderUserId",
                table: "Product",
                newName: "IX_Product_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_User_UserId",
                table: "Product",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
