using Microsoft.AspNetCore.Mvc;
using Examination_System.Models;
using Examination_System.Repository;
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

        public IActionResult StartExam(int id, int Studentid)
        {
            Exam exam=new Exam() { CourseId=id,StudentId=Studentid};
            ExamRepo.AddExam(exam);
            int ExamId=exam.ExamId;
            ExamRepo.AddExamQuestions(ExamId, id);
            List<ExamQuestions> examQuestion= ExamRepo.ShowRandomQuestions(ExamId);
            return View(examQuestion);
        }

        [HttpPost]
        public IActionResult StartExam(int ExamId, Dictionary<int, int> ChoiceIds)
        {
            foreach (var questionId in ChoiceIds.Keys)
            {
                var choiceId = ChoiceIds[questionId];
                ExamRepo.AddExamAnswer(questionId, new List<int> { choiceId }, ExamId); // Pass choiceId as a list
            }

            return RedirectToAction("ExamResult");
        }


    }
}
