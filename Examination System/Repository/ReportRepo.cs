using Examination_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Examination_System.Repository
{

    public interface IReportRepo
    {
        public List<Student> StudentsInformation(int DeptNumber);
        public List<Department> GetDepartments();
    }
    public class ReportRepo: IReportRepo
    {

        ItiContext db;

        public ReportRepo(ItiContext _db)
        {
            db = _db;
        }
        
        public List<Student> StudentsInformation(int DeptNumber)
        {
            return  db.Students.Include(a=>a.User).Include(a=>a.Branch).Where(a=>a.DepartmentId == DeptNumber).ToList();
        }
        public List<Department> GetDepartments()
        {
            return db.Departments.ToList();
        }

    }
}
