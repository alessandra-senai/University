using University.Models;

namespace University.Seeds
{
    public static class StudentSeed
    {
        public static List<Student> Seed { get; set; } = new List<Student>()
        {
            new Student
           {
               Id = 1,
               Birthday = new DateTime(2010, 09, 09),
               CPF = "111.222.333.40-0",
               Email = "aluno1@gmail.com",
               Name = "Aluno 1",
               Phone = "2132-3344"
           },
           new Student
           {
               Id = 2,
               Birthday = new DateTime(2010, 09, 09),
               CPF = "222.222.333.40-0",
               Email = "aluno2@gmail.com",
               Name = "Aluno 2",
               Phone = "4556-3344"
           }
        };
    }
}
