using Examination_System.Models;

namespace Examination_System.Repository
{
    public class ChoiceRepo
    {
        ItiContext db;

        public ChoiceRepo(ItiContext _db)
        {
            db = _db;
        }
    }
}
