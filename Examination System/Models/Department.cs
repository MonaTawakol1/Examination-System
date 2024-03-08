using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Examination_System.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }

        [Required]
        public string DepartmentName { get; set; }

      public List<Branch> Branches { get; set; }

      public  List<Instructor> instructors { get; set; }

       public  List<Student> students { get; set; }

      // [ForeignKey("Manager")]
        public int ManagerId { get; set; }
        public Instructor Manager { get; set; }


    }
}
