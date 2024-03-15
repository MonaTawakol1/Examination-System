using Examination_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Examination_System.Repository
{
    public interface IChoiceRepo
    {
    
    public Choice GetChoice(int id);
    
    
    
    }

    public class ChoiceRepo:IChoiceRepo
    {
        ItiContext db;

        public ChoiceRepo(ItiContext _db)
        {
            db = _db;
        }
        public Choice GetChoice(int id)
        {
            Choice ch =db.Choices.FirstOrDefault(a=>a.ChoiceId == id);
            return ch;
        }

    }
}
