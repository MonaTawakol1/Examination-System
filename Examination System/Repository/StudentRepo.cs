using Examination_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Examination_System.Repository
{
    public interface IStudentRepo
    {
        public List<Student> GetAllStudents (int courseId);
        public List<Student> GetAllStudents();
        public Student GetStudentById(int studentId);
        public void AddStudentToCourse(int courseId, int studentId);
        //public void UpdateStudentInCourse(int courseId, Student student);
     
        public void RemoveStudentFromCourse(int courseId, int studentId);
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
        public List<Student> GetAllStudents()
        {
            var model = db.Students.ToList();
            return model;
        }
        public Student GetStudentById(int studentId)
        {
            return db.Students.FirstOrDefault(s => s.StudentId == studentId);
        }
        public void AddStudentToCourse(int courseId, int studentId)
        {
            var course = db.Courses.Include(c => c.Students).FirstOrDefault(c => c.CourseId == courseId);
            var student = db.Students.FirstOrDefault(s => s.StudentId == studentId);
            if (course != null && student != null)
            {
                course.Students.Add(student);
                db.SaveChanges();
            }

        }
        ////public void UpdateStudentInCourse(int courseId, Student student)
        ////{
        ////    var course = db.Courses.Include(c => c.Students).FirstOrDefault(c => c.CourseId == courseId);
        ////    var existingStudent = course.Students.FirstOrDefault(s => s.StudentId == student.StudentId);
        ////    if (existingStudent != null)
        ////    {
        ////        existingStudent.StudentName = student.StudentName;
        ////        db.SaveChanges();
        ////    }
        ////}
        public void RemoveStudentFromCourse(int courseId, int studentId)
        {
            var course = db.Courses.Include(c => c.Students).FirstOrDefault(c => c.CourseId == courseId);
            var student = course.Students.FirstOrDefault(s => s.StudentId == studentId);
            if (student != null)
            {
                course.Students.Remove(student);
                db.SaveChanges();
            }
        }
        //public void AddStudentToCourse(int courseId, int studentId)
        //{
        //    var course = db.Courses.Include(c => c.Students).FirstOrDefault(c => c.CourseId == courseId);
        //    var student = db.Students.FirstOrDefault(s => s.StudentId == studentId);
        //    if (course != null && student != null)
        //    {
        //        course.Students.Add(student);
        //        db.SaveChanges();
        //    }
        //}

        //public void RemoveStudentFromCourse(int courseId, int studentId)
        //{
        //    var course = db.Courses.Include(c => c.Students).FirstOrDefault(c => c.CourseId == courseId);
        //    var student = db.Students.FirstOrDefault(s => s.StudentId == studentId);
        //    if (course != null && student != null)
        //    {
        //        course.Students.Remove(student);
        //        db.SaveChanges();
        //    }
        //}
        //public void AddStudentToCourse(int courseId, int studentId)
        //{
        //    var course = db.Courses.FirstOrDefault(c => c.CourseId == courseId);
        //    var student = db.Students.FirstOrDefault(s => s.StudentId == studentId);

        //    if (course != null && student != null)
        //    {
        //        course.Students.Add(student);
        //        db.SaveChanges();
        //    }
        //}

        //public void RemoveStudentFromCourse(int courseId, int studentId)
        //{
        //    var course = db.Courses.FirstOrDefault(c => c.CourseId == courseId);
        //    var student = db.Students.FirstOrDefault(s => s.StudentId == studentId);

        //    if (course != null && student != null)
        //    {
        //        course.Students.Remove(student);
        //        db.SaveChanges();
        //    }
        //}

    }
}
