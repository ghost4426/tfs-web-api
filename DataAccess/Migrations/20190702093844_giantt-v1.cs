using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class gianttv1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Material");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "MaterialCategories");

            migrationBuilder.AddColumn<int>(
                name: "PremisesId",
                table: "User",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PremisesType",
                columns: table => new
                {
                    TypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PremisesType", x => x.TypeId);
                });

            migrationBuilder.CreateTable(
                name: "TransactionStatus",
                columns: table => new
                {
                    StatusId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
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
                    ParentTreatmentId = table.Column<int>(nullable: true),
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
                });

            migrationBuilder.CreateTable(
                name: "Food",
                columns: table => new
                {
                    FoodId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CategoriesId = table.Column<int>(nullable: false),
                    FarmerId = table.Column<int>(nullable: false),
                    ProviderUserId = table.Column<int>(nullable: true),
                    ProviderPremisesId = table.Column<int>(nullable: true),
                    TreatmentId = table.Column<int>(nullable: true),
                    IsFeeding = table.Column<bool>(nullable: false),
                    IsVaccination = table.Column<bool>(nullable: false),
                    IsCertification = table.Column<bool>(nullable: false),
                    IsTreatment = table.Column<bool>(nullable: false),
                    IsPackaging = table.Column<bool>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Food", x => x.FoodId);
                    table.ForeignKey(
                        name: "FK_Food_Categories_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Food_Premises_FarmerId",
                        column: x => x.FarmerId,
                        principalTable: "Premises",
                        principalColumn: "PremisesId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Food_Premises_ProviderPremisesId",
                        column: x => x.ProviderPremisesId,
                        principalTable: "Premises",
                        principalColumn: "PremisesId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Food_Treatment_TreatmentId",
                        column: x => x.TreatmentId,
                        principalTable: "Treatment",
                        principalColumn: "TreatmentId",
                        onDelete: ReferentialAction.NoAction);
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
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_DistributorFood_Premises_PremisesId",
                        column: x => x.PremisesId,
                        principalTable: "Premises",
                        principalColumn: "PremisesId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    TransactionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FarmerId = table.Column<int>(nullable: false),
                    ProviderId = table.Column<int>(nullable: false),
                    PremisesId = table.Column<int>(nullable: true),
                    FoodId = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ConfirmDate = table.Column<DateTime>(nullable: false),
                    StatusId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.TransactionId);
                    table.ForeignKey(
                        name: "FK_Transaction_Premises_FarmerId",
                        column: x => x.FarmerId,
                        principalTable: "Premises",
                        principalColumn: "PremisesId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transaction_Food_FoodId",
                        column: x => x.FoodId,
                        principalTable: "Food",
                        principalColumn: "FoodId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Transaction_Premises_PremisesId",
                        column: x => x.PremisesId,
                        principalTable: "Premises",
                        principalColumn: "PremisesId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transaction_TransactionStatus_StatusId",
                        column: x => x.StatusId,
                        principalTable: "TransactionStatus",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_PremisesId",
                table: "User",
                column: "PremisesId");

            migrationBuilder.CreateIndex(
                name: "IX_DistributorFood_PremisesId",
                table: "DistributorFood",
                column: "PremisesId");

            migrationBuilder.CreateIndex(
                name: "IX_Food_CategoriesId",
                table: "Food",
                column: "CategoriesId");

            migrationBuilder.CreateIndex(
                name: "IX_Food_FarmerId",
                table: "Food",
                column: "FarmerId");

            migrationBuilder.CreateIndex(
                name: "IX_Food_ProviderPremisesId",
                table: "Food",
                column: "ProviderPremisesId");

            migrationBuilder.CreateIndex(
                name: "IX_Food_TreatmentId",
                table: "Food",
                column: "TreatmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Premises_TypeId",
                table: "Premises",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_FarmerId",
                table: "Transaction",
                column: "FarmerId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_FoodId",
                table: "Transaction",
                column: "FoodId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_PremisesId",
                table: "Transaction",
                column: "PremisesId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_StatusId",
                table: "Transaction",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Treatment_PremisesId",
                table: "Treatment",
                column: "PremisesId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Premises_PremisesId",
                table: "User",
                column: "PremisesId",
                principalTable: "Premises",
                principalColumn: "PremisesId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Premises_PremisesId",
                table: "User");

            migrationBuilder.DropTable(
                name: "DistributorFood");

            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.DropTable(
                name: "Food");

            migrationBuilder.DropTable(
                name: "TransactionStatus");

            migrationBuilder.DropTable(
                name: "Treatment");

            migrationBuilder.DropTable(
                name: "Premises");

            migrationBuilder.DropTable(
                name: "PremisesType");

            migrationBuilder.DropIndex(
                name: "IX_User_PremisesId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "PremisesId",
                table: "User");

            migrationBuilder.CreateTable(
                name: "MaterialCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CategoriesId = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    DistributorUserId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    ProviderUserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_Categories_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Product_User_DistributorUserId",
                        column: x => x.DistributorUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Product_User_ProviderUserId",
                        column: x => x.ProviderUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Material",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CategoriesId = table.Column<int>(nullable: true),
                    CategoryId = table.Column<int>(nullable: false),
                    CreatedById = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    FarmerId = table.Column<int>(nullable: false),
                    MaterialName = table.Column<string>(nullable: true),
                    ProviderId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Material", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Material_MaterialCategories_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "MaterialCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Material_User_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Material_User_FarmerId",
                        column: x => x.FarmerId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Material_User_ProviderId",
                        column: x => x.ProviderId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Material_CategoriesId",
                table: "Material",
                column: "CategoriesId");

            migrationBuilder.CreateIndex(
                name: "IX_Material_CreatedById",
                table: "Material",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Material_FarmerId",
                table: "Material",
                column: "FarmerId");

            migrationBuilder.CreateIndex(
                name: "IX_Material_ProviderId",
                table: "Material",
                column: "ProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_CategoriesId",
                table: "Product",
                column: "CategoriesId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_DistributorUserId",
                table: "Product",
                column: "DistributorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProviderUserId",
                table: "Product",
                column: "ProviderUserId");
        }
    }
}
