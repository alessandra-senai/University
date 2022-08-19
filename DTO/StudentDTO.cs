using System.ComponentModel.DataAnnotations;

namespace University.DTO
{
    public class StudentDTO
    {
        [Required(ErrorMessage = "Informar nome")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Informar Valor")]
        public string CPF { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }
        public DateTime? Birthday { get; set; }
    }
}
