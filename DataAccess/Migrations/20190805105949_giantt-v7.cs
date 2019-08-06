using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class gianttv7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProviderComment",
                table: "Transaction",
                newName: "ReceiverComment");

            migrationBuilder.AddColumn<int>(
                name: "RejectById",
                table: "Transaction",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_RejectById",
                table: "Transaction",
                column: "RejectById");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Users_RejectById",
                table: "Transaction",
                column: "RejectById",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Users_RejectById",
                table: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_Transaction_RejectById",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "RejectById",
                table: "Transaction");

            migrationBuilder.RenameColumn(
                name: "ReceiverComment",
                table: "Transaction",
                newName: "ProviderComment");
        }
    }
}
