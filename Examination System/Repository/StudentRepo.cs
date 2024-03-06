using Examination_System.Models;

namespace Examination_System.Repository
{
    public class StudentRepo
    {
        ItiContext db;

        public StudentRepo(ItiContext _db)
        {
            db = _db;
        }
    }
}
