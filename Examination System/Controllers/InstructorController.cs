using Examination_System.Models;
using Examination_System.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Examination_System.Controllers
{
    public class InstructorController : Controller
    {
        public IInstructorRepo insRepo { get; set; }
        public ICourseRepo _courseRepo { get; set; }
        public IStudentRepo _studentRepo { get; set; }
        public InstructorController(IInstructorRepo instructorRepo, ICourseRepo courseRepo, IStudentRepo studentRepo) {
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
            var model =insRepo.GetAllCourses(instructorId);
            return View(model);
        }
        public IActionResult ShowTopics(int courseId)
        {
            var model = _courseRepo.GetAllTopics(courseId);
            return View(model);
        }
        public IActionResult ShowStudents(int courseId)
        {
            var model = _studentRepo.GetAllStudents(courseId);
            return View(model);
        }
    }
}
