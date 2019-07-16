using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class gianttv3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Food_Premises_FarmId",
                table: "Food");

            migrationBuilder.DropForeignKey(
                name: "FK_Food_Premises_ProviderId",
                table: "Food");

            migrationBuilder.DropIndex(
                name: "IX_Food_ProviderId",
                table: "Food");

            migrationBuilder.DropColumn(
                name: "ProviderId",
                table: "Food");

            migrationBuilder.AddColumn<DateTime>(
                name: "Dob",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Users",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "Treatment",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Treatment",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CertificationDate",
                table: "Transaction",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "Transaction",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "RejectedReason",
                table: "Transaction",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VeterinaryId",
                table: "Transaction",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "Food",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ProviderFood",
                columns: table => new
                {
                    PremisesId = table.Column<int>(nullable: false),
                    FoodId = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProviderFood", x => new { x.FoodId, x.PremisesId });
                    table.ForeignKey(
                        name: "FK_ProviderFood_Food_FoodId",
                        column: x => x.FoodId,
                        principalTable: "Food",
                        principalColumn: "FoodId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ProviderFood_Premises_PremisesId",
                        column: x => x.PremisesId,
                        principalTable: "Premises",
                        principalColumn: "PremisesId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.UpdateData(
                table: "FoodDetailType",
                keyColumn: "TypeId",
                keyValue: 2,
                column: "Name",
                value: "Thức ăn");

            migrationBuilder.UpdateData(
                table: "FoodDetailType",
                keyColumn: "TypeId",
                keyValue: 3,
                column: "Name",
                value: "Vac-xin");

            migrationBuilder.UpdateData(
                table: "FoodDetailType",
                keyColumn: "TypeId",
                keyValue: 4,
                column: "Name",
                value: "Kiểm định");

            migrationBuilder.UpdateData(
                table: "FoodDetailType",
                keyColumn: "TypeId",
                keyValue: 5,
                column: "Name",
                value: "Nhà cung cấp");

            migrationBuilder.UpdateData(
                table: "FoodDetailType",
                keyColumn: "TypeId",
                keyValue: 6,
                column: "Name",
                value: "Quy trình xử lý");

            migrationBuilder.UpdateData(
                table: "FoodDetailType",
                keyColumn: "TypeId",
                keyValue: 7,
                column: "Name",
                value: "Đóng gói");

            migrationBuilder.CreateIndex(
                name: "IX_Treatment_CreatedById",
                table: "Treatment",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_CreatedById",
                table: "Transaction",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_VeterinaryId",
                table: "Transaction",
                column: "VeterinaryId");

            migrationBuilder.CreateIndex(
                name: "IX_Food_CreatedById",
                table: "Food",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProviderFood_PremisesId",
                table: "ProviderFood",
                column: "PremisesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Food_Users_CreatedById",
                table: "Food",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Food_Premises_FarmId",
                table: "Food",
                column: "FarmId",
                principalTable: "Premises",
                principalColumn: "PremisesId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Users_CreatedById",
                table: "Transaction",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Users_VeterinaryId",
                table: "Transaction",
                column: "VeterinaryId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Treatment_Users_CreatedById",
                table: "Treatment",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Food_Users_CreatedById",
                table: "Food");

            migrationBuilder.DropForeignKey(
                name: "FK_Food_Premises_FarmId",
                table: "Food");

            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Users_CreatedById",
                table: "Transaction");

            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Users_VeterinaryId",
                table: "Transaction");

            migrationBuilder.DropForeignKey(
                name: "FK_Treatment_Users_CreatedById",
                table: "Treatment");

            migrationBuilder.DropTable(
                name: "ProviderFood");

            migrationBuilder.DropIndex(
                name: "IX_Treatment_CreatedById",
                table: "Treatment");

            migrationBuilder.DropIndex(
                name: "IX_Transaction_CreatedById",
                table: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_Transaction_VeterinaryId",
                table: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_Food_CreatedById",
                table: "Food");

            migrationBuilder.DropColumn(
                name: "Dob",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Treatment");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Treatment");

            migrationBuilder.DropColumn(
                name: "CertificationDate",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "RejectedReason",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "VeterinaryId",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Food");

            migrationBuilder.AddColumn<int>(
                name: "ProviderId",
                table: "Food",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "FoodDetailType",
                keyColumn: "TypeId",
                keyValue: 2,
                column: "Name",
                value: "Thêm thông tin thức ăn");

            migrationBuilder.UpdateData(
                table: "FoodDetailType",
                keyColumn: "TypeId",
                keyValue: 3,
                column: "Name",
                value: "Thêm thông tin vac-xin");

            migrationBuilder.UpdateData(
                table: "FoodDetailType",
                keyColumn: "TypeId",
                keyValue: 4,
                column: "Name",
                value: "Thêm thông tin kiểm định");

            migrationBuilder.UpdateData(
                table: "FoodDetailType",
                keyColumn: "TypeId",
                keyValue: 5,
                column: "Name",
                value: "Thêm nhà cung cấp");

            migrationBuilder.UpdateData(
                table: "FoodDetailType",
                keyColumn: "TypeId",
                keyValue: 6,
                column: "Name",
                value: "Thêm quy trình xử lý");

            migrationBuilder.UpdateData(
                table: "FoodDetailType",
                keyColumn: "TypeId",
                keyValue: 7,
                column: "Name",
                value: "Thêm thông tin đóng gói");

            migrationBuilder.CreateIndex(
                name: "IX_Food_ProviderId",
                table: "Food",
                column: "ProviderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Food_Premises_FarmId",
                table: "Food",
                column: "FarmId",
                principalTable: "Premises",
                principalColumn: "PremisesId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Food_Premises_ProviderId",
                table: "Food",
                column: "ProviderId",
                principalTable: "Premises",
                principalColumn: "PremisesId",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
