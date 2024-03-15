using Examination_System.Models;
using Examination_System.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Examination_System.Controllers
{
    public class InstructorController : Controller
    {

        IInstructorRepo instructorRepo;
        IQuestionRepo questionRepo;
        IExamRepo examRepo;

        public InstructorController(IInstructorRepo _instructorRepo , IQuestionRepo _questionRepo, IExamRepo _examRepo)
        public IInstructorRepo insRepo { get; set; }
        public ICourseRepo _courseRepo { get; set; }
        public IStudentRepo _studentRepo { get; set; }
        public InstructorController(IInstructorRepo instructorRepo, ICourseRepo courseRepo, IStudentRepo studentRepo)
        {
            instructorRepo = _instructorRepo;
            questionRepo = _questionRepo;
             examRepo = _examRepo;
        }
        public  async Task< IActionResult> Index() {

            // Get the ClaimsPrincipal from the HttpContext
            var principal = HttpContext.User;

            // Retrieve the Sid claim
            var sidClaim = principal.FindFirst(ClaimTypes.Sid);

            
                // Sid value found, you can access it here
                var sidValue = sidClaim.Value;
                int id = int.Parse(sidValue);
                Instructor instructor = instructorRepo.GetInstructorByUserId(id);

                // Do something with the student data
               // return View(student);
           
            //var instructor = instructorRepo.ShowCourses(id);
            var courses = instructor.Courses.ToList();
            return View(courses);
        }
    


        public IActionResult AddNewQuestion(int id)
        {
            var model = insRepo.GetAllCourses(instructorId);
            return View(model);

            ViewBag.id = id;
            return View();
        }
        //static int courseId = 2;
        public IActionResult ShowTopics(int CourseId)

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
            var model = _courseRepo.GetAllTopics(CourseId);
            return View(model);
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
            var model = _studentRepo.GetAllStudents(courseId);
            ViewBag.CourseId = courseId;
            return View(model);
            int courseId = instructorRepo.GetCourseIdByQuestionId(id);
            instructorRepo.DeleteQuestion(id);
            return RedirectToAction("ShowQuestions", new { id = courseId });
        }
        [HttpGet]
        public IActionResult AddStudent(int courseId)


        public IActionResult EditQuestion(int id)
        {
            ViewBag.StdList = _studentRepo.GetAllStudents();
            ViewBag.CourseId = courseId;
            return View();
            Question question = questionRepo.GetQuestion(id);
            //Choice Answer = questionRepo.getQuestionAnswer(question);
            //List<Choice> choices = questionRepo.GetQuestionChoices(question);
            //int courseId = questionRepo.getQuestionCourse(question).CourseId;
            //EditQuestionViewModel editQuestionViewModel = new EditQuestionViewModel()
            //{
            //    question = questionRepo.GetQuestion(id),
            //    Answer = Answer,
            //    Choices = choices,
            //    CourseId = courseId
            //};


            return View(question);
        }

        [HttpPost]
        public IActionResult AddStudent(int courseId, int StudentId)
        public IActionResult EditQuestion(Question q)
        {
            _studentRepo.AddStudentToCourse(courseId, StudentId);
            return RedirectToAction("ShowStudents", new { courseId = courseId });


            questionRepo.updateQuestion(q);
            return RedirectToAction("ShowQuestions");
        }
        [HttpGet]
        public IActionResult UpdateStudent(int courseId, int studentId)

        public IActionResult AddExam(int id)
        {
            var student = _studentRepo.GetStudentById(studentId);
            ViewBag.CourseId = courseId;
            return View(student);
           Course crs =  examRepo.getCourseById(id);
            return View(crs);
        }

        [HttpPost]
        public IActionResult AddExam(int id , DateTime ExamStartDateTime, DateTime ExamEndDateTime, int NumberOfTrueAndFalseQuestions, int NumberOfMcqQuestions)
        {
            _studentRepo.RemoveStudentFromCourse(courseId, studentId);
            return RedirectToAction("ShowStudents", new { courseId = courseId });
            examRepo.AddExamDuration(id, ExamStartDateTime, ExamEndDateTime, NumberOfTrueAndFalseQuestions, NumberOfMcqQuestions);
            return View();
        }
    }
}
