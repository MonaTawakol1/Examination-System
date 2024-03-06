using Examination_System.Models;

namespace Examination_System.Repository
{
    public class DepartmentRepo
    {
        ItiContext db;

        public DepartmentRepo(ItiContext _db)
        {
            db = _db;
        }
    }
}
