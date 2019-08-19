using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class gianttv13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Users_CreateById",
                table: "Transaction");

            migrationBuilder.AlterColumn<int>(
                name: "CreateById",
                table: "Transaction",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CertificationNumber",
                table: "Transaction",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPacked",
                table: "ProviderFood",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsTreatmented",
                table: "ProviderFood",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Premises",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "CertificationNumber",
                table: "Food",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsReadyForSale",
                table: "Food",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSoldOut",
                table: "Food",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Users_CreateById",
                table: "Transaction",
                column: "CreateById",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Users_CreateById",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "CertificationNumber",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "IsPacked",
                table: "ProviderFood");

            migrationBuilder.DropColumn(
                name: "IsTreatmented",
                table: "ProviderFood");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Premises");

            migrationBuilder.DropColumn(
                name: "CertificationNumber",
                table: "Food");

            migrationBuilder.DropColumn(
                name: "IsReadyForSale",
                table: "Food");

            migrationBuilder.DropColumn(
                name: "IsSoldOut",
                table: "Food");

            migrationBuilder.AlterColumn<int>(
                name: "CreateById",
                table: "Transaction",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Users_CreateById",
                table: "Transaction",
                column: "CreateById",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
