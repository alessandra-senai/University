using University.Models;

namespace University.Mock
{
    public class MockStudents
    {
        public static List<Student> Students = new()
        {
              new Student
                {
                    Id = 1,
                    Birthday = DateTime.Now,
                    CPF = "111.222.333.444-56",
                    Email = "aluno1@senai.br",
                    Name = "Aluno 1",
                    Phone = "2345-5667"
                },
                new Student
                {
                    Id = 2,
                    Birthday = DateTime.Now,
                    CPF = "222.333.555.678-56",
                    Email = "aluno2@senai.br",
                    Name = "Aluno 2",
                    Phone = "9087-0988"
                },
                new Student
                {
                    Id = 3,
                    Birthday = DateTime.Now,
                    CPF = "666.777.890-56",
                    Email = "aluno3@senai.br",
                    Name = "Aluno 3",
                    Phone = "3456-6678"
                }
        };

    }
}

