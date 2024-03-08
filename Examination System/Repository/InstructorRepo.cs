using Examination_System.Models;

namespace Examination_System.Repository
{
    public class InstructorRepo
    {
        ItiContext db;

        public InstructorRepo(ItiContext _db)
        {
            db = _db;
        }
    }
}
