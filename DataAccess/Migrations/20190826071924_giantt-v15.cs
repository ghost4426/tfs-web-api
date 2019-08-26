using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class gianttv15 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_VaccineFood",
                table: "VaccineFood");

            migrationBuilder.AddColumn<int>(
                name: "VaccineFoodId",
                table: "VaccineFood",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<DateTime>(
                name: "VaccineDate",
                table: "VaccineFood",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_VaccineFood",
                table: "VaccineFood",
                column: "VaccineFoodId");

            migrationBuilder.CreateIndex(
                name: "IX_VaccineFood_FoodId",
                table: "VaccineFood",
                column: "FoodId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_VaccineFood",
                table: "VaccineFood");

            migrationBuilder.DropIndex(
                name: "IX_VaccineFood_FoodId",
                table: "VaccineFood");

            migrationBuilder.DropColumn(
                name: "VaccineFoodId",
                table: "VaccineFood");

            migrationBuilder.DropColumn(
                name: "VaccineDate",
                table: "VaccineFood");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VaccineFood",
                table: "VaccineFood",
                columns: new[] { "FoodId", "VaccineId" });
        }
    }
}
