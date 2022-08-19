using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using University.Context;
using University.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace University.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstructorsController : ControllerBase
    {
        private UniversityContext _universityContext;
        private readonly ILogger<InstructorsController> _logger;

        public InstructorsController(UniversityContext universityContext
            , ILogger<InstructorsController> logger)
        {
            _universityContext = universityContext;
            _logger = logger;
        }

        /// <summary>
        /// Retorna uma  lista de instrutores
        /// </summary>
        /// <returns>Retorna instrutores cadastrados no banco de dados</returns>
        /// <response code="200">Retorna uma lista de instrutores</response>
        /// <response code="404"> Não encontrou lista de instrutores</response>
        /// <response code="500">Ocorreu erro durante a execução</response>

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Instructor>>> Get()
        {
            try
            {
                var instructors = await _universityContext.Instructors
                .ToListAsync();

                _logger.LogInformation($"Class:{nameof(InstructorsController)}-Method:{nameof(Get)}");


                return instructors.Any() ? Ok(instructors) : StatusCode(404);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Class:{nameof(InstructorsController)}-Method:{nameof(Get)}");

                return StatusCode(500);
            }

        }

        /// <summary>
        /// Retorna instrutor
        /// </summary>
        /// <param name="id">Id do Instrutor</param>
        /// <returns>Retorna instrutor cadastrado no banco de dados</returns>
        /// <response code="200">Retorna instrutor</response>
        /// <response code="404"> Não encontrou instrutor pesquisado</response>
        /// <response code="500">Ocorreu erro durante a execução</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Instructor>> GetById(int id)
        {
            try
            {
                var instructor = await _universityContext.Instructors.FindAsync(id);

                _logger.LogInformation($"Class:{nameof(InstructorsController)}-Method:{nameof(GetById)}");


                return instructor is not null ? Ok(instructor) : StatusCode(404);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Class:{nameof(InstructorsController)}-Method:{nameof(GetById)}");

                return StatusCode(500);
            }
        }

        /// <summary>
        /// Inserir Instrutor
        /// </summary>
        /// <param name="instructor">Instrutor</param>
        /// <returns>Retorna instrutor inserido com sucesso no banco de dados</returns>
        /// <response code="201">Instrutor inserido com sucesso</response>
        /// <response code="404"> Inserção não realizada</response>
        /// <response code="500">Ocorreu erro durante a execução</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] Instructor instructor)
        {
            try
            {
                _universityContext.Instructors.Add(instructor);
                await _universityContext.SaveChangesAsync();

                _logger.LogInformation($"Class:{nameof(InstructorsController)}-Method:{nameof(Post)}");


                return CreatedAtAction("GetById", new { id = instructor.Id }, instructor);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Class:{nameof(InstructorsController)}-Method:{nameof(Post)}");

                return StatusCode(500);
            }
        }

        /// <summary>
        /// Atualizar Instrutor
        /// </summary>
        /// <param name="instructor">Instrutor</param>
        /// <returns>Retorna instrutor atualizado com sucesso no banco de dados</returns>
        /// <response code="201">Instrutor atualizado com sucesso</response>
        /// <response code="404"> Atualização não realizada</response>
        /// <response code="500">Ocorreu erro durante a execução</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put([FromBody] Instructor instructor)
        {
            try
            {
                bool instructorExist = _universityContext.Instructors.Any(e => e.Id == instructor.Id);

                if (!instructorExist)
                    return NotFound();

                _universityContext.Update(instructor);

                await _universityContext.SaveChangesAsync();

                _logger.LogInformation($"Class:{nameof(InstructorsController)}-Method:{nameof(Put)}");

                return StatusCode(202);

            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Class:{nameof(InstructorsController)}-Method:{nameof(Put)}");

                return StatusCode(500);
            }
        }

        /// <summary>
        /// Remover Instrutor
        /// </summary>
        /// <param name="id">Id Instrutor</param>
        /// <returns>Retorna instrutor excluído com sucesso no banco de dados</returns>
        /// <response code="201">Instrutor excluído com sucesso</response>
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
                var instructor = await _universityContext.Instructors.FindAsync(id);

                if (instructor is null)
                    return NotFound();

                _universityContext.Instructors.Remove(instructor);
                await _universityContext.SaveChangesAsync();

                _logger.LogInformation($"Class:{nameof(InstructorsController)}-Method:{nameof(Delete)}");

                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Class:{nameof(InstructorsController)}-Method:{nameof(Delete)}");

                return StatusCode(500);
            }
        }
    }
}