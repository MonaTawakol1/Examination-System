using System.ComponentModel.DataAnnotations.Schema;

namespace Examination_System.Models
{
    public class Exam
    {
        public int ExamId { get; set; }

        [ForeignKey("Student")]
        public int StudentId { get; set; }

        public Student Student { get; set; }

        [ForeignKey("Course")]
        public int CourseId { get; set; }

        public Course Course { get; set; }

        public int StudentGrade { get; set; }

        public List<ExamQuestions> ExamQuestions { get; set; }
    }
}
