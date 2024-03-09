using Examination_System.Models;

namespace Examination_System.Repository
{
    public interface IQuestionRepo
    {
        public void AddQuestion(Question question);
        public void AddChoices(int QuestionId, Choice choice);
        
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
        public void AddChoices(int QuestionId,Choice choice)
        {

            db.Choices.Add(choice);
            db.SaveChanges();   
        }


    }
}
