using Examination_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Examination_System.Repository
{
    public interface IStudentRepo
    {
        public List<Student> GetAllStudents (int courseId);
    }
    public class StudentRepo : IStudentRepo
    {
        ItiContext db;

        public StudentRepo(ItiContext _db)
        {
            db = _db;
        }
        public List<Student> GetAllStudents(int courseId)
        {
            var course = db.Courses.Include(i => i.Students).SingleOrDefault(i => i.CourseId == courseId);
            if (course == null)
            {
                return new List<Student>();
            }
            else
            {
                return course.Students.ToList();
            }
        }

    }
}
