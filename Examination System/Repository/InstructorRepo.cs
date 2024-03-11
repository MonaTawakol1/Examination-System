using Examination_System.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Examination_System.Repository
{

    public interface IInstructorRepo
    {
        public Instructor ShowCourses(int id);
        public List<Question> ShowQuestions(int id);
        public void DeleteQuestion(int id);
        public int GetCourseIdByQuestionId(int questionId);
    }
    public class InstructorRepo : IInstructorRepo
    {
        ItiContext db;

        public InstructorRepo(ItiContext _db)
        {
            db = _db;
        }

        public Instructor ShowCourses(int id)
        {
            Instructor instructor = db.Instructors.Include(a => a.Courses).FirstOrDefault(a => a.InstructorId == id);
            return instructor;
        }
        public List<Question> ShowQuestions(int id)
        {
           List<Question> question = db.Questions.Include(a=>a.ChoicesList).Where(a=>a.CourseId == id).Where(a => a.isDeleted == false).ToList();
            return question;
          
        }
       public void DeleteQuestion(int id)
        {
            var question = db.Questions.Include(a=>a.ChoicesList).SingleOrDefault(a => a.QuestionId == id);
            if (question != null)
            {

                question.isDeleted = true;
               // db.Questions.Remove(question);
                db.SaveChanges();
            }
        }
        public int GetCourseIdByQuestionId(int questionId)
        {
            var questioncourse = db.Questions.SingleOrDefault(a=>a.QuestionId ==  questionId);
            var courseId = questioncourse.CourseId;
            return courseId;
        }

    }
}
