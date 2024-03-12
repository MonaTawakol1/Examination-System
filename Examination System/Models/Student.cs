using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Examination_System.Models
{
    public class Student
    {
        public int StudentId { get; set; }

        //[Required]
        //public string StudentName { get; set; }
        
        //public string StudentPassword { get; set;}

  

        public List<Exam> Exams { get; set; }   
        [ForeignKey("Department")]
        public int DepartmentId { get; set; }

       public Department Department { get; set; }

        public List<Course> Courses { get; set; }

        [ForeignKey("Branch")]
        public int branchId { get; set; }
        public Branch Branch { get; set; }


        public int UserId { get; set; }

        public User User { get; set; }

    }
}
