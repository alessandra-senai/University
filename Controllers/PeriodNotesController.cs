using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using University.Context;
using University.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace University.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeriodNotesController : ControllerBase
    {
        private UniversityContext _universityContext;
        private readonly ILogger<PeriodNotesController> _logger;

        public PeriodNotesController(UniversityContext universityContext
            , ILogger<PeriodNotesController> logger)
        {
            _universityContext = universityContext;
            _logger = logger;
        }

        /// <summary>
        /// Retorna uma  lista de notas períodos
        /// </summary>
        /// <returns>Retorna notas períodos cadastrados no banco de dados</returns>
        /// <response code="200">Retorna uma lista de notas períodos</response>
        /// <response code="404"> Não encontrou lista de notas períodos</response>
        /// <response code="500">Ocorreu erro durante a execução</response>

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<PeriodNote>>> Get()
        {
            try
            {
                var periodNotes = await _universityContext.PeriodNotes
                .ToListAsync();

                _logger.LogInformation($"Class:{nameof(PeriodNotesController)}-Method:{nameof(Get)}");


                return periodNotes.Any() ? Ok(periodNotes) : StatusCode(404);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Class:{nameof(PeriodNotesController)}-Method:{nameof(Get)}");

                return StatusCode(500);
            }

        }

        /// <summary>
        /// Retorna nota período
        /// </summary>
        /// <param name="id">Id do Nota Período</param>
        /// <returns>Retorna nota período cadastrado no banco de dados</returns>
        /// <response code="200">Retorna nota período</response>
        /// <response code="404"> Não encontrou nota período pesquisado</response>
        /// <response code="500">Ocorreu erro durante a execução</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PeriodNote>> GetById(int id)
        {
            try
            {
                var periodNote = await _universityContext.PeriodNotes.FindAsync(id);

                _logger.LogInformation($"Class:{nameof(PeriodNotesController)}-Method:{nameof(GetById)}");


                return periodNote is not null ? Ok(periodNote) : StatusCode(204);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Class:{nameof(PeriodNotesController)}-Method:{nameof(GetById)}");

                return StatusCode(500);
            }
        }

        /// <summary>
        /// Inserir Nota Período
        /// </summary>
        /// <param name="periodNote">Nota Período</param>
        /// <returns>Retorna nota período inserido com sucesso no banco de dados</returns>
        /// <response code="201">Nota Período inserido com sucesso</response>
        /// <response code="404"> Inserção não realizada</response>
        /// <response code="500">Ocorreu erro durante a execução</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] PeriodNote periodNote)
        {
            try
            {
                _universityContext.PeriodNotes.Add(periodNote);
                await _universityContext.SaveChangesAsync();

                _logger.LogInformation($"Class:{nameof(PeriodNotesController)}-Method:{nameof(Post)}");


                return CreatedAtAction("GetById", new { id = periodNote.Id }, periodNote);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Class:{nameof(PeriodNotesController)}-Method:{nameof(Post)}");

                return StatusCode(500);
            }
        }

        /// <summary>
        /// Atualizar Nota Período
        /// </summary>
        /// <param name="periodNote">Nota Período</param>
        /// <returns>Retorna nota período atualizado com sucesso no banco de dados</returns>
        /// <response code="201">Nota Período atualizado com sucesso</response>
        /// <response code="404"> Atualização não realizada</response>
        /// <response code="500">Ocorreu erro durante a execução</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put([FromBody] PeriodNote periodNote)
        {
            try
            {
                bool periodNoteExist = _universityContext.PeriodNotes.Any(e => e.Id == periodNote.Id);

                if (!periodNoteExist)
                    return NotFound();

                _universityContext.Update(periodNote);

                await _universityContext.SaveChangesAsync();

                _logger.LogInformation($"Class:{nameof(PeriodNotesController)}-Method:{nameof(Put)}");

                return StatusCode(202);

            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Class:{nameof(PeriodNotesController)}-Method:{nameof(Put)}");

                return StatusCode(500);
            }
        }

        /// <summary>
        /// Remover Nota Período
        /// </summary>
        /// <param name="id">Id Nota Período</param>
        /// <returns>Retorna nota período excluído com sucesso no banco de dados</returns>
        /// <response code="201">Nota Período excluído com sucesso</response>
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
                var periodNote = await _universityContext.PeriodNotes.FindAsync(id);

                if (periodNote is null)
                    return NotFound();

                _universityContext.PeriodNotes.Remove(periodNote);
                await _universityContext.SaveChangesAsync();

                _logger.LogInformation($"Class:{nameof(PeriodNotesController)}-Method:{nameof(Delete)}");

                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Class:{nameof(PeriodNotesController)}-Method:{nameof(Delete)}");

                return StatusCode(500);
            }
        }
    }
}