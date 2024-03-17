using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Examination_System.Models
{
    public class Course
    {
        public int CourseId { get; set; }

        [Required]
        public string CourseName { get; set; }

        public List<Topic> Topics { get; set; }

        public List<Question> Questions { get; set; }

        public List<Exam> Exams { get; set; }

        public List<Student> Students { get; set; }

        public List<Instructor> Instructors { get; set; }
        public List<Department> Departments { get; set; }

        public Boolean isDeleted { get; set; } = false;


        [CustomValidation(typeof(Course), "ValidateStartDateTime")]
        public DateTime? ExamStartDateTime { get; set; }

       

        public DateTime? ExamEndDateTime { get; set;}
        [Required(ErrorMessage = "Number of true/false questions is required.")]
        [Range(3, 9, ErrorMessage = "Number of true/false questions must be between 3 and 9.")]
        public int? NumberOfTrueAndFalseQuestions { get; set; } = 1;
        [Required(ErrorMessage = "Number of MCQ questions is required.")]
        [Range(3, 9, ErrorMessage = "Number of MCQ questions must be between 3 and 9.")]
        public int? NumberOfMcqQuestions { get; set; } = 1;


        public static ValidationResult ValidateStartDateTime(DateTime? startDateTime, ValidationContext validationContext)
        {
            if (startDateTime.HasValue && startDateTime < DateTime.Today)
            {
                return new ValidationResult("Exam start date must be greater than today's date.");
            }
            return ValidationResult.Success;
        }

    }
}
