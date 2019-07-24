using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class gianttv4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Users_VeterinaryId",
                table: "Transaction");

            migrationBuilder.RenameColumn(
                name: "CertificationDate",
                table: "Transaction",
                newName: "VerifyDate");

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "Users",
                nullable: true,
                defaultValue: "/app-assets/images/avatar.jpg",
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<int>(
                name: "VeterinaryId",
                table: "Transaction",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "ProviderComment",
                table: "Transaction",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VeterinaryComment",
                table: "Transaction",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "CategoryId",
                keyValue: 1,
                column: "Name",
                value: "Heo");

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "CategoryId",
                keyValue: 2,
                column: "Name",
                value: "Gà");

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "CategoryId",
                keyValue: 3,
                column: "Name",
                value: "Bò");

            migrationBuilder.UpdateData(
                table: "FoodDetailType",
                keyColumn: "TypeId",
                keyValue: 3,
                column: "Name",
                value: "Vac-xin");

            migrationBuilder.UpdateData(
                table: "FoodDetailType",
                keyColumn: "TypeId",
                keyValue: 7,
                column: "Name",
                value: "Đóng gói");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Users_VeterinaryId",
                table: "Transaction",
                column: "VeterinaryId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Users_VeterinaryId",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "ProviderComment",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "VeterinaryComment",
                table: "Transaction");

            migrationBuilder.RenameColumn(
                name: "VerifyDate",
                table: "Transaction",
                newName: "CertificationDate");

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "Users",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true,
                oldDefaultValue: "/app-assets/images/avatar.jpg");

            migrationBuilder.AlterColumn<int>(
                name: "VeterinaryId",
                table: "Transaction",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "CategoryId",
                keyValue: 1,
                column: "Name",
                value: "Thịt Heo");

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "CategoryId",
                keyValue: 2,
                column: "Name",
                value: "Thịt Gà");

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "CategoryId",
                keyValue: 3,
                column: "Name",
                value: "Thịt Bò");

            migrationBuilder.UpdateData(
                table: "FoodDetailType",
                keyColumn: "TypeId",
                keyValue: 3,
                column: "Name",
                value: "ac-xin");

            migrationBuilder.UpdateData(
                table: "FoodDetailType",
                keyColumn: "TypeId",
                keyValue: 7,
                column: "Name",
                value: "Đónng gói");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Users_VeterinaryId",
                table: "Transaction",
                column: "VeterinaryId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
