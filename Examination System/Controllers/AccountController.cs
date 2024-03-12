using Examination_System.Models;
using Examination_System.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Examination_System.Controllers
{
    public class AccountController : Controller
    {   ItiContext db=new ItiContext();

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            //check data
            var res = db.Users.Include(a => a.Roles).FirstOrDefault(a => a.Email == model.Email && a.Password == model.Password);
            if (res == null)
            {
                ModelState.AddModelError("", "Username and Password Invalid");
                return View(model);
            }

            string userid=res.Id.ToString();
            //add claims
            Claim c1 = new Claim(ClaimTypes.Name, res.Name);
            Claim c2 = new Claim(ClaimTypes.Email, res.Email);
            Claim c3 = new Claim(ClaimTypes.Sid, userid);
            List<Claim> Roleclaims = new List<Claim>();
            foreach (var item in res.Roles)
            {
                Roleclaims.Add(new Claim(ClaimTypes.Role, item.Name));

            }


            ClaimsIdentity ci = new ClaimsIdentity("Cookies");
            ci.AddClaim(c1);
            ci.AddClaim(c2);
            ci.AddClaim(c3);
            foreach (var item in Roleclaims)
            {
                ci.AddClaim(item);
            }

            ClaimsPrincipal cp = new ClaimsPrincipal();
            cp.AddIdentity(ci);

            await HttpContext.SignInAsync(cp);
            if (ModelState.IsValid)
            {
                foreach (var item in res.Roles)
                {
                    if (item.Name == "Admins")
                    {
                        return RedirectToAction("Index", "Admin");

                    }
                    else if (item.Name == "Students")
                    {
                        return RedirectToAction("Index", "Student");

                    }
                    else if (item.Name == "Instructors")
                    {
                        return RedirectToAction("Index", "Instructor");

                    }
                }
            }
            //return RedirectToAction("login");
            return View(model);

        }
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction("Index", "Home");

        }
    }
}
