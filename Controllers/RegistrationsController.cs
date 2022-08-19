using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using University.Context;
using University.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace University.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationsController : ControllerBase
    {
        private UniversityContext _universityContext;
        private readonly ILogger<RegistrationsController> _logger;

        public RegistrationsController(UniversityContext universityContext
            , ILogger<RegistrationsController> logger)
        {
            _universityContext = universityContext;
            _logger = logger;
        }

        /// <summary>
        /// Retorna uma  lista de matrículas
        /// </summary>
        /// <returns>Retorna matrículas cadastrados no banco de dados</returns>
        /// <response code="200">Retorna uma lista de matrículas</response>
        /// <response code="404"> Não encontrou lista de matrículas</response>
        /// <response code="500">Ocorreu erro durante a execução</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Registration>>> Get()
        {
            try
            {
                var registrations = await _universityContext.Registrations
                    .Include(x => x.Team)
                    .ThenInclude(z => z.Instructor)
                    .Include(w => w.Team.CourseAway)
                    .Include(y => y.Student) 
                .ToListAsync();

                _logger.LogInformation($"Class:{nameof(RegistrationsController)}-Method:{nameof(Get)}");


                return registrations.Any() ? Ok(registrations) : StatusCode(404);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Class:{nameof(RegistrationsController)}-Method:{nameof(Get)}");

                return StatusCode(500);
            }

        }

        /// <summary>
        /// Retorna matrícula
        /// </summary>
        /// <param name="id">Id do Matrícula</param>
        /// <returns>Retorna matrícula cadastrado no banco de dados</returns>
        /// <response code="200">Retorna matrícula</response>
        /// <response code="404"> Não encontrou matrícula pesquisado</response>
        /// <response code="500">Ocorreu erro durante a execução</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Registration>> GetById(int id)
        {
            try
            {
                var registration = await _universityContext.Registrations
                    .Include(x => x.Team)
                    .Include(z => z.Team.Instructor)
                    .Include(w => w.Team.CourseAway)
                    .Include(y => y.Student)
                    .FirstOrDefaultAsync(x => x.Id == id);

                _logger.LogInformation($"Class:{nameof(RegistrationsController)}-Method:{nameof(GetById)}");


                return registration is not null ? Ok(registration) : StatusCode(404);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Class:{nameof(RegistrationsController)}-Method:{nameof(GetById)}");

                return StatusCode(500);
            }
        }

        /// <summary>
        /// Inserir Matrícula
        /// </summary>
        /// <param name="registration">Matrícula</param>
        /// <returns>Retorna matrícula inserido com sucesso no banco de dados</returns>
        /// <response code="201">Matrícula inserido com sucesso</response>
        /// <response code="404"> Inserção não realizada</response>
        /// <response code="500">Ocorreu erro durante a execução</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] Registration registration)
        {
            try
            {
                _universityContext.Registrations.Add(registration);
                await _universityContext.SaveChangesAsync();

                _logger.LogInformation($"Class:{nameof(RegistrationsController)}-Method:{nameof(Post)}");


                return CreatedAtAction("GetById", new { id = registration.Id }, registration);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Class:{nameof(RegistrationsController)}-Method:{nameof(Post)}");

                return StatusCode(500);
            }
        }

        /// <summary>
        /// Atualizar Matrícula
        /// </summary>
        /// <param name="registration">Matrícula</param>
        /// <returns>Retorna matrícula atualizado com sucesso no banco de dados</returns>
        /// <response code="201">Matrícula atualizado com sucesso</response>
        /// <response code="404"> Atualização não realizada</response>
        /// <response code="500">Ocorreu erro durante a execução</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put([FromBody] Registration registration)
        {
            try
            {
                bool teamExist = _universityContext.Registrations.Any(e => e.Id == registration.Id);

                if (!teamExist)
                    return NotFound();

                _universityContext.Entry(registration).State = EntityState.Modified;

                await _universityContext.SaveChangesAsync();

                _logger.LogInformation($"Class:{nameof(RegistrationsController)}-Method:{nameof(Put)}");

                return StatusCode(202);

            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Class:{nameof(RegistrationsController)}-Method:{nameof(Put)}");

                return StatusCode(500);
            }
        }

        /// <summary>
        /// Remover Matrícula
        /// </summary>
        /// <param name="id">Id Matrícula</param>
        /// <returns>Retorna matrícula excluído com sucesso no banco de dados</returns>
        /// <response code="201">Matrícula excluído com sucesso</response>
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
                var registration = await _universityContext.Registrations.FindAsync(id);

                if (registration is null)
                    return NotFound();

                _universityContext.Registrations.Remove(registration);
                await _universityContext.SaveChangesAsync();

                _logger.LogInformation($"Class:{nameof(RegistrationsController)}-Method:{nameof(Delete)}");

                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Class:{nameof(RegistrationsController)}-Method:{nameof(Delete)}");

                return StatusCode(500);
            }
        }
    }
}