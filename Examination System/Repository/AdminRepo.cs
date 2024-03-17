using Examination_System.Models;
using Examination_System.ViewModels;
using Humanizer;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Net;

namespace Examination_System.Repository
{
    public interface IAdminRepo
    {
        public List<User> showAdmins();
        public void AddUser(User user);
        public Admin GetAdmin(int userId);
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
        public List<Instructor> getInstructorsInCourse(int CourseId);
        public void RemoveInsFromCourse(int courseId, int InstructorId);
        public bool IsEmailAlreadyRegistered(string email);
        public void deleteIns(Instructor instructor);
        public Instructor getInstructorById(int id);



        //new 
        public List<Instructor> ListOfInstructorsNotInDepartment(int deptId);
        public void AddInstructorToDepartment(int InstructorId, int deptId);
        public List<Instructor> getInstructorsInDepartment(int deptId);

        public void RemoveInsFromDepartment(int departmentId, int InstructorId);
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
           return db.Users.Where(a => a.Id == a.Instructor.UserId && a.Instructor.isDeleted==false).Include(a => a.Instructor).ToList();
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
            var branches = db.Branches.ToList();
            var managersIds = branches.Select(branch => branch.BranchManagerId).ToList();

            var instructors = db.Users
                                .Include(u => u.Instructor)
                                .Where(u => u.Instructor != null && !u.Instructor.isDeleted && u.Id == u.Instructor.UserId && !managersIds.Contains(u.Id))
                                .ToList();

            return instructors;
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
            var instructors = db.Instructors.Include(a => a.User).Include(a => a.Courses).Where(a=>a.isDeleted==false).ToList();
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


        public List<Instructor> ListOfInstructorsNotInCourse(int courseId)
        {
            var instructors = db.Instructors.Include(a => a.Departments).Include(a => a.Courses).Where(a => a.isDeleted == false).ToList();

            // List to store instructors who are teaching the given course
            List<Instructor> instructorsInCourse = new List<Instructor>();

            // Find instructors teaching the given course
            foreach (var ins in instructors)
            {
                foreach (var crs in ins.Courses)
                {
                    if (crs.CourseId == courseId)
                    {
                        instructorsInCourse.Add(ins);
                        break; // Once found, no need to continue searching
                    }
                }
            }

            // Retrieve departments associated with the given course
            List<Department> departmentsInCourse = ListOfDepartmentInCourse(courseId);

            // List to store instructors not teaching the given course
            List<Instructor> notInCourse = new List<Instructor>();

            // Find instructors who are in departments associated with the course but not teaching it
            foreach (var instructor in instructors)
            {
                // Check if the instructor is in any of the departments associated with the course
                bool isInCourseDepartment = departmentsInCourse != null && departmentsInCourse.Any(dept => dept.instructors != null && dept.instructors.Contains(instructor));

                // If the instructor is in one of the departments but not teaching the course, add them to the list
                if (isInCourseDepartment && !instructorsInCourse.Contains(instructor))
                {
                    notInCourse.Add(instructor);
                }
            }

            return notInCourse;
        }



        public void AddInstructorToCourse(int InstructorId, int CourseId)
        {
            var instructor = db.Instructors.FirstOrDefault(a => a.InstructorId == InstructorId);
            var course = db.Courses.Include(a => a.Instructors).FirstOrDefault(a => a.CourseId == CourseId);
            course.Instructors.Add(instructor);
            db.Courses.Update(course);
            db.SaveChanges();
        }

        public List<Instructor> getInstructorsInCourse(int CourseId)
        {

            var instructors = db.Instructors.Include(a => a.Departments).Include(a => a.Courses).Where(a => a.isDeleted == false).ToList();

            // List to store instructors who are teaching the given course
            List<Instructor> instructorsInCourse = new List<Instructor>();

            // Find instructors teaching the given course
            foreach (var ins in instructors)
            {
                foreach (var crs in ins.Courses)
                {
                    if (crs.CourseId == CourseId)
                    {
                        instructorsInCourse.Add(ins);
                        break; // Once found, no need to continue searching
                    }
                }
            }

            return instructorsInCourse;
        }

        public void RemoveInsFromCourse(int courseId,int InstructorId)
        {
            var instructor=db.Instructors.FirstOrDefault(a=>a.InstructorId== InstructorId);
            var course = db.Courses.Include(a => a.Instructors).FirstOrDefault(a => a.CourseId == courseId);
            course.Instructors.Remove(instructor);
            db.Courses.Update(course);
            db.SaveChanges();
        }

        public bool IsEmailAlreadyRegistered(string email)
        {
            // Assuming your DbContext is named ApplicationDbContext
            return db.Users.Any(u => u.Email == email);
        }


        public Admin GetAdmin(int userId)
        {
            return db.Admins.Include(a=>a.User).FirstOrDefault(u => u.UserId == userId);
        }


        public void deleteIns(Instructor instructor)
        {
            instructor.isDeleted = true;
            db.Instructors.Update(instructor);
            db.SaveChanges();
        }
        public Instructor getInstructorById(int id)
        {
            return db.Instructors.FirstOrDefault(a => a.UserId == id);
        }




     
        ////// new
        ///

        public List<Instructor> ListOfInstructorsNotInDepartment(int deptId)
        {
            var instructors = db.Instructors.Include(a => a.Departments).Where(a => a.isDeleted == false).ToList();

            List<Instructor> notInDept = new List<Instructor>();
            foreach (var instructor in instructors)
            {
                bool isInDept = false;
                foreach (var dept in instructor.Departments)
                {
                    if (dept.DepartmentId == deptId)
                    {
                        isInDept = true;
                        break;
                    }
                }

                if (!isInDept)
                {
                    notInDept.Add(instructor);
                }
            }

            return notInDept;
        }



        public void AddInstructorToDepartment(int InstructorId, int deptId)
        {
            var instructor = db.Instructors.FirstOrDefault(a => a.InstructorId == InstructorId);
            var dept = db.Departments.Include(a => a.instructors).FirstOrDefault(a => a.DepartmentId == deptId);
            dept.instructors.Add(instructor);
            db.Departments.Update(dept);
            db.SaveChanges();
        }

        public List<Instructor> getInstructorsInDepartment(int deptId)
        {

            var instructors = db.Instructors.Include(a => a.Departments).Where(a => a.isDeleted == false).ToList();
            List<Instructor> instructorsIndept = new List<Instructor>();


            foreach (var ins in instructors)
            {
                foreach (var dept in ins.Departments)
                {
                    if (dept.DepartmentId == deptId)
                    {
                        instructorsIndept.Add(ins);
                        break;
                    }
                }
            }

            return instructorsIndept;
        }

        public void RemoveInsFromDepartment(int departmentId, int InstructorId)
        {
            var instructor = db.Instructors.FirstOrDefault(a => a.InstructorId == InstructorId);
            var dept = db.Departments.Include(a => a.instructors).FirstOrDefault(a => a.DepartmentId == departmentId);
            dept.instructors.Remove(instructor);
            db.Departments.Update(dept);
            db.SaveChanges();
        }












    }
}
