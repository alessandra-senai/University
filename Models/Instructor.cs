using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace University.Models
{
    [Table("Instrutor")]
    public class Instructor
    {
        public int Id { get; set; }

        [Required]
        [Column("Nome")]
        [StringLength(150)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [Column("Telefone")]
        [StringLength(50)]
        public string Phone { get; set; }

        [Column("ValorHora")]
        public double? ValueHour { get; set; }

        [Column("Certificados")]
        [StringLength(255)]
        public string? Certificates { get; set; }
    }
}