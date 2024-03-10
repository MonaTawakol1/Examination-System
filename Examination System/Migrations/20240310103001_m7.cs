using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Examination_System.Migrations
{
    /// <inheritdoc />
    public partial class m7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exams_ExamQuestions_ExamQuestionsQuestionId_ExamQuestionsExamId",
                table: "Exams");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_ExamQuestions_ExamQuestionsQuestionId_ExamQuestionsExamId",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Questions_ExamQuestionsQuestionId_ExamQuestionsExamId",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Exams_ExamQuestionsQuestionId_ExamQuestionsExamId",
                table: "Exams");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExamQuestions",
                table: "ExamQuestions");

            migrationBuilder.DropIndex(
                name: "IX_ExamQuestions_ExamId",
                table: "ExamQuestions");

            migrationBuilder.DropColumn(
                name: "ExamQuestionsExamId",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "ExamQuestionsQuestionId",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "ExamQuestionsExamId",
                table: "Exams");

            migrationBuilder.DropColumn(
                name: "ExamQuestionsQuestionId",
                table: "Exams");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExamQuestions",
                table: "ExamQuestions",
                columns: new[] { "ExamId", "QuestionId" });

            migrationBuilder.CreateIndex(
                name: "IX_ExamQuestions_QuestionId",
                table: "ExamQuestions",
                column: "QuestionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ExamQuestions",
                table: "ExamQuestions");

            migrationBuilder.DropIndex(
                name: "IX_ExamQuestions_QuestionId",
                table: "ExamQuestions");

            migrationBuilder.AddColumn<int>(
                name: "ExamQuestionsExamId",
                table: "Questions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ExamQuestionsQuestionId",
                table: "Questions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ExamQuestionsExamId",
                table: "Exams",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ExamQuestionsQuestionId",
                table: "Exams",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExamQuestions",
                table: "ExamQuestions",
                columns: new[] { "QuestionId", "ExamId" });

            migrationBuilder.CreateIndex(
                name: "IX_Questions_ExamQuestionsQuestionId_ExamQuestionsExamId",
                table: "Questions",
                columns: new[] { "ExamQuestionsQuestionId", "ExamQuestionsExamId" });

            migrationBuilder.CreateIndex(
                name: "IX_Exams_ExamQuestionsQuestionId_ExamQuestionsExamId",
                table: "Exams",
                columns: new[] { "ExamQuestionsQuestionId", "ExamQuestionsExamId" });

            migrationBuilder.CreateIndex(
                name: "IX_ExamQuestions_ExamId",
                table: "ExamQuestions",
                column: "ExamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exams_ExamQuestions_ExamQuestionsQuestionId_ExamQuestionsExamId",
                table: "Exams",
                columns: new[] { "ExamQuestionsQuestionId", "ExamQuestionsExamId" },
                principalTable: "ExamQuestions",
                principalColumns: new[] { "QuestionId", "ExamId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_ExamQuestions_ExamQuestionsQuestionId_ExamQuestionsExamId",
                table: "Questions",
                columns: new[] { "ExamQuestionsQuestionId", "ExamQuestionsExamId" },
                principalTable: "ExamQuestions",
                principalColumns: new[] { "QuestionId", "ExamId" });
        }
    }
}
