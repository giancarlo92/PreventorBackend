using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Entities.Model
{
    public class StudentCourse
    {
        [Column("studentid")]
        public int StudentId { get; set; }
        [Column("courseid")]
        public int CourseId { get; set; }
        
        public virtual Student Student { get; set; }
        public virtual Course Course { get; set; }
    }

}
