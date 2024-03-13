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
        public void CorrectExam(int examId);
        public void AddExamGrade(int examId);
        public Course getCourseById(int id);
        public void AddExamDuration(int id, DateTime start, DateTime end, int TorF, int Mcq);
    }
    public class ExamRepo : IExamRepo
    {
        ItiContext db;
        IQuestionRepo _questionRepo;

        public ExamRepo(ItiContext _db, IQuestionRepo questionRepo)
        {
            db = _db;
            _questionRepo = questionRepo;

        }


        public void AddExam(Exam exam)
        {
            db.Add(exam);
            db.SaveChanges();

        }


        public void AddExamQuestions(int Examid, int CrsId)
        {
            var random = new Random();
            List<int> randomQuestion = new List<int>();
            List<int> ids = new List<int>();
            var questionsBank = db.Questions.Where(x => x.CourseId == CrsId).Where(a => a.isDeleted == false).ToList();
            questionsBank.ForEach
                (x =>
                {
                    ids.Add(x.QuestionId);
                });


            Random rand = new Random();
            Course crs = db.Courses.FirstOrDefault(a => a.CourseId == CrsId);
            int numOfTFQ = crs.NumberOfTrueAndFalseQuestions.Value;
            int numOfMCQQ = db.Courses.FirstOrDefault(a => a.CourseId == CrsId).NumberOfMcqQuestions.Value;
            int count = numOfTFQ + numOfMCQQ;
            while (randomQuestion.Count() < count)
            {
                int k = rand.Next(ids.Count());
                var value = ids[k];
                Question q = _questionRepo.GetQuestion(value);
                if (q.QuestionType == QuestionType.TrueOrFalse && numOfTFQ != 0)
                {
                    ids.Remove(ids[k]);
                    randomQuestion.Add(value);

                    numOfTFQ--;


                }
                else if (q.QuestionType == QuestionType.Mcq && numOfMCQQ != 0)
                {
                    ids.Remove(ids[k]);
                    randomQuestion.Add(value);

                    numOfMCQQ--;


                }


            }

            foreach (var item in randomQuestion)
            {
                ExamQuestions e = new ExamQuestions() { ExamId = Examid, QuestionId = item, InsertedAt = DateTime.Now };
                db.ExamQuestions.Add(e);


            }
            db.SaveChanges();

        }


        public List<ExamQuestions> ShowRandomQuestions(int Examid)
        {
            var questionsInOrder = db.ExamQuestions.Where(a => a.ExamId == Examid).Include(a => a.Exam).Include(a => a.Question).ThenInclude(b => b.ChoicesList).OrderBy(e => e.InsertedAt).ToList();
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

        public void CorrectExam(int examId)
        {
            var ExamQuestions = db.ExamQuestions.Include(a => a.Question).ThenInclude(a => a.ChoicesList).Where(a => a.ExamId == examId).ToList();
            foreach (var examQuestion in ExamQuestions)
            {
                var choice = db.Choices.FirstOrDefault(a => a.ChoiceId == examQuestion.ExamAnswers);
                if (choice != null)
                {
                    if (choice.IsAnswer == true)
                    {
                        examQuestion.IsCorrect = true;
                    }
                    else
                    {
                        examQuestion.IsCorrect = false;
                    }
                    db.ExamQuestions.Update(examQuestion);
                }
            }

            db.SaveChanges();
        }

        public void AddExamGrade(int examId)
        {
            var RightQuestions = db.ExamQuestions.Where(a => a.ExamId == examId && a.IsCorrect == true).ToList();
            int grade = 0;
            foreach (var rightQuestion in RightQuestions)
            {
                var question = db.Questions.FirstOrDefault(a => a.QuestionId == rightQuestion.QuestionId);
                grade += question.QuestionMark;
            }
            var exam = db.Exams.FirstOrDefault(a => a.ExamId == examId);
            exam.StudentGrade = grade;
            db.Exams.Update(exam);
            db.SaveChanges();
        }
        public Course getCourseById(int id)
        {
            return db.Courses.FirstOrDefault(a => a.CourseId == id);
        }

        public void AddExamDuration(int id, DateTime start, DateTime end, int TorF, int Mcq)
        {
            Course crs = getCourseById(id);
            crs.ExamStartDateTime = start;
            crs.ExamEndDateTime = end;
            crs.NumberOfMcqQuestions = Mcq;
            crs.NumberOfTrueAndFalseQuestions = TorF;
            db.Courses.Update(crs);
            db.SaveChanges();
        }
    }
}
