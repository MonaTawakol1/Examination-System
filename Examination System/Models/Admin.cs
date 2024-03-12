using System.ComponentModel.DataAnnotations;

namespace Examination_System.Models
{
    public class Admin
    {
        [Key]
        [Required]
        public int AdminId { get; set; }
        //public string Name { get; set; }
        //public string Email { get; set; }
        //public string Password { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

    }
}
