using Azure.Core;
using Examination_System.Models;
using Examination_System.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Examination_System.Controllers
{
    public class InstructorController : Controller
    {

        IInstructorRepo instructorRepo;

        public InstructorController(IInstructorRepo _instructorRepo)
        {
            instructorRepo = _instructorRepo;
        }
        public IActionResult Index(int id) { 
      
            var instructor = instructorRepo.ShowCourses(id);
            var courses = instructor.Courses.ToList();
            return View(courses);
        }
    


        public IActionResult AddNewQuestion(int id)
        {

            ViewBag.id = id;
            return View();
        }

        [HttpPost]
        public IActionResult AddNewQuestion(Question question, List<String> choicesBody)
        {

            return View();
        }
    }
}
