using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class gianttv3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DistributorFood_Food_FoodId",
                table: "DistributorFood");

            migrationBuilder.DropForeignKey(
                name: "FK_Food_Categories_CategoriesId",
                table: "Food");

            migrationBuilder.DropForeignKey(
                name: "FK_Food_Premises_FarmerId",
                table: "Food");

            migrationBuilder.DropForeignKey(
                name: "FK_Food_Premises_ProviderId",
                table: "Food");

            migrationBuilder.DropForeignKey(
                name: "FK_Food_Treatment_TreatmentId",
                table: "Food");

            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Food_FoodId",
                table: "Transaction");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Premises_PremisesId",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Role_RoleId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_DistributorFood_PremisesId",
                table: "DistributorFood");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Role",
                table: "Role");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Food",
                table: "Food");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "Role",
                newName: "Roles");

            migrationBuilder.RenameTable(
                name: "Food",
                newName: "Foods");

            migrationBuilder.RenameIndex(
                name: "IX_User_RoleId",
                table: "Users",
                newName: "IX_Users_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_User_PremisesId",
                table: "Users",
                newName: "IX_Users_PremisesId");

            migrationBuilder.RenameIndex(
                name: "IX_Food_TreatmentId",
                table: "Foods",
                newName: "IX_Foods_TreatmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Food_ProviderId",
                table: "Foods",
                newName: "IX_Foods_ProviderId");

            migrationBuilder.RenameIndex(
                name: "IX_Food_FarmerId",
                table: "Foods",
                newName: "IX_Foods_FarmerId");

            migrationBuilder.RenameIndex(
                name: "IX_Food_CategoriesId",
                table: "Foods",
                newName: "IX_Foods_CategoriesId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                table: "Roles",
                column: "RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Foods",
                table: "Foods",
                column: "FoodId");

            migrationBuilder.CreateIndex(
                name: "IX_DistributorFood_FoodId",
                table: "DistributorFood",
                column: "FoodId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DistributorFood_PremisesId",
                table: "DistributorFood",
                column: "PremisesId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DistributorFood_Foods_FoodId",
                table: "DistributorFood",
                column: "FoodId",
                principalTable: "Foods",
                principalColumn: "FoodId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Foods_Categories_CategoriesId",
                table: "Foods",
                column: "CategoriesId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Foods_Premises_FarmerId",
                table: "Foods",
                column: "FarmerId",
                principalTable: "Premises",
                principalColumn: "PremisesId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Foods_Premises_ProviderId",
                table: "Foods",
                column: "ProviderId",
                principalTable: "Premises",
                principalColumn: "PremisesId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Foods_Treatment_TreatmentId",
                table: "Foods",
                column: "TreatmentId",
                principalTable: "Treatment",
                principalColumn: "TreatmentId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Foods_FoodId",
                table: "Transaction",
                column: "FoodId",
                principalTable: "Foods",
                principalColumn: "FoodId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Premises_PremisesId",
                table: "Users",
                column: "PremisesId",
                principalTable: "Premises",
                principalColumn: "PremisesId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DistributorFood_Foods_FoodId",
                table: "DistributorFood");

            migrationBuilder.DropForeignKey(
                name: "FK_Foods_Categories_CategoriesId",
                table: "Foods");

            migrationBuilder.DropForeignKey(
                name: "FK_Foods_Premises_FarmerId",
                table: "Foods");

            migrationBuilder.DropForeignKey(
                name: "FK_Foods_Premises_ProviderId",
                table: "Foods");

            migrationBuilder.DropForeignKey(
                name: "FK_Foods_Treatment_TreatmentId",
                table: "Foods");

            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Foods_FoodId",
                table: "Transaction");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Premises_PremisesId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_DistributorFood_FoodId",
                table: "DistributorFood");

            migrationBuilder.DropIndex(
                name: "IX_DistributorFood_PremisesId",
                table: "DistributorFood");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                table: "Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Foods",
                table: "Foods");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.RenameTable(
                name: "Roles",
                newName: "Role");

            migrationBuilder.RenameTable(
                name: "Foods",
                newName: "Food");

            migrationBuilder.RenameIndex(
                name: "IX_Users_RoleId",
                table: "User",
                newName: "IX_User_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_PremisesId",
                table: "User",
                newName: "IX_User_PremisesId");

            migrationBuilder.RenameIndex(
                name: "IX_Foods_TreatmentId",
                table: "Food",
                newName: "IX_Food_TreatmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Foods_ProviderId",
                table: "Food",
                newName: "IX_Food_ProviderId");

            migrationBuilder.RenameIndex(
                name: "IX_Foods_FarmerId",
                table: "Food",
                newName: "IX_Food_FarmerId");

            migrationBuilder.RenameIndex(
                name: "IX_Foods_CategoriesId",
                table: "Food",
                newName: "IX_Food_CategoriesId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Role",
                table: "Role",
                column: "RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Food",
                table: "Food",
                column: "FoodId");

            migrationBuilder.CreateIndex(
                name: "IX_DistributorFood_PremisesId",
                table: "DistributorFood",
                column: "PremisesId");

            migrationBuilder.AddForeignKey(
                name: "FK_DistributorFood_Food_FoodId",
                table: "DistributorFood",
                column: "FoodId",
                principalTable: "Food",
                principalColumn: "FoodId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Food_Categories_CategoriesId",
                table: "Food",
                column: "CategoriesId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Food_Premises_FarmerId",
                table: "Food",
                column: "FarmerId",
                principalTable: "Premises",
                principalColumn: "PremisesId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Food_Premises_ProviderId",
                table: "Food",
                column: "ProviderId",
                principalTable: "Premises",
                principalColumn: "PremisesId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Food_Treatment_TreatmentId",
                table: "Food",
                column: "TreatmentId",
                principalTable: "Treatment",
                principalColumn: "TreatmentId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Food_FoodId",
                table: "Transaction",
                column: "FoodId",
                principalTable: "Food",
                principalColumn: "FoodId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Premises_PremisesId",
                table: "User",
                column: "PremisesId",
                principalTable: "Premises",
                principalColumn: "PremisesId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Role_RoleId",
                table: "User",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
