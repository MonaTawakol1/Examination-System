using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Examination_System.Migrations
{
    /// <inheritdoc />
    public partial class m6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExamQuestions_Exams_ExamId",
                table: "ExamQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_ExamQuestions_Questions_QuestionId",
                table: "ExamQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_Exams_ExamQuestions_ExamQuestionsExamId_ExamQuestionsQuestionId",
                table: "Exams");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_ExamQuestions_ExamQuestionsExamId_ExamQuestionsQuestionId",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Questions_ExamQuestionsExamId_ExamQuestionsQuestionId",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Exams_ExamQuestionsExamId_ExamQuestionsQuestionId",
                table: "Exams");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExamQuestions",
                table: "ExamQuestions");

            migrationBuilder.DropIndex(
                name: "IX_ExamQuestions_QuestionId",
                table: "ExamQuestions");

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
                name: "FK_ExamQuestions_Exams_ExamId",
                table: "ExamQuestions",
                column: "ExamId",
                principalTable: "Exams",
                principalColumn: "ExamId",
               onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_ExamQuestions_Questions_QuestionId",
                table: "ExamQuestions",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "QuestionId",
                onDelete: ReferentialAction.NoAction);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExamQuestions_Exams_ExamId",
                table: "ExamQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_ExamQuestions_Questions_QuestionId",
                table: "ExamQuestions");

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

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExamQuestions",
                table: "ExamQuestions",
                columns: new[] { "ExamId", "QuestionId" });

            migrationBuilder.CreateIndex(
                name: "IX_Questions_ExamQuestionsExamId_ExamQuestionsQuestionId",
                table: "Questions",
                columns: new[] { "ExamQuestionsExamId", "ExamQuestionsQuestionId" });

            migrationBuilder.CreateIndex(
                name: "IX_Exams_ExamQuestionsExamId_ExamQuestionsQuestionId",
                table: "Exams",
                columns: new[] { "ExamQuestionsExamId", "ExamQuestionsQuestionId" });

            migrationBuilder.CreateIndex(
                name: "IX_ExamQuestions_QuestionId",
                table: "ExamQuestions",
                column: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExamQuestions_Exams_ExamId",
                table: "ExamQuestions",
                column: "ExamId",
                principalTable: "Exams",
                principalColumn: "ExamId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExamQuestions_Questions_QuestionId",
                table: "ExamQuestions",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exams_ExamQuestions_ExamQuestionsExamId_ExamQuestionsQuestionId",
                table: "Exams",
                columns: new[] { "ExamQuestionsExamId", "ExamQuestionsQuestionId" },
                principalTable: "ExamQuestions",
                principalColumns: new[] { "ExamId", "QuestionId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_ExamQuestions_ExamQuestionsExamId_ExamQuestionsQuestionId",
                table: "Questions",
                columns: new[] { "ExamQuestionsExamId", "ExamQuestionsQuestionId" },
                principalTable: "ExamQuestions",
                principalColumns: new[] { "ExamId", "QuestionId" });
        }
    }
}
