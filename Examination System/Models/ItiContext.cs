using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Examination_System.Models
{
    public class ItiContext : DbContext
    {

        public DbSet<Branch> Branches { get; set; }
        public DbSet<Choice> Choices { get; set; }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<ExamQuestions> ExamQuestions { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public ItiContext()
        {

        }
        public ItiContext(DbContextOptions options) : base()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=ExaminationSystem;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           // modelBuilder.Entity<ExamQuestions>().HasKey(c => new { c.ExamId, c.QuestionId });


            //ins dept
            modelBuilder.Entity<Instructor>()
                .HasOne(i => i.Department)
                .WithOne(b => b.Manager)
                .HasForeignKey<Department>(d => d.ManagerId)
                .OnDelete(DeleteBehavior.ClientSetNull);


            //ins dept many to many

            modelBuilder.Entity<Instructor>()
                .HasMany(i => i.Departments)
                .WithMany(b => b.instructors);



            //modelBuilder.Entity<Choice>()
            //    .HasOne(b => b.Question)
            //    .WithOne(i => i.ModelAnswer)
            //    .HasForeignKey<Question>(d => d.ModelAnswerId)
            //    .OnDelete(DeleteBehavior.ClientSetNull);


            //ques choices
            //modelBuilder.Entity<Question>()
            //    .HasMany(b => b.ChoicesList)
            //    .WithOne().OnDelete(DeleteBehavior.ClientSetNull);


            ////exam ques
            ///


            modelBuilder.Entity<ExamQuestions>()
               .HasKey(eq => new { eq.ExamId, eq.QuestionId });

            //modelBuilder.Entity<ExamQuestions>()
            //    .HasOne(eq => eq.Exam)
            //    .WithMany(e => e.ExamQuestions)
            //    .HasForeignKey(eq => eq.ExamId).OnDelete(DeleteBehavior.ClientSetNull); 

            //modelBuilder.Entity<ExamQuestions>()
            //    .HasOne(eq => eq.Question)
            //    .WithMany(q => q.ExamQuestions)
            //    .HasForeignKey(eq => eq.QuestionId).OnDelete(DeleteBehavior.ClientSetNull);



            //modelBuilder.Entity<Exam>()
            //    .HasMany(p => p.)
            //    .WithMany(t => t.Exams)
            //    .UsingEntity<ExamQuestions>(
            //    j => j
            //        .HasOne(pt => pt.Question)
            //        .WithMany(t => t.ExamQuestions)
            //        .HasForeignKey(pt => pt.QuestionId).OnDelete(DeleteBehavior.ClientSetNull),
            //    j => j
            //        .HasOne(pt => pt.Exam)
            //        .WithMany(p => p.ExamQuestions)
            //        .HasForeignKey(pt => pt.ExamId).OnDelete(DeleteBehavior.ClientSetNull)
            //        ,
            //    j =>
            //    {
            //        j.HasKey(t => new { t.QuestionId, t.ExamId });
            //    }
            //    );

            base.OnModelCreating(modelBuilder);

    

        }

    }
}
