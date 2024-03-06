using System.ComponentModel.DataAnnotations;

namespace Examination_System.Models
{
    public class Branch
    { 
        public int BranchId { get; set; }

        [Required]
        public string BranchName { get; set; }

        public string BranchManager { get; set; }
        public List<Department> DepartmentList { get; set; }
    }
}
