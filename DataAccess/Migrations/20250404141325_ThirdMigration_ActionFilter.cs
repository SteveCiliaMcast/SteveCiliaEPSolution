using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ThirdMigration_ActionFilter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OptionlText",
                table: "Polls",
                newName: "VotedUsers");

            migrationBuilder.AddColumn<string>(
                name: "Option1Text",
                table: "Polls",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Option1Text",
                table: "Polls");

            migrationBuilder.RenameColumn(
                name: "VotedUsers",
                table: "Polls",
                newName: "OptionlText");
        }
    }
}
