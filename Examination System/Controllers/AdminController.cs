using Examination_System.Models;
using Examination_System.Repository;
using Examination_System.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Examination_System.Controllers
{
    [Authorize(Roles = "Admins")]
    public class AdminController : Controller
    {
        IAdminRepo adminRepo;
        public AdminController(IAdminRepo _adminRepo)
        {
            adminRepo = _adminRepo;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ShowAdmins()
        {
            var model = adminRepo.showAdmins();
            return View(model);
        }
        public IActionResult createAdmins()
        {
            return View();
        }
        [HttpPost]
        public IActionResult createAdmins(User user)
        {
            if (ModelState.IsValid)
            {
                adminRepo.AddUser(user);

                adminRepo.AddRole(user);
                 Admin admin = new Admin();
                {
                    admin.UserId = user.Id;
                }

                adminRepo.AddAdmin(admin);
                return RedirectToAction("ShowAdmins");
            }
            return View(user);
        }

        public IActionResult EditAdmin(int? id)
        {

            var model = adminRepo.getUser(id.Value);
              return View(model);

        }


        [HttpPost]
        public IActionResult EditAdmin(User u)
        {
            adminRepo.EditUser(u);
            return RedirectToAction("ShowAdmins");

        }

        public IActionResult DeleteAdmin(int id)
        {

            var model = adminRepo.getUser(id);
            adminRepo.DeleteUser(model);
            return RedirectToAction("ShowAdmins");
        }

        public IActionResult ShowInstructor()
        {
            var model = adminRepo.GetUsers();
            return View(model);
        }
        public IActionResult createInstructor()
        {
            return View();
        }
        [HttpPost]
        public IActionResult createInstructor(User user)
        {
            if (ModelState.IsValid)
            {

                adminRepo.AddUser(user);

                adminRepo.AddRoleInstructor(user);

                adminRepo.AddInstructor(user);
                return RedirectToAction("ShowInstructor");
            }
            return View(user);
        }

        public IActionResult EditInstructor(int? id)
        {
           
            var model = adminRepo.getUser(id.Value);
         
            return View(model);

        }


        [HttpPost]
        public IActionResult EditInstructor(User user)
        {
            adminRepo.EditInstructor(user);
            return RedirectToAction("ShowInstructor");

        }
        public IActionResult DeleteInstructor(int id)
        {
            if (id == null)
                return BadRequest();
            var model = adminRepo.getUser(id);
            if (model == null)
                return NotFound();

            adminRepo.DeleteUser(model);
            return RedirectToAction("ShowInstructor");
        }

        public IActionResult ShowStudent()
        {
            var model = adminRepo.getStudents();
            return View(model);
        }
        public IActionResult createStudent()
        {
            ViewBag.deptList = adminRepo.getDepartments();
            ViewBag.deptList2 = adminRepo.getBranches();

            return View();
        }


        [HttpPost]
        public IActionResult createStudent(RegisterStdVM u)
        {
            if (ModelState.IsValid)
            {

               User user =  adminRepo.AddStudent(u);
                adminRepo.AddRoleStudent(user);

                adminRepo.AddStudent(user, u);
                return RedirectToAction("ShowStudent");
            }
            return View(u);
        }

        public IActionResult EditStudent(int? id)
        {
            ViewBag.deptList = adminRepo.getDepartments();
            ViewBag.deptList2 = adminRepo.getBranches();

            var model = adminRepo.getUserStudent(id.Value);
            return View(model);

        }


        [HttpPost]
        public IActionResult EditStudent(User u)
        {
            var old = adminRepo.getOldStudentUsers(u);
            adminRepo.EditUserStudent(u,old);
            return RedirectToAction("ShowStudent");

        }

        public IActionResult DeleteStudent(int id)
        {
            var model = adminRepo.getUser(id);

            adminRepo.DeleteUserStudent(model);
           
            return RedirectToAction("ShowStudent");
        }

    }


}
