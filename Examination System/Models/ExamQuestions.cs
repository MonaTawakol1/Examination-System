using System.ComponentModel.DataAnnotations.Schema;

namespace Examination_System.Models
{
    public class ExamQuestions
    {

        // public List<Question> Questions { get; set; }

        //public List<Exam> Exams { get; set; }
        
        public bool? IsCorrect {  get; set; }

        public int? ExamAnswers { get; set; }

        [ForeignKey("Exam")]
        public int ExamId { get; set; }
        public Exam Exam { get; set; }

        [ForeignKey("Question")]
        public int QuestionId { get; set; }
        public Question Question { get; set; }

        public DateTime InsertedAt { get; set; }
    }
}
