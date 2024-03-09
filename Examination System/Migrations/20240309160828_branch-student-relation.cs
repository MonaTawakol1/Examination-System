using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Examination_System.Migrations
{
    /// <inheritdoc />
    public partial class branchstudentrelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "branchId",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Students_branchId",
                table: "Students",
                column: "branchId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Branches_branchId",
                table: "Students",
                column: "branchId",
                principalTable: "Branches",
                principalColumn: "BranchId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Branches_branchId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_branchId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "branchId",
                table: "Students");
        }
    }
}
