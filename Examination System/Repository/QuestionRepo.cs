using Examination_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Examination_System.Repository
{
    public interface IQuestionRepo
    {
        public void AddQuestion(Question question);
        public void AddChoices(Choice choice);
        public Question GetQuestion(int id);
        public List<Choice> GetQuestionChoices(Question q);
        public Choice getQuestionAnswer(Question q);
        public Course getQuestionCourse(Question q);
        public void updateQuestion(Question q);


    }
    public class QuestionRepo : IQuestionRepo
    {
        ItiContext db;
        public QuestionRepo(ItiContext _db)
        {
            db = _db;
        }
        public void AddQuestion(Question question)

        {
            
            db.Questions.Add(question);
            db.SaveChanges();
        }
        public void AddChoices(Choice choice)
        {
             
            db.Choices.Add(choice);
            db.SaveChanges();   
        }

        public Question GetQuestion(int id)
        {
            Question q= db.Questions.Include(a=>a.ChoicesList).SingleOrDefault(a=>a.QuestionId==id);
            return q;
        }

        public List<Choice> GetQuestionChoices(Question q)
        {
            List<Choice> QuestionchoicesList = new List<Choice>();


                var choice = db.Choices.Where(a => a.QuestionId == q.QuestionId).ToList();
                foreach(var item in choice)
                {
                    if (item.IsAnswer == false)
                    {
                        QuestionchoicesList.Add(item);

                    }
                }
               
               return QuestionchoicesList;
        }

        public Choice getQuestionAnswer(Question q)
        {
            var answer = db.Choices.FirstOrDefault(a => a.QuestionId == q.QuestionId && a.IsAnswer);
            return answer;

        }

        public Course getQuestionCourse(Question q)
        {
            var course=db.Courses.FirstOrDefault(a=>a.CourseId == q.CourseId);

            return course;
        }

        public void updateQuestion(Question q)
        {
            db.Questions.Update(q);
            db.SaveChanges();
        }




    }
}
