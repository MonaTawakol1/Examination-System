using Examination_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Examination_System.Repository
{

    public interface IExamRepo
    {


        public void AddExam(Exam exam);
        public void AddExamQuestions(int Examid,int CrsId);
        public List<ExamQuestions> ShowRandomQuestions(int Examid);
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
            //return all questions in course
            List<int> ids = new List<int>();
            var questionsBank = db.Questions.Where(x => x.CourseId == CrsId).ToList();
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
            //for loop l7d 3add el as2la w b3den random.next(0,10)
            // examquestions.add(list(random no.)

            foreach (var item in ids)
            {
                ExamQuestions e = new ExamQuestions() { ExamId = Examid, QuestionId = item, InsertedAt = DateTime.Now };
                db.ExamQuestions.Add(e);
            
                //Console.WriteLine(row);
            }
            db.SaveChanges();

        }


        public List<ExamQuestions> ShowRandomQuestions(int Examid)
        {
            var questionsInOrder = db.ExamQuestions.Where(a => a.ExamId == Examid).Include(a=>a.Question).ThenInclude(b=>b.ChoicesList).OrderBy(e => e.InsertedAt).ToList();
            return questionsInOrder;
        }
    }
}
