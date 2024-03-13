using Examination_System.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Examination_System.Controllers
{
    public class ReportController : Controller
    {
        IReportRepo reportRepo;

        public ReportController(IReportRepo _reportRepo)
        {
            reportRepo = _reportRepo;
            
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
    }
}
