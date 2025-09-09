using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EIMS.Migrations
{
    /// <inheritdoc />
    public partial class InitialUpdatePermitWorks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Approvals_Permits_PermitId",
                table: "Approvals");

            migrationBuilder.DropForeignKey(
                name: "FK_Permits_CardTypes_RequiredCardTypeId",
                table: "Permits");

            migrationBuilder.DropForeignKey(
                name: "FK_Permits_Users_AutoApprovedBy",
                table: "Permits");

            migrationBuilder.DropForeignKey(
                name: "FK_Permits_Users_UserId",
                table: "Permits");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Permits",
                table: "Permits");

            migrationBuilder.RenameTable(
                name: "Permits",
                newName: "PermitToWorks");

            migrationBuilder.RenameIndex(
                name: "IX_Permits_UserId",
                table: "PermitToWorks",
                newName: "IX_PermitToWorks_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Permits_RequiredCardTypeId",
                table: "PermitToWorks",
                newName: "IX_PermitToWorks_RequiredCardTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Permits_AutoApprovedBy",
                table: "PermitToWorks",
                newName: "IX_PermitToWorks_AutoApprovedBy");

            migrationBuilder.AlterColumn<int>(
                name: "RequiredCardTypeId",
                table: "PermitToWorks",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PermitToWorks",
                table: "PermitToWorks",
                column: "PermitId");

            migrationBuilder.AddForeignKey(
                name: "FK_Approvals_PermitToWorks_PermitId",
                table: "Approvals",
                column: "PermitId",
                principalTable: "PermitToWorks",
                principalColumn: "PermitId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PermitToWorks_CardTypes_RequiredCardTypeId",
                table: "PermitToWorks",
                column: "RequiredCardTypeId",
                principalTable: "CardTypes",
                principalColumn: "CardTypeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PermitToWorks_Users_AutoApprovedBy",
                table: "PermitToWorks",
                column: "AutoApprovedBy",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PermitToWorks_Users_UserId",
                table: "PermitToWorks",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Approvals_PermitToWorks_PermitId",
                table: "Approvals");

            migrationBuilder.DropForeignKey(
                name: "FK_PermitToWorks_CardTypes_RequiredCardTypeId",
                table: "PermitToWorks");

            migrationBuilder.DropForeignKey(
                name: "FK_PermitToWorks_Users_AutoApprovedBy",
                table: "PermitToWorks");

            migrationBuilder.DropForeignKey(
                name: "FK_PermitToWorks_Users_UserId",
                table: "PermitToWorks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PermitToWorks",
                table: "PermitToWorks");

            migrationBuilder.RenameTable(
                name: "PermitToWorks",
                newName: "Permits");

            migrationBuilder.RenameIndex(
                name: "IX_PermitToWorks_UserId",
                table: "Permits",
                newName: "IX_Permits_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_PermitToWorks_RequiredCardTypeId",
                table: "Permits",
                newName: "IX_Permits_RequiredCardTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_PermitToWorks_AutoApprovedBy",
                table: "Permits",
                newName: "IX_Permits_AutoApprovedBy");

            migrationBuilder.AlterColumn<int>(
                name: "RequiredCardTypeId",
                table: "Permits",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Permits",
                table: "Permits",
                column: "PermitId");

            migrationBuilder.AddForeignKey(
                name: "FK_Approvals_Permits_PermitId",
                table: "Approvals",
                column: "PermitId",
                principalTable: "Permits",
                principalColumn: "PermitId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Permits_CardTypes_RequiredCardTypeId",
                table: "Permits",
                column: "RequiredCardTypeId",
                principalTable: "CardTypes",
                principalColumn: "CardTypeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Permits_Users_AutoApprovedBy",
                table: "Permits",
                column: "AutoApprovedBy",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Permits_Users_UserId",
                table: "Permits",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
