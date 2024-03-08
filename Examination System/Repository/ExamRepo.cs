using Examination_System.Models;

namespace Examination_System.Repository
{
    public class ExamRepo
    {
        ItiContext db;

        public ExamRepo(ItiContext _db)
        {
            db = _db;
        }
    }
}
