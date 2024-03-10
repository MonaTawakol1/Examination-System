using Azure.Core;
using Examination_System.Models;
using Examination_System.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Examination_System.Controllers
{
    public class InstructorController : Controller
    {

        IInstructorRepo instructorRepo;
        IQuestionRepo questionRepo;

        public InstructorController(IInstructorRepo _instructorRepo , IQuestionRepo _questionRepo)
        {
            instructorRepo = _instructorRepo;
            questionRepo = _questionRepo;
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
        public IActionResult AddNewQuestion(Question question, List<String> choicesBody, string choiceAnswer ,int id)
        {
            question.CourseId = id;
            questionRepo.AddQuestion(question);
            int idd = question.QuestionId;
            foreach (var choice in choicesBody)
            {

                if (choice != null)
                {
                    Choice ch1 = new Choice() { QuestionId = idd, ChoiceBody = choice, IsAnswer = false };
                    questionRepo.AddChoices(ch1);
                }
                
            }

            Choice ch = new Choice() { QuestionId = idd, ChoiceBody = choiceAnswer, IsAnswer = true };
            questionRepo.AddChoices(ch);


            return View();
        }
    }
}
