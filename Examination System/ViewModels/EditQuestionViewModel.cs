using Examination_System.Models;

namespace Examination_System.ViewModels
{
    public class EditQuestionViewModel
    {
    
        public Question question { get; set; }
        public Choice Answer { get; set; }

        public List<Choice> Choices { get; set; }
    
     
        public int CourseId {  get; set; }
    }
}
