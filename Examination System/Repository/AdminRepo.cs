using Examination_System.Models;
using Examination_System.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Examination_System.Repository
{
    public interface IAdminRepo
    {
        public List<User> showAdmins();
        public void AddUser(User user);
        public void AddRole(User user);
        public void AddAdmin(Admin admin);
        public User getUser(int userId);
        public void EditUser(User user);
        public void DeleteUser(User user);
        public void AddRoleInstructor(User user);
        public List<User> GetUsers();
        public void AddInstructor(User user);
        public void EditInstructor(User user);
        public List<User> getStudents();
        public List<Department> getDepartments();
        public List<Branch> getBranches();

        public User AddStudent(RegisterStdVM u);
        public void AddRoleStudent(User user);
        public void AddStudent(User user, RegisterStdVM u);

        public User getUserStudent(int userId);
        public User getOldStudentUsers(User u);
        public void EditUserStudent(User u, User old);
        public void DeleteUserStudent(User user);
    }

    public class AdminRepo :IAdminRepo
    {
        ItiContext db;

        public AdminRepo(ItiContext _db)
        {
            db = _db;
        }
        public List<User> showAdmins()
        {
           return db.Users.Where(a => a.Id == a.Admin.UserId).Include(a => a.Admin).ToList();
        }
        public void AddUser(User user)
        {
            db.Users.Add(user);
            db.SaveChanges();
        }
        public void AddRole (User user)
        {
            var role = db.Roles.FirstOrDefault(a => a.Name == "Admins");
            user.Roles.Add(role);
            db.SaveChanges();
        }
        public void AddAdmin(Admin admin)
        {
            db.Admins.Add(admin);
            db.SaveChanges();
        }

        public User getUser(int userId)
        {
           return db.Users.FirstOrDefault(a => a.Id == userId);
        }

        public void EditUser (User user)
        {
            db.Users.Update(user);
            db.SaveChanges();
        }
        public void DeleteUser(User user)
        {

            db.Users.Remove(user);
            db.SaveChanges();
        }

        public List<User> GetUsers()
        {
           return db.Users.Where(a => a.Id == a.Instructor.UserId).Include(a => a.Instructor).ToList();
        }

        public void AddRoleInstructor(User user)
        {
           var role  =  db.Roles.FirstOrDefault(a => a.Name == "Instructors");
            user.Roles.Add(role);
            db.SaveChanges();
        }

        public void AddInstructor (User user)
        {
            Instructor instructor = new Instructor();
            {
                instructor.UserId = user.Id;
            }
            db.Instructors.Add(instructor);
            db.SaveChanges();
        }
        public void EditInstructor(User user)
        {
            db.Users.Update(user);
            db.SaveChanges();
        }

        public List<User> getStudents()
        {
           return db.Users.Where(a => a.Id == a.Student.UserId).Include(a => a.Student).ThenInclude(a => a.Branch).Include(a => a.Student.Department).ToList();
        }

        public List<Department> getDepartments()
        {
            return db.Departments.ToList(); 
        }

        public List<Branch> getBranches()
        {
            return db.Branches.ToList();
        }
        public User AddStudent(RegisterStdVM u)
        {
             User user = new();
                user.Name = u.Name;
                user.Email = u.Email;
                user.Password = u.Password;
            db.Users.Add(user);
            db.SaveChanges();
            return user;
        }

        public void AddRoleStudent(User user)
        {
            var role = db.Roles.FirstOrDefault(a => a.Name == "Students");
            user.Roles.Add(role);
            db.SaveChanges();
        }

        public void AddStudent(User user , RegisterStdVM u)
        {
            Student student = new Student();
            {
                student.DepartmentId = u.DepartmentId;
                student.branchId = u.branchId;
                student.UserId = user.Id;
            }
            db.Students.Add(student);
            db.SaveChanges();
        }
        
        public User getUserStudent(int userId)
        {
            return db.Users.Include(a => a.Roles).FirstOrDefault(a => a.Id == userId);
        }

        public User getOldStudentUsers(User u)
        {
          return  db.Users.Include(x => x.Roles).Include(x => x.Student).FirstOrDefault(x => x.Id == u.Id);
        }
        public void EditUserStudent(User u ,User old)
        {
            old.Name = u.Name;
            old.Email = u.Email;
            old.Password = u.Password;
            old.Student.DepartmentId = u.Student.DepartmentId;
            old.Student.branchId = u.Student.branchId;
            db.SaveChanges();
        }

        public void DeleteUserStudent(User user )
        {

           var std = db.Students.FirstOrDefault(a => a.UserId == user.Id);
            db.Students.Remove(std);
            db.Users.Remove(user);
            db.SaveChanges();
        }
    }
}
