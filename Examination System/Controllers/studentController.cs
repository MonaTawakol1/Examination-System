using Microsoft.AspNetCore.Mvc;
using Examination_System.Models;
using Examination_System.Repository;
namespace Examination_System.Controllers
{
    public class studentController : Controller
    {
        IstudentRepo studentRepo;

        public studentController(IstudentRepo _studentRepo)
        {
            studentRepo = _studentRepo;
        }
        public IActionResult Index(int id)
        {
            var student = studentRepo.GetStudent(id);
            return View(student);
        }
        public IActionResult ShowCourses(int id)
        {
            var std = studentRepo.ShowCourses(id);
           var courses = std.Courses.ToList();
            return View(courses);
        }
        public IActionResult showTopics(int id) 
        {

            var cr = studentRepo.showtopics(id);
            var topics = cr.Topics.ToList();    
            return View(topics);
        }
    }
}
