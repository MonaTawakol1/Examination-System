using Examination_System.Models;

using Microsoft.EntityFrameworkCore;
namespace Examination_System.Repository
{

    public interface IstudentRepo
    {

        public Student GetStudent(int id);
        public Student ShowCourses(int id);
        public Course showtopics(int id);
    }
    public class StudentRepo:IstudentRepo
    {
        ItiContext db;

        public StudentRepo(ItiContext _db)
        {
            db = _db;
        }

        public Student GetStudent(int id)
        {
          Student student = db.Students.Include(a=>a.Department).Include(a=>a.Branch).FirstOrDefault(a=>a.StudentId==id);

            return student;
        }
        public Course showtopics(int id)
        {
           Course c = db.Courses.Include(a => a.Topics).FirstOrDefault(a => a.CourseId == id);
            return c;
        }

        public Student ShowCourses(int id)
        {
            Student student = db.Students.Include(a => a.Courses).FirstOrDefault(a => a.StudentId == id);
            return student;
        }


    }
}
