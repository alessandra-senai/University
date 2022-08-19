using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using University.Context;
using University.Mock;
using University.Models;

namespace University.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0", Deprecated = true)]
    public class StudentsController : ControllerBase
    {

        private UniversityContext _universityContext;
        private readonly ILogger<StudentsController> _logger;

        public StudentsController(UniversityContext universityContext
            , ILogger<StudentsController> logger)
        {
            _universityContext = universityContext;
            _logger = logger;
        }

        /// <summary>
        /// Retorna uma  lista de alunos
        /// </summary>
        /// <returns>Retorna alunos cadastrados no banco de dados</returns>
        /// <response code="200">Retorna uma lista de alunos</response>
        /// <response code="404"> Não encontrou lista de alunos</response>
        /// <response code="500">Ocorreu erro durante a execução</response>

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Student>>> Get()
        {
            try
            {
                var students = await _universityContext.Students
                .ToListAsync();

                _logger.LogInformation($"Class:{nameof(StudentsController)}-Method:{nameof(Get)}");


                return students.Any() ? Ok(students) : StatusCode(404);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Class:{nameof(StudentsController)}-Method:{nameof(Get)}");

                return StatusCode(500);
            }

        }

        /// <summary>
        /// Retorna aluno
        /// </summary>
        /// <param name="id">Id do Aluno</param>
        /// <returns>Retorna aluno cadastrado no banco de dados</returns>
        /// <response code="200">Retorna aluno</response>
        /// <response code="404"> Não encontrou aluno pesquisado</response>
        /// <response code="500">Ocorreu erro durante a execução</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Student>> GetById(int id)
        {
            try
            {
                var student = await _universityContext.Students.FindAsync(id);

                _logger.LogInformation($"Class:{nameof(StudentsController)}-Method:{nameof(GetById)}");


                return student is not null ? Ok(student) : StatusCode(204);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Class:{nameof(StudentsController)}-Method:{nameof(GetById)}");

                return StatusCode(500);
            }
        }

        /// <summary>
        /// Inserir Aluno
        /// </summary>
        /// <param name="student">Aluno</param>
        /// <returns>Retorna aluno inserido com sucesso no banco de dados</returns>
        /// <response code="201">Aluno inserido com sucesso</response>
        /// <response code="404"> Inserção não realizada</response>
        /// <response code="500">Ocorreu erro durante a execução</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] Student student)
        {
            try
            {
                _universityContext.Students.Add(student);
                await _universityContext.SaveChangesAsync();

                _logger.LogInformation($"Class:{nameof(StudentsController)}-Method:{nameof(Post)}");


                return CreatedAtAction("GetById", new { id = student.Id }, student);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Class:{nameof(StudentsController)}-Method:{nameof(Post)}");

                return StatusCode(500);
            }
        }

        /// <summary>
        /// Atualizar Aluno
        /// </summary>
        /// <param name="student">Aluno</param>
        /// <returns>Retorna aluno atualizado com sucesso no banco de dados</returns>
        /// <response code="201">Aluno atualizado com sucesso</response>
        /// <response code="404"> Atualização não realizada</response>
        /// <response code="500">Ocorreu erro durante a execução</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put([FromBody] Student student)
        {
            try
            {
                bool studentExist = _universityContext.Students.Any(e => e.Id == student.Id);

                if (!studentExist)
                    return NotFound();
                 
                _universityContext.Update(student);

                await _universityContext.SaveChangesAsync();

                _logger.LogInformation($"Class:{nameof(StudentsController)}-Method:{nameof(Put)}");

                return StatusCode(202);

            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Class:{nameof(StudentsController)}-Method:{nameof(Put)}");

                return StatusCode(500);
            }
        }

        /// <summary>
        /// Remover Aluno
        /// </summary>
        /// <param name="id">Id Aluno</param>
        /// <returns>Retorna aluno excluído com sucesso no banco de dados</returns>
        /// <response code="201">Aluno excluído com sucesso</response>
        /// <response code="404"> Exclusão não realizada</response>
        /// <response code="500">Ocorreu erro durante a execução</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var student = await _universityContext.Students.FindAsync(id);

                if (student is null)
                    return NotFound();

                _universityContext.Students.Remove(student);
                await _universityContext.SaveChangesAsync();

                _logger.LogInformation($"Class:{nameof(StudentsController)}-Method:{nameof(Delete)}");

                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Class:{nameof(StudentsController)}-Method:{nameof(Delete)}");

                return StatusCode(500);
            } 
        }
    }
}