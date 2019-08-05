using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class gianttv6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Food_Users_CreateById",
                table: "Food");

            migrationBuilder.DropForeignKey(
                name: "FK_Food_Treatment_TreatmentId",
                table: "Food");

            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Premises_FarmId",
                table: "Transaction");

            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Premises_ProviderId",
                table: "Transaction");

            migrationBuilder.DropForeignKey(
                name: "FK_Treatment_Users_CreateById",
                table: "Treatment");

            migrationBuilder.DropIndex(
                name: "IX_Food_TreatmentId",
                table: "Food");

            migrationBuilder.DropColumn(
                name: "FeedingId",
                table: "Food");

            migrationBuilder.DropColumn(
                name: "TreatmentId",
                table: "Food");

            migrationBuilder.RenameColumn(
                name: "ProviderId",
                table: "Transaction",
                newName: "SenderId");

            migrationBuilder.RenameColumn(
                name: "FarmId",
                table: "Transaction",
                newName: "ReceiverId");

            migrationBuilder.RenameIndex(
                name: "IX_Transaction_ProviderId",
                table: "Transaction",
                newName: "IX_Transaction_SenderId");

            migrationBuilder.RenameIndex(
                name: "IX_Transaction_FarmId",
                table: "Transaction",
                newName: "IX_Transaction_ReceiverId");

            migrationBuilder.AlterColumn<bool>(
                name: "IsConfirmEmail",
                table: "Users",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CreateById",
                table: "Treatment",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CreateById",
                table: "Food",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Food_Users_CreateById",
                table: "Food",
                column: "CreateById",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Premises_ReceiverId",
                table: "Transaction",
                column: "ReceiverId",
                principalTable: "Premises",
                principalColumn: "PremisesId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Premises_SenderId",
                table: "Transaction",
                column: "SenderId",
                principalTable: "Premises",
                principalColumn: "PremisesId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Treatment_Users_CreateById",
                table: "Treatment",
                column: "CreateById",
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
                name: "FK_Transaction_Premises_ReceiverId",
                table: "Transaction");

            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Premises_SenderId",
                table: "Transaction");

            migrationBuilder.DropForeignKey(
                name: "FK_Treatment_Users_CreateById",
                table: "Treatment");

            migrationBuilder.RenameColumn(
                name: "SenderId",
                table: "Transaction",
                newName: "ProviderId");

            migrationBuilder.RenameColumn(
                name: "ReceiverId",
                table: "Transaction",
                newName: "FarmId");

            migrationBuilder.RenameIndex(
                name: "IX_Transaction_SenderId",
                table: "Transaction",
                newName: "IX_Transaction_ProviderId");

            migrationBuilder.RenameIndex(
                name: "IX_Transaction_ReceiverId",
                table: "Transaction",
                newName: "IX_Transaction_FarmId");

            migrationBuilder.AlterColumn<string>(
                name: "IsConfirmEmail",
                table: "Users",
                nullable: true,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CreateById",
                table: "Treatment",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "CreateById",
                table: "Food",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "FeedingId",
                table: "Food",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TreatmentId",
                table: "Food",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Food_TreatmentId",
                table: "Food",
                column: "TreatmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Food_Users_CreateById",
                table: "Food",
                column: "CreateById",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Food_Treatment_TreatmentId",
                table: "Food",
                column: "TreatmentId",
                principalTable: "Treatment",
                principalColumn: "TreatmentId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Premises_FarmId",
                table: "Transaction",
                column: "FarmId",
                principalTable: "Premises",
                principalColumn: "PremisesId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Premises_ProviderId",
                table: "Transaction",
                column: "ProviderId",
                principalTable: "Premises",
                principalColumn: "PremisesId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Treatment_Users_CreateById",
                table: "Treatment",
                column: "CreateById",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
