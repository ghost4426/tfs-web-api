using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class gianttv2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Food_Premises_ProviderPremisesId",
                table: "Food");

            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Premises_PremisesId",
                table: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_Transaction_PremisesId",
                table: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_Food_ProviderPremisesId",
                table: "Food");

            migrationBuilder.DropColumn(
                name: "PremisesId",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "ProviderPremisesId",
                table: "Food");

            migrationBuilder.RenameColumn(
                name: "ProviderUserId",
                table: "Food",
                newName: "ProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_ProviderId",
                table: "Transaction",
                column: "ProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_Food_ProviderId",
                table: "Food",
                column: "ProviderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Food_Premises_ProviderId",
                table: "Food",
                column: "ProviderId",
                principalTable: "Premises",
                principalColumn: "PremisesId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Premises_ProviderId",
                table: "Transaction",
                column: "ProviderId",
                principalTable: "Premises",
                principalColumn: "PremisesId",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Food_Premises_ProviderId",
                table: "Food");

            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Premises_ProviderId",
                table: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_Transaction_ProviderId",
                table: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_Food_ProviderId",
                table: "Food");

            migrationBuilder.RenameColumn(
                name: "ProviderId",
                table: "Food",
                newName: "ProviderUserId");

            migrationBuilder.AddColumn<int>(
                name: "PremisesId",
                table: "Transaction",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProviderPremisesId",
                table: "Food",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_PremisesId",
                table: "Transaction",
                column: "PremisesId");

            migrationBuilder.CreateIndex(
                name: "IX_Food_ProviderPremisesId",
                table: "Food",
                column: "ProviderPremisesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Food_Premises_ProviderPremisesId",
                table: "Food",
                column: "ProviderPremisesId",
                principalTable: "Premises",
                principalColumn: "PremisesId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Premises_PremisesId",
                table: "Transaction",
                column: "PremisesId",
                principalTable: "Premises",
                principalColumn: "PremisesId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
