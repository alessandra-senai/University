using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace University.Models
{
    [Table("Nota")]
    public class Note
    {
        public int Id { get; set; }

        [Required]
        [Column("Nota")]
        public decimal Value { get; set; }

        [Required]
        [Column("IdMatricula")]
        [ForeignKey("Registration")]
        public int IdRegistration { get; set; }

        [Required]
        [Column("IdNotaPeriodo")]
        [ForeignKey("PeriodNote")]
        public int IdPeriodNote { get; set; }

        public Registration? Registration { get; set; }
        public PeriodNote? PeriodNote { get; set; }
    }
}