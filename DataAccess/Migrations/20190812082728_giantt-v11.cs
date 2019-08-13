using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class gianttv11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VaccinFood");

            migrationBuilder.DropColumn(
                name: "Dob",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "VaccinId",
                table: "Vaccin",
                newName: "VaccineId");

            migrationBuilder.CreateTable(
                name: "VaccineFood",
                columns: table => new
                {
                    VaccineId = table.Column<int>(nullable: false),
                    FoodId = table.Column<int>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "DBO.dReturnDate(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VaccineFood", x => new { x.FoodId, x.VaccineId });
                    table.ForeignKey(
                        name: "FK_VaccineFood_Food_FoodId",
                        column: x => x.FoodId,
                        principalTable: "Food",
                        principalColumn: "FoodId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VaccineFood_Vaccin_VaccineId",
                        column: x => x.VaccineId,
                        principalTable: "Vaccin",
                        principalColumn: "VaccineId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VaccineFood_VaccineId",
                table: "VaccineFood",
                column: "VaccineId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VaccineFood");

            migrationBuilder.RenameColumn(
                name: "VaccineId",
                table: "Vaccin",
                newName: "VaccinId");

            migrationBuilder.AddColumn<DateTime>(
                name: "Dob",
                table: "Users",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "VaccinFood",
                columns: table => new
                {
                    FoodId = table.Column<int>(nullable: false),
                    VaccinId = table.Column<int>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "DBO.dReturnDate(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VaccinFood", x => new { x.FoodId, x.VaccinId });
                    table.ForeignKey(
                        name: "FK_VaccinFood_Food_FoodId",
                        column: x => x.FoodId,
                        principalTable: "Food",
                        principalColumn: "FoodId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VaccinFood_Vaccin_VaccinId",
                        column: x => x.VaccinId,
                        principalTable: "Vaccin",
                        principalColumn: "VaccinId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VaccinFood_VaccinId",
                table: "VaccinFood",
                column: "VaccinId");
        }
    }
}
