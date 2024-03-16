using Examination_System.Models;
using Examination_System.Repository;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using static Examination_System.Repository.ICourseRepo;

namespace Examination_System
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<ItiContext>(a =>
            {
                a.UseSqlServer(builder.Configuration.GetConnectionString("Data Source=.;Initial Catalog=ExaminationSystem;Integrated Security=True;Encrypt=True;Trust Server Certificate=True"));
            });

            builder.Services.AddTransient<IstudentRepo, StudentRepo>();
            builder.Services.AddTransient<IInstructorRepo, InstructorRepo>();
            builder.Services.AddTransient<IQuestionRepo, QuestionRepo>();
            builder.Services.AddTransient<IExamRepo, ExamRepo>();
            builder.Services.AddTransient<IChoiceRepo, ChoiceRepo>();
            builder.Services.AddTransient<IReportRepo, ReportRepo>();
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();   //-------------added----------


                builder.Services.AddTransient(typeof(IInstructorRepo), typeof(InstructorRepo));
                builder.Services.AddTransient(typeof(ICourseRepo), typeof(CourseRepo));
                builder.Services.AddTransient(typeof(IStudentRepo), typeof(StudentRepo));
            var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();        //--------added for cookie-----------


            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");


            app.Run();
        }
    }
}
