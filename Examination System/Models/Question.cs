using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Examination_System.Models
{
    public enum QuestionType
    {
       TrueOrFalse,
       Mcq
    }
    public class Question
    {
        public int QuestionId { get; set; }

        [Required]
        public string QuestionBody { get; set; }

        public int QuestionMark { get; set; }

        public QuestionType QuestionType { get; set; }

       // [ForeignKey("ModelAnswer")]
        //public int ModelAnswerId { get; set; }

       // public string ModelAnswer { get; set; }

        public List<Choice> ChoicesList { get; set; }  

          [ForeignKey("Course")]
           public int CourseId { get; set; }

           public Course Course { get; set; }

            public List<ExamQuestions> ExamQuestions { get; set; }=new List<ExamQuestions>();

        public bool isDeleted { get; set; } = false;


    }
}
