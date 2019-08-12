using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class gianttv12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vaccin_Users_CreateById",
                table: "Vaccin");

            migrationBuilder.DropForeignKey(
                name: "FK_Vaccin_Premises_PremisesId",
                table: "Vaccin");

            migrationBuilder.DropForeignKey(
                name: "FK_Vaccin_Users_UpdateById",
                table: "Vaccin");

            migrationBuilder.DropForeignKey(
                name: "FK_VaccineFood_Vaccin_VaccineId",
                table: "VaccineFood");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vaccin",
                table: "Vaccin");

            migrationBuilder.DropColumn(
                name: "BlockNumber",
                table: "FoodDetail");

            migrationBuilder.RenameTable(
                name: "Vaccin",
                newName: "Vaccine");

            migrationBuilder.RenameIndex(
                name: "IX_Vaccin_UpdateById",
                table: "Vaccine",
                newName: "IX_Vaccine_UpdateById");

            migrationBuilder.RenameIndex(
                name: "IX_Vaccin_PremisesId",
                table: "Vaccine",
                newName: "IX_Vaccine_PremisesId");

            migrationBuilder.RenameIndex(
                name: "IX_Vaccin_CreateById",
                table: "Vaccine",
                newName: "IX_Vaccine_CreateById");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vaccine",
                table: "Vaccine",
                column: "VaccineId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vaccine_Users_CreateById",
                table: "Vaccine",
                column: "CreateById",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Vaccine_Premises_PremisesId",
                table: "Vaccine",
                column: "PremisesId",
                principalTable: "Premises",
                principalColumn: "PremisesId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Vaccine_Users_UpdateById",
                table: "Vaccine",
                column: "UpdateById",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_VaccineFood_Vaccine_VaccineId",
                table: "VaccineFood",
                column: "VaccineId",
                principalTable: "Vaccine",
                principalColumn: "VaccineId",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vaccine_Users_CreateById",
                table: "Vaccine");

            migrationBuilder.DropForeignKey(
                name: "FK_Vaccine_Premises_PremisesId",
                table: "Vaccine");

            migrationBuilder.DropForeignKey(
                name: "FK_Vaccine_Users_UpdateById",
                table: "Vaccine");

            migrationBuilder.DropForeignKey(
                name: "FK_VaccineFood_Vaccine_VaccineId",
                table: "VaccineFood");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vaccine",
                table: "Vaccine");

            migrationBuilder.RenameTable(
                name: "Vaccine",
                newName: "Vaccin");

            migrationBuilder.RenameIndex(
                name: "IX_Vaccine_UpdateById",
                table: "Vaccin",
                newName: "IX_Vaccin_UpdateById");

            migrationBuilder.RenameIndex(
                name: "IX_Vaccine_PremisesId",
                table: "Vaccin",
                newName: "IX_Vaccin_PremisesId");

            migrationBuilder.RenameIndex(
                name: "IX_Vaccine_CreateById",
                table: "Vaccin",
                newName: "IX_Vaccin_CreateById");

            migrationBuilder.AddColumn<int>(
                name: "BlockNumber",
                table: "FoodDetail",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vaccin",
                table: "Vaccin",
                column: "VaccineId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vaccin_Users_CreateById",
                table: "Vaccin",
                column: "CreateById",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vaccin_Premises_PremisesId",
                table: "Vaccin",
                column: "PremisesId",
                principalTable: "Premises",
                principalColumn: "PremisesId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vaccin_Users_UpdateById",
                table: "Vaccin",
                column: "UpdateById",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VaccineFood_Vaccin_VaccineId",
                table: "VaccineFood",
                column: "VaccineId",
                principalTable: "Vaccin",
                principalColumn: "VaccineId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
