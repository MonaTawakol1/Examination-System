using Examination_System.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Examination_System.Repository
{

    public interface IInstructorRepo
    {
        public Instructor ShowCourses(int id);
        public List<Question> ShowQuestions(int id);
        public void DeleteQuestion(int id);
        public int GetCourseIdByQuestionId(int questionId);
        public Instructor GetInstructorByUserId(int userId);
        public Instructor GetInstructor(int instructorId);
        public List<Course> getCoursesInDepartments(int instructorId, int departmentId);
        public List<Course> getCoursesInDepartmentss(int instructorId, int departmentId);

        public List<Student> getListOfStudents(int courseId);

        public List<Student> GetStudentNotInCourse(int courseId, int departmentId);

        public void Add(Student std, int CourseId);

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
        public List<Question> ShowQuestions(int id)
        {
           List<Question> question = db.Questions.Include(a=>a.ChoicesList).Where(a=>a.CourseId == id).Where(a => a.isDeleted == false).ToList();
            return question;
          
        }
       public void DeleteQuestion(int id)
        {
            var question = db.Questions.Include(a=>a.ChoicesList).SingleOrDefault(a => a.QuestionId == id);
            if (question != null)
            {

                question.isDeleted = true;
               // db.Questions.Remove(question);
                db.SaveChanges();
            }
        }
        public int GetCourseIdByQuestionId(int questionId)
        {
            var questioncourse = db.Questions.SingleOrDefault(a=>a.QuestionId ==  questionId);
            var courseId = questioncourse.CourseId;
            return courseId;
        }


        public Instructor GetInstructorByUserId(int userId)
        {
            Instructor instructor=db.Instructors.Include(a=>a.Courses).Include(a=>a.User).FirstOrDefault(a=>a.UserId == userId);
            return instructor;
        }

        public Instructor GetInstructor(int instructorId)
        {
            var instructor = db.Instructors.Include(a => a.Departments).Include(a => a.User).FirstOrDefault(a => a.UserId == instructorId);
            return instructor;

        }
      

        public List<Course> getCoursesInDepartments(int instructorId , int departmentId)
        {
            

            var departments = db.Departments.Include(a => a.courses).FirstOrDefault(a => a.DepartmentId == departmentId);
            var courses = departments.courses.ToList();
            List<Course> crs = new List<Course>();
            foreach(var course in courses)
            {
               foreach(var item in course.Instructors)
                {
                    if(item.InstructorId == instructorId)
                    {
                        crs.Add(course);
                    }
                }
            }
            return crs;
        }

        public List<Course> getCoursesInDepartmentss(int instructorId, int departmentId)
        {
            var ins = db.Instructors.Include(a => a.Courses).ThenInclude(a => a.Departments).FirstOrDefault(a => a.InstructorId == instructorId);
            var courses = ins.Courses.ToList(); 
            List<Course> crs = new List<Course>();
            foreach (var course in courses)
            {
                foreach (var item in course.Departments)
                {
                    if (item.DepartmentId == departmentId)
                    {
                        crs.Add(course);
                    }
                }
            }
            return crs;
        }

        public List<Student> getListOfStudents(int courseId)
        {
            var students = db.Students.Include(a=>a.User).Include(a => a.Courses).ToList();
            List<Student> studentsList = new List<Student>(); 
            foreach(var std in students)
            {
                foreach(var crs in std.Courses)
                {
                    if(crs.CourseId == courseId)
                    {
                        studentsList.Add(std);
                    }
                }
            }
            return studentsList;
        }

        public List<Student> GetStudentNotInCourse(int courseId , int departmentId)
        {
            
            var student = db.Students.Where(a=>a.DepartmentId == departmentId).ToList();
           var Allstudents =  getListOfStudents(courseId);
            
            foreach(var std in student)
            {
                foreach(var item in Allstudents)
                {
                    if(std.StudentId == item.StudentId)
                    {
                        student.Remove(std);
                    }
                    
                }
            }

            return student;

        }
        public void Add(Student std , int CourseId)
        {
           var courses = db.Courses.Include(a=>a.Students).FirstOrDefault(a=>a.CourseId == CourseId);
            courses.Students.Add(std);
            db.SaveChanges();
            
        }



    }
}
