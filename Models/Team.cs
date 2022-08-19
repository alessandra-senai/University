using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace University.Models
{
    [Table("Turma")]
    public class Team
    {
        public int Id { get; set; }

        [Required]
        [Column("IdInstrutor")]
        [ForeignKey("Instructor")]
        public int IdInstructor { get; set; }

        public int CourseTeamId { get; set; }

        public int CourseAwayId { get; set; }


        [Column("DataInicio")]
        public DateTime? InitialDate { get; set; }

        [Column("DataFinal")]
        public DateTime? FinishDate { get; set; }

        [Column("CargaHoraria")]
        public int? Workload { get; set; }

        public Instructor? Instructor { get; set; }


        public virtual Course? CourseTeam { get; set; }
        public virtual Course? CourseAway { get; set; }


    }
}
