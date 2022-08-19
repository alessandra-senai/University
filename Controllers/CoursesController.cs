using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using University.Context;
using University.Models;

namespace University.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private UniversityContext _universityContext;
        private readonly ILogger<CoursesController> _logger;

        public CoursesController(UniversityContext universityContext
            , ILogger<CoursesController> logger)
        {
            _universityContext = universityContext;
            _logger = logger;
        }

        /// <summary>
        /// Retorna uma  lista de cursos
        /// </summary>
        /// <returns>Retorna cursos cadastrados no banco de dados</returns>
        /// <response code="200">Retorna uma lista de cursos</response>
        /// <response code="404"> Não encontrou lista de cursos</response>
        /// <response code="500">Ocorreu erro durante a execução</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Course>>> Get()
        {
            try
            {
                var courses = await _universityContext.Courses
                .ToListAsync();

                _logger.LogInformation($"Class:{nameof(CoursesController)}-Method:{nameof(Get)}");


                return courses.Any() ? Ok(courses) : StatusCode(404);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Class:{nameof(CoursesController)}-Method:{nameof(Get)}");

                return StatusCode(500);
            }

        }

        /// <summary>
        /// Retorna curso
        /// </summary>
        /// <param name="id">Id do Curso</param>
        /// <returns>Retorna curso cadastrado no banco de dados</returns>
        /// <response code="200">Retorna curso</response>
        /// <response code="404"> Não encontrou curso pesquisado</response>
        /// <response code="500">Ocorreu erro durante a execução</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Course>> GetById(int id)
        {
            try
            {
                var course = await _universityContext.Courses.FindAsync(id);

                _logger.LogInformation($"Class:{nameof(CoursesController)}-Method:{nameof(GetById)}");


                return course is not null ? Ok(course) : StatusCode(404);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Class:{nameof(CoursesController)}-Method:{nameof(GetById)}");

                return StatusCode(500);
            }
        }

        /// <summary>
        /// Inserir Curso
        /// </summary>
        /// <param name="course">Curso</param>
        /// <returns>Retorna curso inserido com sucesso no banco de dados</returns>
        /// <response code="201">Curso inserido com sucesso</response>
        /// <response code="404"> Inserção não realizada</response>
        /// <response code="500">Ocorreu erro durante a execução</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] Course course)
        {
            try
            {
                _universityContext.Courses.Add(course);
                await _universityContext.SaveChangesAsync();

                _logger.LogInformation($"Class:{nameof(CoursesController)}-Method:{nameof(Post)}");


                return CreatedAtAction("GetById", new { id = course.Id }, course);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Class:{nameof(CoursesController)}-Method:{nameof(Post)}");

                return StatusCode(500);
            }
        }

        /// <summary>
        /// Atualizar Curso
        /// </summary>
        /// <param name="course">Curso</param>
        /// <returns>Retorna curso atualizado com sucesso no banco de dados</returns>
        /// <response code="201">Curso atualizado com sucesso</response>
        /// <response code="404"> Atualização não realizada</response>
        /// <response code="500">Ocorreu erro durante a execução</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put([FromBody] Course course)
        {
            try
            {
                bool courseExist = _universityContext.Courses.Any(e => e.Id == course.Id);

                if (!courseExist)
                    return NotFound();

                _universityContext.Entry(course).State = EntityState.Modified;

                await _universityContext.SaveChangesAsync();

                _logger.LogInformation($"Class:{nameof(CoursesController)}-Method:{nameof(Put)}");

                return StatusCode(202);

            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Class:{nameof(CoursesController)}-Method:{nameof(Put)}");

                return StatusCode(500);
            }
        }

        /// <summary>
        /// Remover Curso
        /// </summary>
        /// <param name="id">Id Curso</param>
        /// <returns>Retorna curso excluído com sucesso no banco de dados</returns>
        /// <response code="201">Curso excluído com sucesso</response>
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
                var course = await _universityContext.Courses.FindAsync(id);

                if (course is null)
                    return NotFound();

                _universityContext.Courses.Remove(course);
                await _universityContext.SaveChangesAsync();

                _logger.LogInformation($"Class:{nameof(CoursesController)}-Method:{nameof(Delete)}");

                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Class:{nameof(CoursesController)}-Method:{nameof(Delete)}");

                return StatusCode(500);
            }
        }
    }
}