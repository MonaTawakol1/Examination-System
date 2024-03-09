using System.ComponentModel.DataAnnotations;

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
    }
}
