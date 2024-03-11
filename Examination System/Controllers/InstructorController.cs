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

        public IActionResult ShowQuestions(int id)
        {
            List<Question> question = instructorRepo.ShowQuestions(id);
            return View(question);
        }
        public IActionResult DeleteQuestion(int id)
        {
            int courseId = instructorRepo.GetCourseIdByQuestionId(id);
            instructorRepo.DeleteQuestion(id);
            return RedirectToAction("ShowQuestions", new { id = courseId });
        }


        public IActionResult EditQuestion(int id)
        {
            Question question = questionRepo.GetQuestion(id);
            Choice Answer = questionRepo.getQuestionAnswer(question);
            List<Choice> choices = questionRepo.GetQuestionChoices(question);
            int courseId= questionRepo.getQuestionCourse(question).CourseId;
            EditQuestionViewModel editQuestionViewModel = new EditQuestionViewModel()
            {
                question = questionRepo.GetQuestion(id),
                Answer= Answer,
                Choices= choices,
                CourseId= courseId  
            };

            
          return View(editQuestionViewModel);
        }

        [HttpPost]
        public IActionResult EditQuestion(EditQuestionViewModel equestion, List<String> choicesBody,int id)
        {

            

            return View();
        }
    }
}
