using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Examination_System.Migrations
{
    /// <inheritdoc />
    public partial class isdeletedIns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "Instructors",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "Instructors");
        }
    }
}
