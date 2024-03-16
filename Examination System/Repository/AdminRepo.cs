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
        public List<Branch> ShowBranches();
        public List<User> getusersbranches();
        public void AddBranch(Branch branch);
        public Branch getBranchById(int id);
        public void UpdateBranch(Branch branch);
        public void DeleteBranch(Branch branch);
        public List<Department> getDepartmentList();
        public void AddDepartment(Department department);
        public Department GetDepartmentById(int id);
        public void UpdateDepartment(Department department);
        public void removeDepartment(Department department);
        public List<Course> GetListOfCourses();
        public List<Instructor> GetInstructorByCourseId(int courseId);
       
        public List<Instructor> ListOfInstructorsNotInCourse(int courseId);

        public void AddInstructorToCourse(int InstructorId, int CourseId);
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
            return db.Branches.Where(a=>a.isDeleted==false).ToList();
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

        public List<Branch> ShowBranches()
        {
            var model = db.Branches.Include(a => a.branchmanger).ThenInclude(a => a.User).Where(a=>a.isDeleted==false).ToList();
            return model;
        }
        public List<User> getusersbranches()
        {
           return db.Users.Where(a => a.Id == a.Instructor.UserId).Include(a => a.Instructor).ToList();
        }

        public void AddBranch(Branch branch)
        {
            db.Branches.Add(branch);
            db.SaveChanges();
        }
        public Branch getBranchById(int id)
        {
            return  db.Branches.FirstOrDefault(a => a.BranchId == id);
        }
        public void UpdateBranch(Branch branch)
        {
            db.Branches.Update(branch);
            db.SaveChanges();
        }
        public void DeleteBranch(Branch branch)
        {
            branch.isDeleted = true;
            db.Branches.Update(branch);
            db.SaveChanges();
        }
        public List<Department> getDepartmentList()
        {
            return db.Departments.Include(a => a.Manager).ThenInclude(a => a.User).Where(a=>a.isDeleted==false).ToList();
        }
        public void AddDepartment(Department department)
        {
            db.Departments.Add(department);
            db.SaveChanges();
        }
        public Department GetDepartmentById(int id)
        {
            return db.Departments.FirstOrDefault(a => a.DepartmentId == id);
        }
        public void UpdateDepartment(Department department)
        {
            db.Departments.Update(department);
            db.SaveChanges();
        }

        public void removeDepartment(Department department)
        {
           department.isDeleted = true;
            db.Departments.Update(department);
            db.SaveChanges();
        }

        public List<Course> GetListOfCourses()
        {
            return db.Courses.Include(a=>a.Instructors).ToList();
        }

        public List<Instructor> GetInstructorByCourseId(int courseId)
        {
            var instructors = db.Instructors.Include(a => a.User).Include(a => a.Courses).ToList();
            List<Instructor> instructorList = new List<Instructor>();
            foreach (var std in instructors)
            {
                foreach (var crs in std.Courses)
                {
                    if (crs.CourseId == courseId)
                    {
                        instructorList.Add(std);
                    }
                }
            }
            return instructorList;
            
        }

        public List<Department> ListOfDepartmentInCourse(int courseId)
        {
            var deptList = db.Departments.Include(a => a.courses).ToList();
            List<Department> departmentsInCourse = new List<Department>();
            foreach (var department in deptList)
            {
                foreach (var crs in department.courses)
                {
                    if (crs.CourseId == courseId)
                    {
                        departmentsInCourse.Add(department);
                    }
                }
            }
            return departmentsInCourse;
        }



        //public List<Instructor> ListOfInstructorsNotInCourse(int courseId)
        //{

        //    var instructors = db.Instructors.Include(a => a.Departments).ToList();

        //    List<Department> departmentsInCourse = ListOfDepartmentInCourse(courseId);


        //    List<Instructor> instructorsNotInCourse = new List<Instructor>();

        //    foreach (var instructor in instructors)
        //    {

        //        if (!instructor.Departments.Any(d => departmentsInCourse.Contains(d)))
        //        {
        //            instructorsNotInCourse.Add(instructor);
        //        }
        //    }

        //    return instructorsNotInCourse;
        //}

        public List<Instructor> ListOfInstructorsNotInCourse(int courseId)
        {
            var instructors = db.Instructors.Include(a => a.Departments).ToList();

            List<Department> departmentsInCourse = ListOfDepartmentInCourse(courseId);

            List<Instructor> instructorsNotInCourse = new List<Instructor>();

            foreach (var instructor in instructors)
            {
                // Check if any department of the instructor is not in the departments of the course
                if (instructor.Departments.Any(d => !departmentsInCourse.Contains(d)))
                {
                    instructorsNotInCourse.Add(instructor);
                }
            }

            return instructorsNotInCourse;
        }



        public void AddInstructorToCourse(int InstructorId, int CourseId)
        {
            var instructor = db.Instructors.FirstOrDefault(a => a.InstructorId == InstructorId);
            var course = db.Courses.Include(a => a.Instructors).FirstOrDefault(a => a.CourseId == CourseId);
            course.Instructors.Add(instructor);
            db.Courses.Update(course);
            db.SaveChanges();
        }


     



    }
}
