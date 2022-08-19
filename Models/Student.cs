using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace University.Models
{
    [Table("Aluno")]
    public class Student
    {
        public int Id { get; set; }

        [Required]
        [Column("Nome")]
        public string Name { get; set; }

        [Required]
        public string CPF { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [Column("Telefone")]
        public string Phone { get; set; }

        [Column("DataNascimento")]
        public DateTime? Birthday { get; set; }
    }
}
