using Examination_System.Models;

namespace Examination_System.Repository
{
    public class CourseRepo
    {
        ItiContext db;

        public CourseRepo(ItiContext _db)
        {
            db = _db;
        }
    }
}
