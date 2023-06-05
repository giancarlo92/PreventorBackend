using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Entities.Model
{
    public class Student
    {
        [Column("studentid")]
        public int StudentId { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("surname")]
        public string Surname { get; set; }
        [Column("documenttype")]
        public int? DocumentType { get; set; }
        [Column("passport")]
        public string Passport { get; set; }
        [Column("email")]
        public string Email { get; set; }
        [Column("phone")]
        public string Phone { get; set; }
    }
}
