using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Examination_System.Models
{
    public class Branch
    { 
        public int BranchId { get; set; }

        [Required]
        public string BranchName { get; set; }

        public List<Department> DepartmentList { get; set; }
        public List<Student> StudentList { get; set; }

        

        public Boolean isDeleted {  get; set; }=false;
    }
}
