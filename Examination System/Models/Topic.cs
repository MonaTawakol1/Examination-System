using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Examination_System.Models
{
    public class Topic
    {
        public int TopicId {  get; set; }

        [Required]
        public string TopicName { get; set; }

        [ForeignKey("Course")]
        public int CourseId { get; set; }

        Course Course { get; set; }
    }
}
