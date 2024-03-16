using Examination_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Examination_System.Repository
{
    public interface ICourseRepo
    {
        public List<Topic> GetAllTopics(int courseId);
    }
    public class CourseRepo : ICourseRepo
    {
        ItiContext db;

        public CourseRepo(ItiContext _db)
        {
            db = _db;
        }
        public List<Topic> GetAllTopics(int courseId)
        {
            var course = db.Courses.Include(i => i.Topics).SingleOrDefault(i => i.CourseId == courseId);
            if(course == null)
            {
                return new List<Topic>();
            }
            else
            {
                return course.Topics.ToList();
            }

        }
    }
}
