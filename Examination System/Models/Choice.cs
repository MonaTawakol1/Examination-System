using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Examination_System.Models
{
    public class Choice
    {
        public int ChoiceId { get; set; }

        [Required]
        public string ChoiceBody { get; set; }


        //[ForeignKey("Question")]
        public int QuestionId { get; set; }

        public  Question Question { get; set; }
    }
}
