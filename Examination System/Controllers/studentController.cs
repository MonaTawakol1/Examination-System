using Microsoft.AspNetCore.Mvc;
using Examination_System.Models;
using Examination_System.Repository;
using System.Security.Claims;
namespace Examination_System.Controllers
{
    public class studentController : Controller
    {
        IstudentRepo studentRepo;
        IExamRepo ExamRepo;
        IChoiceRepo choiceRepo;

        public studentController(IstudentRepo _studentRepo, IExamRepo examRepo, IChoiceRepo choiceRepo)
        {
            studentRepo = _studentRepo;
            ExamRepo = examRepo;
            this.choiceRepo = choiceRepo;
        }
        //public IActionResult Index()
        //{
        //    var principal = HttpContext.User;
        //    int id;

        //    // Retrieve the Sid claim
        //    var sidClaim = principal.FindFirst(ClaimTypes.Sid);



        //         var sidValue = sidClaim.Value;
        //         id = int.Parse(sidValue);
        //         var student = studentRepo.GetStudentByUserId(id);





        //    return View(student);
        //}

        public async Task<IActionResult> Index()
        {
            // Get the ClaimsPrincipal from the HttpContext
            var principal = HttpContext.User;

            // Retrieve the Sid claim
            var sidClaim = principal.FindFirst(ClaimTypes.Sid);

            if (sidClaim != null)
            {
                // Sid value found, you can access it here
                var sidValue = sidClaim.Value;
                int id = int.Parse(sidValue);
                var student = studentRepo.GetStudentByUserId(id);

                // Do something with the student data
                return View(student);
            }
            else
            {
                // Sid claim not found, handle the situation accordingly
                return RedirectToAction("Login", "Account");
            }
        }
        public IActionResult ShowCourses(int id)
        {
            ViewBag.stdid = id;
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

        public IActionResult StartExam(int id, int Studentid)
        {
            Exam exam=new Exam() { CourseId=id,StudentId=Studentid};
            ExamRepo.AddExam(exam);
            int ExamId=exam.ExamId;
            ExamRepo.AddExamQuestions(ExamId, id);
            List<ExamQuestions> examQuestion= ExamRepo.ShowRandomQuestions(ExamId);
            return View(examQuestion);
        }

        public IActionResult CorrectExam(int examId)
        {
            List<ExamQuestions> examQuestion = ExamRepo.ShowRandomQuestions(examId); 
            foreach(ExamQuestions questions in examQuestion)
            {
                int grade = questions.Exam.StudentGrade.Value;
                ViewBag.grade = grade;
            }
            return View(examQuestion);
        }


        [HttpPost]
        public IActionResult StartExam(int ExamId, Dictionary<int, int> ChoiceIds)
        {
            foreach (var questionId in ChoiceIds.Keys)
            {
                var choiceId = ChoiceIds[questionId];
                ExamRepo.AddExamAnswer(questionId, new List<int> { choiceId }, ExamId); 
            }

            ExamRepo.CorrectExam(ExamId);
            ExamRepo.AddExamGrade(ExamId);
            return RedirectToAction("CorrectExam" , new { examId = ExamId });
        }


    }
}
