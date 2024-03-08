using Examination_System.Models;

namespace Examination_System.Repository
{
    public class TopicRepo
    {

        ItiContext db;

        public TopicRepo(ItiContext _db)
        {
            db = _db;
        }

    }
}
