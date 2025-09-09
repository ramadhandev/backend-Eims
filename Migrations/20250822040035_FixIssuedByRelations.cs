using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EIMS.Migrations
{
    /// <inheritdoc />
    public partial class FixIssuedByRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UseDocuments_Users_UserId1",
                table: "UseDocuments");

            migrationBuilder.DropIndex(
                name: "IX_UseDocuments_UserId_DocumentRequirementId",
                table: "UseDocuments");

            migrationBuilder.DropIndex(
                name: "IX_UseDocuments_UserId1",
                table: "UseDocuments");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "UseDocuments");

            migrationBuilder.CreateIndex(
                name: "IX_UseDocuments_UserId_DocumentRequirementId",
                table: "UseDocuments",
                columns: new[] { "UserId", "DocumentRequirementId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UseDocuments_UserId_DocumentRequirementId",
                table: "UseDocuments");

            migrationBuilder.AddColumn<int>(
                name: "UserId1",
                table: "UseDocuments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UseDocuments_UserId_DocumentRequirementId",
                table: "UseDocuments",
                columns: new[] { "UserId", "DocumentRequirementId" });

            migrationBuilder.CreateIndex(
                name: "IX_UseDocuments_UserId1",
                table: "UseDocuments",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_UseDocuments_Users_UserId1",
                table: "UseDocuments",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "UserId");
        }
    }
}
