using Examination_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Examination_System.Repository
{

    public interface IInstructorRepo
    {
        public Instructor ShowCourses(int id);
    }
    public class InstructorRepo : IInstructorRepo
    {
        ItiContext db;

        public InstructorRepo(ItiContext _db)
        {
            db = _db;
        }

        public Instructor ShowCourses(int id)
        {
            Instructor instructor = db.Instructors.Include(a => a.Courses).FirstOrDefault(a => a.InstructorId == id);
            return instructor;
        }

    }
}
