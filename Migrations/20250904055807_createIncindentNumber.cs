using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EIMS.Migrations
{
    /// <inheritdoc />
    public partial class createIncindentNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IncidentNumber",
                table: "IncidentReports",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IncidentNumber",
                table: "IncidentReports");
        }
    }
}
