using Examination_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Examination_System.Repository
{

    public interface IExamRepo
    {
        public void AddExam(Exam exam);
        public void AddExamQuestions(int Examid,int CrsId);
        public List<ExamQuestions> ShowRandomQuestions(int Examid);
        public void AddExamAnswer(int questionId, List<int> choiceIds, int examId);
    }
    public class ExamRepo:IExamRepo
    {
        ItiContext db;

        public ExamRepo(ItiContext _db)
        {
            db = _db;
        }


        public void AddExam(Exam exam)
        {
            db.Add(exam);
            db.SaveChanges();
            
        }


        public void AddExamQuestions(int Examid, int CrsId)
        {
            var random = new Random();
            
            List<int> ids = new List<int>();
            var questionsBank = db.Questions.Where(x => x.CourseId == CrsId).Where(a=>a.isDeleted==false).ToList();
            questionsBank.ForEach
                (x =>
                {
                    ids.Add(x.QuestionId);
                });
        

            Random rand = new Random();
            int n = 10;
            while (n > 1)
            {
                n--;
                int k = rand.Next(n + 1);
                var value = ids[k];
                ids[k] = ids[n];
                ids[n] = value;
            }
        
            foreach (var item in ids)
            {
                ExamQuestions e = new ExamQuestions() { ExamId = Examid, QuestionId = item, InsertedAt = DateTime.Now };
                db.ExamQuestions.Add(e);
            
              
            }
            db.SaveChanges();

        }


        public List<ExamQuestions> ShowRandomQuestions(int Examid)
        {
            var questionsInOrder = db.ExamQuestions.Where(a => a.ExamId == Examid).Include(a=>a.Question).ThenInclude(b=>b.ChoicesList).OrderBy(e => e.InsertedAt).ToList();
            return questionsInOrder;
        }

        public void AddExamAnswer(int questionId, List<int> choiceIds, int examId)
        {
            var examQuestion = db.ExamQuestions.FirstOrDefault(a => a.ExamId == examId && a.QuestionId == questionId);
            if (examQuestion != null)
            {
                examQuestion.ExamAnswers = choiceIds.First(); // Assuming only one choice is selected
                db.SaveChanges();
            }
        }
    }
}
