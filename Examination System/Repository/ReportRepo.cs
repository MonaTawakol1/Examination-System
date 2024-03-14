using Examination_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Examination_System.Repository
{

    public interface IReportRepo
    {
        public List<Student> StudentsInformation(int DeptNumber);
        public List<Department> GetDepartments();
        public List<Student> GetStudents();
        public List<Exam> GetExams(int studentid);
        public List<Instructor> GetInstructors();
        public List<Course> GetCourse(int InstructorId);
        public List<Course> GetAllCourses();
        public List<Topic> showtopics(int CourseId);
        public List<ExamQuestions> GetExamQuestions(int examid);
        public List<Exam> GetAllExam();
    }
    public class ReportRepo: IReportRepo
    {

        ItiContext db;

        public ReportRepo(ItiContext _db)
        {
            db = _db;
        }
        
        public List<Student> StudentsInformation(int DeptNumber)
        {
            return  db.Students.Include(a=>a.User).Include(a=>a.Branch).Where(a=>a.DepartmentId == DeptNumber).ToList();
        }
        public List<Department> GetDepartments()
        {
            return db.Departments.ToList();
        }
        public List<Student> GetStudents()
        {

            return db.Students.ToList();
        }
        public List<Exam> GetExams(int studentid)
        {
            return db.Exams.Include(a => a.Course).Include(a => a.Student).ThenInclude(a => a.User).Where(a => a.StudentId == studentid).ToList();

        }

        public List<Instructor> GetInstructors()
        {
           return db.Instructors.ToList();
        }
        public List<Course> GetCourse(int InstructorId)
        {
            var instructor= db.Instructors.Include(a => a.Courses).ThenInclude(a=>a.Students).FirstOrDefault(a => a.InstructorId == InstructorId);
            return instructor.Courses.ToList();


        }

        public List<Course> GetAllCourses()
        {
            return db.Courses.Include(a=>a.Topics).ToList();
        }

        public List<Topic> showtopics(int CourseId)
        {
            return db.Topics.Where(a => a.CourseId == CourseId).ToList();
        }

        public List<ExamQuestions> GetExamQuestions(int examid)
        {
            var ExamQ=db.ExamQuestions.Include(a=>a.Question).ThenInclude(a=>a.ChoicesList).Where(a=>a.ExamId==examid).ToList();
            return ExamQ;
        }

        public List<Exam> GetAllExam()
        {

            return db.Exams.ToList();
        }

       

    }
}
