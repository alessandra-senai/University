using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace University.Models
{
    [Table("NotaPeriodo")]
    public class PeriodNote
    {

        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Column("Periodo")]
        public string Period { get; set; }

    }
}
