using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace University.Models
{
    [Table("Curso")]
    public class Course
    {
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        [Column("Nome")]
        public string Name { get; set; }

        [StringLength(255)]
        [Column("Requisito")]
        public string? Requirement { get; set; }

        [Column("CargaHoraria")]
        public int? Workload { get; set; }

        [Required]
        [Column("Valor")]
        public double Value { get; set; }
    }
}