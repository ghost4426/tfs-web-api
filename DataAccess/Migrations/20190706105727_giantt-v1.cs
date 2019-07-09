using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class gianttv1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FoodDetailType",
                columns: table => new
                {
                    TypeId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodDetailType", x => x.TypeId);
                });

            migrationBuilder.CreateTable(
                name: "PremisesType",
                columns: table => new
                {
                    TypeId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PremisesType", x => x.TypeId);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "TransactionStatus",
                columns: table => new
                {
                    StatusId = table.Column<int>(nullable: false),
                    Status = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionStatus", x => x.StatusId);
                });

            migrationBuilder.CreateTable(
                name: "Premises",
                columns: table => new
                {
                    PremisesId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    TypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Premises", x => x.PremisesId);
                    table.ForeignKey(
                        name: "FK_Premises_PremisesType_TypeId",
                        column: x => x.TypeId,
                        principalTable: "PremisesType",
                        principalColumn: "TypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Treatment",
                columns: table => new
                {
                    TreatmentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    TreatmentParentId = table.Column<int>(nullable: true),
                    PremisesId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Treatment", x => x.TreatmentId);
                    table.ForeignKey(
                        name: "FK_Treatment_Premises_PremisesId",
                        column: x => x.PremisesId,
                        principalTable: "Premises",
                        principalColumn: "PremisesId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Treatment_Treatment_TreatmentParentId",
                        column: x => x.TreatmentParentId,
                        principalTable: "Treatment",
                        principalColumn: "TreatmentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Username = table.Column<string>(nullable: true),
                    Fullname = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Salt = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    PhoneNo = table.Column<string>(nullable: true),
                    RoleId = table.Column<int>(nullable: false),
                    PremisesId = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_Premises_PremisesId",
                        column: x => x.PremisesId,
                        principalTable: "Premises",
                        principalColumn: "PremisesId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Food",
                columns: table => new
                {
                    FoodId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Breed = table.Column<string>(nullable: true),
                    CategoryId = table.Column<int>(nullable: false),
                    FarmId = table.Column<int>(nullable: false),
                    ProviderId = table.Column<int>(nullable: true),
                    TreatmentId = table.Column<int>(nullable: true),
                    IsFeeding = table.Column<bool>(nullable: false, defaultValue: false),
                    IsVaccination = table.Column<bool>(nullable: false, defaultValue: false),
                    IsCertification = table.Column<bool>(nullable: false, defaultValue: false),
                    IsTreatment = table.Column<bool>(nullable: false, defaultValue: false),
                    IsPackaging = table.Column<bool>(nullable: false, defaultValue: false),
                    CreatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Food", x => x.FoodId);
                    table.ForeignKey(
                        name: "FK_Food_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Food_Premises_FarmId",
                        column: x => x.FarmId,
                        principalTable: "Premises",
                        principalColumn: "PremisesId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Food_Premises_ProviderId",
                        column: x => x.ProviderId,
                        principalTable: "Premises",
                        principalColumn: "PremisesId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Food_Treatment_TreatmentId",
                        column: x => x.TreatmentId,
                        principalTable: "Treatment",
                        principalColumn: "TreatmentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DistributorFood",
                columns: table => new
                {
                    PremisesId = table.Column<int>(nullable: false),
                    FoodId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DistributorFood", x => new { x.FoodId, x.PremisesId });
                    table.ForeignKey(
                        name: "FK_DistributorFood_Food_FoodId",
                        column: x => x.FoodId,
                        principalTable: "Food",
                        principalColumn: "FoodId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DistributorFood_Premises_PremisesId",
                        column: x => x.PremisesId,
                        principalTable: "Premises",
                        principalColumn: "PremisesId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "FoodDetail",
                columns: table => new
                {
                    DetailId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TypeId = table.Column<int>(nullable: false),
                    BlockNumber = table.Column<int>(nullable: false),
                    TransactionHash = table.Column<string>(nullable: true),
                    FoodId = table.Column<int>(nullable: false),
                    CreatedById = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodDetail", x => x.DetailId);
                    table.ForeignKey(
                        name: "FK_FoodDetail_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FoodDetail_Food_FoodId",
                        column: x => x.FoodId,
                        principalTable: "Food",
                        principalColumn: "FoodId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_FoodDetail_FoodDetailType_TypeId",
                        column: x => x.TypeId,
                        principalTable: "FoodDetailType",
                        principalColumn: "TypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    TransactionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FarmId = table.Column<int>(nullable: false),
                    ProviderId = table.Column<int>(nullable: false),
                    FoodId = table.Column<int>(nullable: false),
                    StatusId = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ConfirmDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.TransactionId);
                    table.ForeignKey(
                        name: "FK_Transaction_Premises_FarmId",
                        column: x => x.FarmId,
                        principalTable: "Premises",
                        principalColumn: "PremisesId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Transaction_Food_FoodId",
                        column: x => x.FoodId,
                        principalTable: "Food",
                        principalColumn: "FoodId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transaction_Premises_ProviderId",
                        column: x => x.ProviderId,
                        principalTable: "Premises",
                        principalColumn: "PremisesId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Transaction_TransactionStatus_StatusId",
                        column: x => x.StatusId,
                        principalTable: "TransactionStatus",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DistributorFood_PremisesId",
                table: "DistributorFood",
                column: "PremisesId");

            migrationBuilder.CreateIndex(
                name: "IX_Food_CategoryId",
                table: "Food",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Food_FarmId",
                table: "Food",
                column: "FarmId");

            migrationBuilder.CreateIndex(
                name: "IX_Food_ProviderId",
                table: "Food",
                column: "ProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_Food_TreatmentId",
                table: "Food",
                column: "TreatmentId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodDetail_CreatedById",
                table: "FoodDetail",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_FoodDetail_FoodId",
                table: "FoodDetail",
                column: "FoodId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodDetail_TypeId",
                table: "FoodDetail",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Premises_TypeId",
                table: "Premises",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_FarmId",
                table: "Transaction",
                column: "FarmId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_FoodId",
                table: "Transaction",
                column: "FoodId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_ProviderId",
                table: "Transaction",
                column: "ProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_StatusId",
                table: "Transaction",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Treatment_PremisesId",
                table: "Treatment",
                column: "PremisesId");

            migrationBuilder.CreateIndex(
                name: "IX_Treatment_TreatmentParentId",
                table: "Treatment",
                column: "TreatmentParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_PremisesId",
                table: "Users",
                column: "PremisesId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DistributorFood");

            migrationBuilder.DropTable(
                name: "FoodDetail");

            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "FoodDetailType");

            migrationBuilder.DropTable(
                name: "Food");

            migrationBuilder.DropTable(
                name: "TransactionStatus");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Treatment");

            migrationBuilder.DropTable(
                name: "Premises");

            migrationBuilder.DropTable(
                name: "PremisesType");
        }
    }
}
