using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Entities.Model
{
    public class Course
    {
        [Column("courseid")]
        public int CourseId { get; set; }
        [Column("name")]
        public string Name { get; set; }

        public virtual ICollection<StudentCourse> StudentCourses { get; set; }
    }

}
