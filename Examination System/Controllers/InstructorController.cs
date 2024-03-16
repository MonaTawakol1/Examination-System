using Examination_System.Models;
using Examination_System.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Examination_System.Controllers
{
    public class InstructorController : Controller
    {
        

        IInstructorRepo instructorRepo;
        IQuestionRepo questionRepo;
        IExamRepo examRepo;
        IstudentRepo studentRepo;

        public InstructorController(IInstructorRepo _instructorRepo , IQuestionRepo _questionRepo, IExamRepo _examRepo, IstudentRepo _studentRepo)
        {
            instructorRepo = _instructorRepo;
            questionRepo = _questionRepo;
             examRepo = _examRepo;
            studentRepo = _studentRepo;
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
        

          
          
            return RedirectToAction("ShowQuestions", new { id = id });

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
         


            return View(question);
        }

        [HttpPost]
        public IActionResult EditQuestion(Question q)
        {
            

            questionRepo.updateQuestion(q);
            return RedirectToAction("Index");
        }

        public IActionResult AddExam(int id)
        {
           Course crs =  examRepo.getCourseById(id);
            return View(crs);
        }

        [HttpPost]
        public IActionResult AddExam(int id , DateTime ExamStartDateTime, DateTime ExamEndDateTime, int NumberOfTrueAndFalseQuestions, int NumberOfMcqQuestions)
        {
            examRepo.AddExamDuration(id, ExamStartDateTime, ExamEndDateTime, NumberOfTrueAndFalseQuestions, NumberOfMcqQuestions);
            return RedirectToAction("Index");
        }
        public IActionResult GetInstructor( int departmentId)
        {
            var principal = HttpContext.User;

           
            var sidClaim = principal.FindFirst(ClaimTypes.Sid);

            if (sidClaim != null)
            {
                // Sid value found, you can access it here
                var sidValue = sidClaim.Value;
                int id = int.Parse(sidValue);
                var instructor = instructorRepo.GetInstructor(id);
                ViewBag.deptId = departmentId;
                
                return View(instructor);
            }
            else
            {
               
                return RedirectToAction("Login", "Account");
            }
        }

        public IActionResult getCoursesInDepartments(int instructorId , int departmentId)
        {
            List<Course> courses = instructorRepo.getCoursesInDepartmentss(instructorId, departmentId);
            ViewBag.deptId = departmentId;
            return View(courses);
        }
        public IActionResult getListOfStudents(int courseId)
        {
            var std = instructorRepo.getListOfStudents(courseId);
            ViewBag.courseId = courseId;
            return View(std);
        }


        public IActionResult getStudentToCourse (int courseId, int departmentId)
        {
          List<Student> stds = instructorRepo.GetStudentNotInCourse(courseId, departmentId);
            ViewBag.Students = stds;
            ViewBag.CourseId = courseId;
            return View();
        }

        [HttpPost]
        public IActionResult getStudentToCourse(int CourseId, int studentId ,int departmentId)
        {
          var std =   studentRepo.GetStudent(studentId);
            instructorRepo.Add(std, CourseId);
            return RedirectToAction("getListOfStudents", new { CourseId = CourseId });
        }

        public IActionResult RemoveStudent(int studentId , int courseId)
        {
            instructorRepo.RemoveStudentFromCourse(studentId, courseId);
            return RedirectToAction("getListOfStudents", new { CourseId = courseId });
        }
    }

    
}
