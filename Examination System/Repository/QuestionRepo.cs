using Examination_System.Models;

namespace Examination_System.Repository
{
    public class QuestionRepo
    {
        ItiContext db;

        public QuestionRepo(ItiContext _db)
        {
            db = _db;
        }
    }
}
