using Examination_System.Models;
using Examination_System.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Examination_System.Controllers
{
    public class InstructorController : Controller
    {
        public IInstructorRepo insRepo { get; set; }
        public ICourseRepo _courseRepo { get; set; }
        public IStudentRepo _studentRepo { get; set; }
        public InstructorController(IInstructorRepo instructorRepo, ICourseRepo courseRepo, IStudentRepo studentRepo)
        {
            insRepo = instructorRepo;
            _courseRepo = courseRepo;
            _studentRepo = studentRepo;
        }
        //TODO
        //READ FROM SESSION 
        static int instructorId = 2;
        //static ItiContext db = new ItiContext();
        //InstructorRepo insRepo = new InstructorRepo(db);

        public IActionResult ShowCourses()
        {
            var model = insRepo.GetAllCourses(instructorId);
            return View(model);
        }
        //static int courseId = 2;
        public IActionResult ShowTopics(int CourseId)
        {
            var model = _courseRepo.GetAllTopics(CourseId);
            return View(model);
        }
        public IActionResult ShowStudents(int courseId)
        {
            var model = _studentRepo.GetAllStudents(courseId);
            ViewBag.CourseId = courseId;
            return View(model);
        }
        [HttpGet]
        public IActionResult AddStudent(int courseId)
        {
            ViewBag.StdList = _studentRepo.GetAllStudents();
            ViewBag.CourseId = courseId;
            return View();
        }
        [HttpPost]
        public IActionResult AddStudent(int courseId, int StudentId)
        {
            _studentRepo.AddStudentToCourse(courseId, StudentId);
            return RedirectToAction("ShowStudents", new { courseId = courseId });
        }
        [HttpGet]
        public IActionResult UpdateStudent(int courseId, int studentId)
        {
            var student = _studentRepo.GetStudentById(studentId);
            ViewBag.CourseId = courseId;
            return View(student);
        }

        [HttpPost]
        //public IActionResult UpdateStudent(int courseId, Student student)
        //{
        //    _studentRepo.UpdateStudentInCourse(courseId, student);
        //    return RedirectToAction("ShowStudents", new { courseId = courseId });
        //}

        //public IActionResult DeleteStudent(int courseId, int studId)
        //{
        //    _studentRepo.RemoveStudentFromCourse(courseId, studId);
        //    return RedirectToAction("ShowStudents");
        //}
        [HttpDelete]
        public IActionResult DeleteStudent(int courseId, int studentId)
        {
            _studentRepo.RemoveStudentFromCourse(courseId, studentId);
            return RedirectToAction("ShowStudents", new { courseId = courseId });
        }
    }
}
