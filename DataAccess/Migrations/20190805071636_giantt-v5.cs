using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class gianttv5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Food_Users_CreatedById",
                table: "Food");

            migrationBuilder.DropForeignKey(
                name: "FK_FoodDetail_Users_CreatedById",
                table: "FoodDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Users_CreatedById",
                table: "Transaction");

            migrationBuilder.DropForeignKey(
                name: "FK_Treatment_Users_CreatedById",
                table: "Treatment");

            migrationBuilder.DropTable(
                name: "RegisterInfo");

            migrationBuilder.DropIndex(
                name: "IX_Transaction_CreatedById",
                table: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_Food_CreatedById",
                table: "Food");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "IsCertification",
                table: "Food");

            migrationBuilder.DropColumn(
                name: "IsFeeding",
                table: "Food");

            migrationBuilder.DropColumn(
                name: "IsPackaging",
                table: "Food");

            migrationBuilder.DropColumn(
                name: "IsTreatment",
                table: "Food");

            migrationBuilder.DropColumn(
                name: "IsVaccination",
                table: "Food");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Users",
                newName: "CreateDate");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Treatment",
                newName: "UpdateDate");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "Treatment",
                newName: "UpdateById");

            migrationBuilder.RenameIndex(
                name: "IX_Treatment_CreatedById",
                table: "Treatment",
                newName: "IX_Treatment_UpdateById");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Transaction",
                newName: "CreateDate");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "ProviderFood",
                newName: "CreateDate");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Premises",
                newName: "CreateDate");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "FoodDetail",
                newName: "CreateDate");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "FoodDetail",
                newName: "CreateById");

            migrationBuilder.RenameIndex(
                name: "IX_FoodDetail_CreatedById",
                table: "FoodDetail",
                newName: "IX_FoodDetail_CreateById");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Food",
                newName: "CreateDate");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "Food",
                newName: "FeedingId");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "DistributorFood",
                newName: "CreateDate");

            migrationBuilder.AddColumn<string>(
                name: "ActivationCode",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IsConfirmEmail",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreateById",
                table: "Treatment",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "Treatment",
                nullable: false,
                defaultValueSql: "getdate()");

            migrationBuilder.AddColumn<int>(
                name: "CreateById",
                table: "Transaction",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TreatmentId",
                table: "ProviderFood",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreateById",
                table: "Food",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Feeding",
                columns: table => new
                {
                    FeedingId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    PremisesId = table.Column<int>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    CreateById = table.Column<int>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    UpdateById = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feeding", x => x.FeedingId);
                    table.ForeignKey(
                        name: "FK_Feeding_Users_CreateById",
                        column: x => x.CreateById,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Feeding_Premises_PremisesId",
                        column: x => x.PremisesId,
                        principalTable: "Premises",
                        principalColumn: "PremisesId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Feeding_Users_UpdateById",
                        column: x => x.UpdateById,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Vaccin",
                columns: table => new
                {
                    VaccinId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    PremisesId = table.Column<int>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    CreateById = table.Column<int>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    UpdateById = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vaccin", x => x.VaccinId);
                    table.ForeignKey(
                        name: "FK_Vaccin_Users_CreateById",
                        column: x => x.CreateById,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Vaccin_Premises_PremisesId",
                        column: x => x.PremisesId,
                        principalTable: "Premises",
                        principalColumn: "PremisesId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Vaccin_Users_UpdateById",
                        column: x => x.UpdateById,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "FeedingFood",
                columns: table => new
                {
                    FeedingId = table.Column<int>(nullable: false),
                    FoodId = table.Column<int>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedingFood", x => new { x.FoodId, x.FeedingId });
                    table.ForeignKey(
                        name: "FK_FeedingFood_Feeding_FeedingId",
                        column: x => x.FeedingId,
                        principalTable: "Feeding",
                        principalColumn: "FeedingId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_FeedingFood_Food_FoodId",
                        column: x => x.FoodId,
                        principalTable: "Food",
                        principalColumn: "FoodId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "VaccinFood",
                columns: table => new
                {
                    VaccinId = table.Column<int>(nullable: false),
                    FoodId = table.Column<int>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VaccinFood", x => new { x.FoodId, x.VaccinId });
                    table.ForeignKey(
                        name: "FK_VaccinFood_Food_FoodId",
                        column: x => x.FoodId,
                        principalTable: "Food",
                        principalColumn: "FoodId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_VaccinFood_Vaccin_VaccinId",
                        column: x => x.VaccinId,
                        principalTable: "Vaccin",
                        principalColumn: "VaccinId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Treatment_CreateById",
                table: "Treatment",
                column: "CreateById");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_CreateById",
                table: "Transaction",
                column: "CreateById");

            migrationBuilder.CreateIndex(
                name: "IX_ProviderFood_TreatmentId",
                table: "ProviderFood",
                column: "TreatmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Food_CreateById",
                table: "Food",
                column: "CreateById");

            migrationBuilder.CreateIndex(
                name: "IX_Feeding_CreateById",
                table: "Feeding",
                column: "CreateById");

            migrationBuilder.CreateIndex(
                name: "IX_Feeding_PremisesId",
                table: "Feeding",
                column: "PremisesId");

            migrationBuilder.CreateIndex(
                name: "IX_Feeding_UpdateById",
                table: "Feeding",
                column: "UpdateById");

            migrationBuilder.CreateIndex(
                name: "IX_FeedingFood_FeedingId",
                table: "FeedingFood",
                column: "FeedingId");

            migrationBuilder.CreateIndex(
                name: "IX_Vaccin_CreateById",
                table: "Vaccin",
                column: "CreateById");

            migrationBuilder.CreateIndex(
                name: "IX_Vaccin_PremisesId",
                table: "Vaccin",
                column: "PremisesId");

            migrationBuilder.CreateIndex(
                name: "IX_Vaccin_UpdateById",
                table: "Vaccin",
                column: "UpdateById");

            migrationBuilder.CreateIndex(
                name: "IX_VaccinFood_VaccinId",
                table: "VaccinFood",
                column: "VaccinId");

            migrationBuilder.AddForeignKey(
                name: "FK_Food_Users_CreateById",
                table: "Food",
                column: "CreateById",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_FoodDetail_Users_CreateById",
                table: "FoodDetail",
                column: "CreateById",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_ProviderFood_Treatment_TreatmentId",
                table: "ProviderFood",
                column: "TreatmentId",
                principalTable: "Treatment",
                principalColumn: "TreatmentId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Users_CreateById",
                table: "Transaction",
                column: "CreateById",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Treatment_Users_CreateById",
                table: "Treatment",
                column: "CreateById",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Treatment_Users_UpdateById",
                table: "Treatment",
                column: "UpdateById",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Food_Users_CreateById",
                table: "Food");

            migrationBuilder.DropForeignKey(
                name: "FK_FoodDetail_Users_CreateById",
                table: "FoodDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_ProviderFood_Treatment_TreatmentId",
                table: "ProviderFood");

            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Users_CreateById",
                table: "Transaction");

            migrationBuilder.DropForeignKey(
                name: "FK_Treatment_Users_CreateById",
                table: "Treatment");

            migrationBuilder.DropForeignKey(
                name: "FK_Treatment_Users_UpdateById",
                table: "Treatment");

            migrationBuilder.DropTable(
                name: "FeedingFood");

            migrationBuilder.DropTable(
                name: "VaccinFood");

            migrationBuilder.DropTable(
                name: "Feeding");

            migrationBuilder.DropTable(
                name: "Vaccin");

            migrationBuilder.DropIndex(
                name: "IX_Treatment_CreateById",
                table: "Treatment");

            migrationBuilder.DropIndex(
                name: "IX_Transaction_CreateById",
                table: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_ProviderFood_TreatmentId",
                table: "ProviderFood");

            migrationBuilder.DropIndex(
                name: "IX_Food_CreateById",
                table: "Food");

            migrationBuilder.DropColumn(
                name: "ActivationCode",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsConfirmEmail",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CreateById",
                table: "Treatment");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "Treatment");

            migrationBuilder.DropColumn(
                name: "CreateById",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "TreatmentId",
                table: "ProviderFood");

            migrationBuilder.DropColumn(
                name: "CreateById",
                table: "Food");

            migrationBuilder.RenameColumn(
                name: "CreateDate",
                table: "Users",
                newName: "CreatedDate");

            migrationBuilder.RenameColumn(
                name: "UpdateDate",
                table: "Treatment",
                newName: "CreatedDate");

            migrationBuilder.RenameColumn(
                name: "UpdateById",
                table: "Treatment",
                newName: "CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_Treatment_UpdateById",
                table: "Treatment",
                newName: "IX_Treatment_CreatedById");

            migrationBuilder.RenameColumn(
                name: "CreateDate",
                table: "Transaction",
                newName: "CreatedDate");

            migrationBuilder.RenameColumn(
                name: "CreateDate",
                table: "ProviderFood",
                newName: "CreatedDate");

            migrationBuilder.RenameColumn(
                name: "CreateDate",
                table: "Premises",
                newName: "CreatedDate");

            migrationBuilder.RenameColumn(
                name: "CreateDate",
                table: "FoodDetail",
                newName: "CreatedDate");

            migrationBuilder.RenameColumn(
                name: "CreateById",
                table: "FoodDetail",
                newName: "CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_FoodDetail_CreateById",
                table: "FoodDetail",
                newName: "IX_FoodDetail_CreatedById");

            migrationBuilder.RenameColumn(
                name: "FeedingId",
                table: "Food",
                newName: "CreatedById");

            migrationBuilder.RenameColumn(
                name: "CreateDate",
                table: "Food",
                newName: "CreatedDate");

            migrationBuilder.RenameColumn(
                name: "CreateDate",
                table: "DistributorFood",
                newName: "CreatedDate");

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "Transaction",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsCertification",
                table: "Food",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFeeding",
                table: "Food",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPackaging",
                table: "Food",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsTreatment",
                table: "Food",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsVaccination",
                table: "Food",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "RegisterInfo",
                columns: table => new
                {
                    RegisterId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    Email = table.Column<string>(nullable: false),
                    Fullname = table.Column<string>(nullable: false),
                    IsConfirm = table.Column<bool>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    PremisesAddress = table.Column<string>(nullable: false),
                    PremisesName = table.Column<string>(nullable: false),
                    PremisesTypeId = table.Column<int>(nullable: false),
                    Username = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegisterInfo", x => x.RegisterId);
                    table.ForeignKey(
                        name: "FK_RegisterInfo_PremisesType_PremisesTypeId",
                        column: x => x.PremisesTypeId,
                        principalTable: "PremisesType",
                        principalColumn: "TypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_CreatedById",
                table: "Transaction",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Food_CreatedById",
                table: "Food",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_RegisterInfo_PremisesTypeId",
                table: "RegisterInfo",
                column: "PremisesTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Food_Users_CreatedById",
                table: "Food",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FoodDetail_Users_CreatedById",
                table: "FoodDetail",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Users_CreatedById",
                table: "Transaction",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Treatment_Users_CreatedById",
                table: "Treatment",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
