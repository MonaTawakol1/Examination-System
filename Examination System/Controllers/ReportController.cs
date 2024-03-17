using Examination_System.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Examination_System.Controllers
{
    [Authorize(Roles = "Admins,Instructors")]
    public class ReportController : Controller
    {
        IReportRepo reportRepo;
        IstudentRepo studentRepo;

        public ReportController(IReportRepo _reportRepo , IstudentRepo _studentRepo)
        {
            reportRepo = _reportRepo;

            studentRepo = _studentRepo;

        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult StudentsInformation()
        {
            var depts = reportRepo.GetDepartments();
            ViewBag.depts = depts;
            return View();
        }

      
        public IActionResult Reports1(int DeptNumber)
        {
           var depts =  reportRepo.GetDepartments();
            ViewBag.depts = depts;
          var stdList =   reportRepo.StudentsInformation(DeptNumber);
            return View(stdList);
        }
        public IActionResult getAllStudent()
        {
            var students = reportRepo.GetStudents();
            ViewBag.students = students;
            return View();
        }

        public IActionResult Reports2(int StudentId)
        {
           var exams = reportRepo.GetExams(StudentId);
            return View(exams);
        }

        public IActionResult getAllInstuctors()
        {

            var instructor = reportRepo.GetInstructors();
            ViewBag.instructor = instructor;
         
            return View();
        }

        public IActionResult Reports3(int InstructorId)
        {
           
            var Courses = reportRepo.GetCourse(InstructorId);
            

            return View(Courses);
        }


        public IActionResult getAllCourses()
        {

            var courses = reportRepo.GetAllCourses();
            ViewBag.courses = courses;

            return View();
        }

        public IActionResult Reports4(int CourseId)
        {
            var Courses = reportRepo.showtopics(CourseId);
            return View(Courses);
        }

        public IActionResult getAllexams()
        {
            var exams = reportRepo.GetAllExam();
            ViewBag.Exams = exams;
            return View();
        }
        public IActionResult Reports5(int ExamId)
        {
            var examQuestions = reportRepo.GetExamQuestions(ExamId);
            return View(examQuestions);
        }

        public IActionResult getAllStudentExam()
        {
            var Students = reportRepo.GetStudents();
            ViewBag.Students = Students;
            return View();
        }
        public IActionResult studentExams(int StudentId) 
        {
           var exams=reportRepo.GetExams(StudentId);
           
            return View(exams);
        
        
        }
        public IActionResult Reports6(int ExamId)
        {
            var examQuestions = reportRepo.GetExamQuestions(ExamId);
            return View(examQuestions);
        }

    }
}
