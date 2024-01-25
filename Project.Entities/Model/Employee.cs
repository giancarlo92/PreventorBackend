using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Entities.Model
{
    public class Employee
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("nombre")]
        public string Nombre { get; set; }
        [Column("apellido")]
        public string Apellido { get; set; }
        [Column("email")]
        public string Email { get; set; }
        [Column("fecha_contratacion")]
        public DateTime FechaContratacion { get; set; }
    }
}
