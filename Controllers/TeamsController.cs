 using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using University.Context;
using University.Models;

namespace University.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private UniversityContext _universityContext;
        private readonly ILogger<TeamsController> _logger;

        public TeamsController(UniversityContext universityContext
            , ILogger<TeamsController> logger)
        {
            _universityContext = universityContext;
            _logger = logger;
        }

        /// <summary>
        /// Retorna uma  lista de turmas
        /// </summary>
        /// <returns>Retorna turmas cadastrados no banco de dados</returns>
        /// <response code="200">Retorna uma lista de turmas</response>
        /// <response code="404"> Não encontrou lista de turmas</response>
        /// <response code="500">Ocorreu erro durante a execução</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Team>>> Get()
        {
            try
            {
                var teams = await _universityContext.Teams
                    .Include(x => x.CourseAway)
                    .Include(y => y.Instructor)
                    .ToListAsync();

                _logger.LogInformation($"Class:{nameof(TeamsController)}-Method:{nameof(Get)}");


                return teams.Any() ? Ok(teams) : StatusCode(404);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Class:{nameof(TeamsController)}-Method:{nameof(Get)}");

                return StatusCode(500);
            }

        }

        /// <summary>
        /// Retorna turma
        /// </summary>
        /// <param name="id">Id do Turma</param>
        /// <returns>Retorna turma cadastrado no banco de dados</returns>
        /// <response code="200">Retorna turma</response>
        /// <response code="404"> Não encontrou turma pesquisado</response>
        /// <response code="500">Ocorreu erro durante a execução</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Team>> GetById(int id)
        {
            try
            {
                var team = await _universityContext.Teams
                    .Include(x => x.CourseAway)
                    .Include(y => y.Instructor).FirstOrDefaultAsync(x => x.Id == id);

                _logger.LogInformation($"Class:{nameof(TeamsController)}-Method:{nameof(GetById)}");


                return team is not null ? Ok(team) : StatusCode(204);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Class:{nameof(TeamsController)}-Method:{nameof(GetById)}");

                return StatusCode(500);
            }
        }

        /// <summary>
        /// Inserir Turma
        /// </summary>
        /// <param name="team">Turma</param>
        /// <returns>Retorna turma inserido com sucesso no banco de dados</returns>
        /// <response code="201">Turma inserido com sucesso</response>
        /// <response code="404"> Inserção não realizada</response>
        /// <response code="500">Ocorreu erro durante a execução</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] Team team)
        {
            try
            {
                _universityContext.Teams.Add(team);
                await _universityContext.SaveChangesAsync();

                _logger.LogInformation($"Class:{nameof(TeamsController)}-Method:{nameof(Post)}");


                return CreatedAtAction("GetById", new { id = team.Id }, team);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Class:{nameof(TeamsController)}-Method:{nameof(Post)}");

                return StatusCode(500);
            }
        }

        /// <summary>
        /// Atualizar Turma
        /// </summary>
        /// <param name="team">Turma</param>
        /// <returns>Retorna turma atualizado com sucesso no banco de dados</returns>
        /// <response code="201">Turma atualizado com sucesso</response>
        /// <response code="404"> Atualização não realizada</response>
        /// <response code="500">Ocorreu erro durante a execução</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put([FromBody] Team team)
        {
            try
            {
                bool teamExist = _universityContext.Teams.Any(e => e.Id == team.Id);

                if (!teamExist)
                    return NotFound();

                _universityContext.Entry(team).State = EntityState.Modified;

                await _universityContext.SaveChangesAsync();

                _logger.LogInformation($"Class:{nameof(TeamsController)}-Method:{nameof(Put)}");

                return StatusCode(202);

            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Class:{nameof(TeamsController)}-Method:{nameof(Put)}");

                return StatusCode(500);
            }
        }

        /// <summary>
        /// Remover Turma
        /// </summary>
        /// <param name="id">Id Turma</param>
        /// <returns>Retorna turma excluído com sucesso no banco de dados</returns>
        /// <response code="201">Turma excluído com sucesso</response>
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
                var team = await _universityContext.Teams.FindAsync(id);

                if (team is null)
                    return NotFound();

                _universityContext.Teams.Remove(team);
                await _universityContext.SaveChangesAsync();

                _logger.LogInformation($"Class:{nameof(TeamsController)}-Method:{nameof(Delete)}");

                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Class:{nameof(TeamsController)}-Method:{nameof(Delete)}");

                return StatusCode(500);
            }
        }
    }
}