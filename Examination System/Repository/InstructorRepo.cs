using Examination_System.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.Metrics;

namespace Examination_System.Repository
{
    public interface IInstructorRepo 
    {
        public List<Course> GetAllCourses(int instructorId);
    }

    public class InstructorRepo: IInstructorRepo
    {
        ItiContext db;

        public InstructorRepo(ItiContext _db)
        {
            db = _db;
        }
        public List<Course> GetAllCourses(int instructorId)
        {
           var instructor = db.Instructors.Include(i=>i.Courses).SingleOrDefault(i=>i.InstructorId== instructorId);
            if(instructor == null)
            {
                return new List<Course>();
            }
            else
            {
                return instructor.Courses.ToList();
            }
        }
    }
}
