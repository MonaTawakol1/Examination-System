using Examination_System.Models;

namespace Examination_System.Repository
{
    public class BranchRepo
    {

        ItiContext db;

        public BranchRepo(ItiContext _db)
        {
            db = _db;
        }
    }
}
