using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class gianttv10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "VaccinFood",
                nullable: false,
                defaultValueSql: "DBO.dReturnDate(getdate())",
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "getdate()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Vaccin",
                nullable: false,
                defaultValueSql: "DBO.dReturnDate(getdate())",
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "getdate()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Users",
                nullable: false,
                defaultValueSql: "DBO.dReturnDate(getdate())",
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "getdate()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Treatment",
                nullable: false,
                defaultValueSql: "DBO.dReturnDate(getdate())",
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "getdate()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Transaction",
                nullable: false,
                defaultValueSql: "DBO.dReturnDate(getdate())",
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "getdate()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ProviderFood",
                nullable: false,
                defaultValueSql: "DBO.dReturnDate(getdate())",
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "getdate()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Premises",
                nullable: false,
                defaultValueSql: "DBO.dReturnDate(getdate())",
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "getdate()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "FoodDetail",
                nullable: false,
                defaultValueSql: "DBO.dReturnDate(getdate())",
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "getdate()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Food",
                nullable: false,
                defaultValueSql: "DBO.dReturnDate(getdate())",
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "getdate()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "FeedingFood",
                nullable: false,
                defaultValueSql: "DBO.dReturnDate(getdate())",
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "getdate()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Feeding",
                nullable: false,
                defaultValueSql: "DBO.dReturnDate(getdate())",
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "getdate()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "DistributorFood",
                nullable: false,
                defaultValueSql: "DBO.dReturnDate(getdate())",
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "getdate()");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "VaccinFood",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "DBO.dReturnDate(getdate())");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Vaccin",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "DBO.dReturnDate(getdate())");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Users",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "DBO.dReturnDate(getdate())");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Treatment",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "DBO.dReturnDate(getdate())");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Transaction",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "DBO.dReturnDate(getdate())");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ProviderFood",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "DBO.dReturnDate(getdate())");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Premises",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "DBO.dReturnDate(getdate())");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "FoodDetail",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "DBO.dReturnDate(getdate())");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Food",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "DBO.dReturnDate(getdate())");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "FeedingFood",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "DBO.dReturnDate(getdate())");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Feeding",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "DBO.dReturnDate(getdate())");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "DistributorFood",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "DBO.dReturnDate(getdate())");
        }
    }
}
