using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Examination_System.Migrations
{
    /// <inheritdoc />
    public partial class relationonetooneinstructormanagebranch : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BranchManager",
                table: "Branches");

            migrationBuilder.AddColumn<int>(
                name: "BranchManagerId",
                table: "Branches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Branches_BranchManagerId",
                table: "Branches",
                column: "BranchManagerId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Branches_Instructors_BranchManagerId",
                table: "Branches",
                column: "BranchManagerId",
                principalTable: "Instructors",
                principalColumn: "InstructorId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Branches_Instructors_BranchManagerId",
                table: "Branches");

            migrationBuilder.DropIndex(
                name: "IX_Branches_BranchManagerId",
                table: "Branches");

            migrationBuilder.DropColumn(
                name: "BranchManagerId",
                table: "Branches");

            migrationBuilder.AddColumn<string>(
                name: "BranchManager",
                table: "Branches",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
