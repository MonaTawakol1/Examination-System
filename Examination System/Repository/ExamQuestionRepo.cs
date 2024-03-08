using Examination_System.Models;

namespace Examination_System.Repository
{
    public class ExamQuestionRepo
    {
        ItiContext db;

        public ExamQuestionRepo(ItiContext _db)
        {
            db = _db;
        }
    }
}
