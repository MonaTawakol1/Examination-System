using Examination_System.Models;

using Microsoft.EntityFrameworkCore;
namespace Examination_System.Repository
{

    public interface IstudentRepo
    {

        public Student GetStudent(int id);
        public Student ShowCourses(int id);
        public Course showtopics(int id);

        public Student GetStudentByUserId(int id);

        public List<Exam> GetExams(int studentid , int courseId) ;



        public List<Student> GetAllStudents(int courseId);
        

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


        public Student GetStudentByUserId(int id)
        {
            Student student = db.Students.Include(a => a.Department).Include(a => a.Branch).Include(a=>a.User).FirstOrDefault(a => a.UserId==id);

            return student;
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


        public List<Exam> GetExams(int studentid, int courseId)
        {
            return db.Exams.Include(a => a.Course).Include(a => a.Student).ThenInclude(a => a.User).Where(a => a.StudentId == studentid && a.CourseId == courseId).ToList();

        }


    }
}
